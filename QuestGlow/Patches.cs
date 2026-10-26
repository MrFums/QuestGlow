using System;
using System.Linq;
using System.Reflection;
using EFT.Quests;
using EFT.UI;
using HarmonyLib;
using UnityEngine;

namespace QuestGlow
{
    internal static class Patches
    {
        public static void Apply(Harmony harmony)
        {
            // trader screen -> Tasks list
            harmony.Patch(Find(typeof(QuestListItem), "Init"),
                postfix: new HarmonyMethod(typeof(Patches), nameof(TraderListPostfix)));

            // character screen -> Tasks (and the same list in raid)
            harmony.Patch(Find(typeof(NotesTask), "Show"),
                postfix: new HarmonyMethod(typeof(Patches), nameof(NotesTaskPostfix)));
        }

        // Both methods are looked up by name and by taking a Quest first, so a game update that
        // changes them fails here at startup instead of quietly doing nothing.
        private static MethodInfo Find(Type type, string name)
        {
            var method = AccessTools.GetDeclaredMethods(type).FirstOrDefault(m =>
                m.Name == name
                && m.GetParameters().Length > 0
                && m.GetParameters()[0].ParameterType == typeof(Quest));

            if (method == null)
                throw new MissingMethodException(type.FullName, name);
            return method;
        }

        private static void TraderListPostfix(QuestListItem __instance, Quest quest)
        {
            Mark(__instance.transform, __instance.transform.GetSiblingIndex(), quest);
        }

        private static void NotesTaskPostfix(NotesTask __instance, Quest quest)
        {
            // The task row is TaskShortInfo. The description that opens under it gets parented
            // to the NotesTask itself, so the root would grow when the task is opened.
            Transform row = __instance.transform.Find("TaskShortInfo");
            if (row == null && __instance._taskLabel != null)
                row = ChildContaining(__instance.transform, __instance._taskLabel.transform);

            Mark(row != null ? row : __instance.transform, __instance.transform.GetSiblingIndex(), quest);
        }

        private static Transform ChildContaining(Transform root, Transform t)
        {
            while (t != null && t.parent != root)
                t = t.parent;
            return t;
        }

        private static void Mark(Transform box, int row, Quest quest)
        {
            try
            {
                Need need = quest?.Template != null ? QuestList.Get(quest.Template.Id) : Need.None;
                QuestBeam.Attach(box, need, row);
            }
            catch (Exception e)
            {
                // never let the outline break the quest screen
                Plugin.Log.LogError($"Could not mark quest {quest?.Template?.Id}: {e}");
            }
        }
    }
}
