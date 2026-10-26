using Microsoft.AspNetCore.Http;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Common.Tables;
using SPTarkov.Server.Core.Models.Enums;
using SPTarkov.Server.Core.Models.Spt.Tables;
using SPTarkov.Server.Core.Servers.Http;
using SPTarkov.Server.Core.Utils;

namespace QuestGlow.Server;

// GET /questglow/quests for the client plugin: quest id -> flags (1 = Kappa, 2 = Lightkeeper, 4 = lock-out).
// Worked out from the server's quest data on every call, so quests and requirements changed by other mods
// are included. The client only ever gets the quests the player can see, so it can't do this itself.
[Injectable(InjectionType.Singleton, TypePriority = OnLoadOrder.Preload)]
public class QuestTreeRoute(TemplateTable templates, JsonUtil jsonUtil) : IHttpListener
{
    public const string Path = "/questglow/quests";

    private const string Collector = "5c51aac186f77432ea65c552";
    private const string KnockKnock = "625d7005a4eb80027c4f2e09";

    // same values as the client's Need flags
    private const int Kappa = 1;
    private const int Lightkeeper = 2;
    private const int Lockout = 4;

    public bool CanHandle(HttpContext context)
    {
        return context.Request.Path.Equals(Path, StringComparison.OrdinalIgnoreCase);
    }

    public async Task HandleAsync(MongoId sessionId, HttpContext context, CancellationToken cancellationToken)
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(jsonUtil.Serialize(Build()) ?? "{}", cancellationToken);
    }

    private Dictionary<string, int> Build()
    {
        var quests = templates.Quests.Values.ToDictionary(quest => quest.Id.ToString());
        var flags = new Dictionary<string, int>();

        void Mark(IEnumerable<string> ids, int flag)
        {
            foreach (var id in ids)
            {
                flags[id] = flags.GetValueOrDefault(id) | flag;
            }
        }

        Mark(Prerequisites(quests, Collector), Kappa);
        Mark(Prerequisites(quests, KnockKnock), Lightkeeper);

        // completing the target fails this quest for good, so both sides are a choice to make
        foreach (var quest in quests.Values)
        {
            foreach (var condition in QuestLinks(quest.Conditions.Fail))
            {
                Mark(Targets(condition).Append(quest.Id.ToString()), Lockout);
            }
        }

        return flags;
    }

    // Everything a quest needs before it can be started, all the way back. A link that also accepts
    // the earlier quest failed can be met by finishing a rival that fails it (Big Customer fails
    // Chemical - Part 4), so those rivals are on the way too.
    private static HashSet<string> Prerequisites(Dictionary<string, Quest> quests, string root)
    {
        var seen = new HashSet<string>();
        var todo = new Stack<string>();
        todo.Push(root);

        while (todo.Count > 0)
        {
            var id = todo.Pop();
            if (!quests.TryGetValue(id, out var quest) || !seen.Add(id))
            {
                continue;
            }

            foreach (var link in QuestLinks(quest.Conditions.AvailableForStart))
            {
                foreach (var target in Targets(link))
                {
                    todo.Push(target);

                    if (link.Status?.Contains(QuestStatusEnum.Fail) == true && quests.TryGetValue(target, out var linked))
                    {
                        foreach (var rival in QuestLinks(linked.Conditions.Fail).SelectMany(Targets))
                        {
                            todo.Push(rival);
                        }
                    }
                }
            }
        }

        return seen;
    }

    private static IEnumerable<QuestCondition> QuestLinks(List<QuestCondition>? conditions)
    {
        return conditions?.Where(condition => condition.ConditionType == "Quest") ?? [];
    }

    private static IEnumerable<string> Targets(QuestCondition condition)
    {
        if (condition.Target?.IsList == true)
        {
            return condition.Target.List!;
        }

        return condition.Target?.Item is { } item ? [item] : [];
    }
}
