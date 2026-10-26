using System;
using System.Collections.Generic;

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

        private static Dictionary<string, Need> _lookup;

        public static int Count => Lookup.Count;

        public static Need Get(string questId)
        {
            if (string.IsNullOrEmpty(questId))
                return Need.None;

            return Lookup.TryGetValue(questId, out var need) ? need : Need.None;
        }

        // a quest listed twice (needed for Kappa and a lock-out) gets both flags
        private static Dictionary<string, Need> Lookup
        {
            get
            {
                if (_lookup == null)
                {
                    _lookup = new Dictionary<string, Need>(Entries.Length);
                    foreach (var entry in Entries)
                    {
                        _lookup.TryGetValue(entry.Id, out var need);
                        _lookup[entry.Id] = need | entry.Need;
                    }
                }
                return _lookup;
            }
        }
    }
}
