using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SPT.Common.Http;

namespace QuestGlow
{
    [Flags]
    internal enum Need
    {
        None = 0,
        Kappa = 1,
        Lightkeeper = 2,
        Lockout = 4
    }

    internal static partial class QuestList
    {
        // short names so the table in QuestList.Data.cs stays readable
        private const Need K = Need.Kappa;
        private const Need L = Need.Lightkeeper;
        private const Need X = Need.Lockout;

        // Quest Glow's server part. It works the list out from the server's own quest data, so quests
        // and requirements changed by other mods are right. The game only sends the client the quests
        // the player can see, so the client can't work it out itself.
        private const string Route = "/questglow/quests";

        // a server without the server part answers every try with an error, so give up after a few
        private const int MaxTries = 3;
        private static readonly TimeSpan RetryAfter = TimeSpan.FromMinutes(1);

        // The built-in table, replaced whole by the server's answer so a reader never sees a half-built one.
        private static volatile Dictionary<string, Need> _lookup;
        private static volatile bool _fromServer;
        private static int _fetching;
        private static int _tries;
        private static DateTime _lastTry = DateTime.MinValue;

        // runs after the field initialisers of both parts, so Entries is filled in
        static QuestList()
        {
            var table = new Dictionary<string, Need>(Entries.Length);
            foreach (var entry in Entries)
            {
                // a quest listed twice (needed for Kappa and a lock-out) gets both flags
                table.TryGetValue(entry.Id, out var need);
                table[entry.Id] = need | entry.Need;
            }
            _lookup = table;
        }

        public static int Count => _lookup.Count;

        public static Need Get(string questId)
        {
            if (string.IsNullOrEmpty(questId))
                return Need.None;

            Fetch();
            return _lookup.TryGetValue(questId, out var need) ? need : Need.None;
        }

        // Asks the server in the background, so the quest screen never waits on it.
        public static void Fetch()
        {
            if (_fromServer || _tries >= MaxTries || DateTime.UtcNow - _lastTry < RetryAfter)
                return;
            if (Interlocked.Exchange(ref _fetching, 1) == 1)
                return;

            _tries++;
            _lastTry = DateTime.UtcNow;
            Task.Run(() =>
            {
                try
                {
                    var flags = JsonConvert.DeserializeObject<Dictionary<string, int>>(RequestHandler.GetJson(Route));
                    if (flags == null || flags.Count == 0)
                        throw new InvalidOperationException("empty answer");

                    _lookup = flags.ToDictionary(pair => pair.Key, pair => (Need)pair.Value);
                    _fromServer = true;
                    Plugin.Log.LogInfo($"Using the server's quest data ({flags.Count} quests marked).");
                }
                catch (Exception e)
                {
                    if (_tries >= MaxTries)
                        Plugin.Log.LogInfo($"Quest Glow's server part didn't answer ({e.Message}), using the built-in quest list.");
                }
                finally
                {
                    Interlocked.Exchange(ref _fetching, 0);
                }
            });
        }
    }
}
