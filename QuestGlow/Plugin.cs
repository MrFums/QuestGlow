using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace QuestGlow
{
    public enum BothMode
    {
        GoldAndBlue,
        GoldOnly,
        BlueOnly
    }

    [BepInPlugin(Guid, Name, Version)]
    public class Plugin : BaseUnityPlugin
    {
        public const string Guid = "com.fums.questglow";
        public const string Name = "Fums-QuestGlow";
        public const string Version = "1.0.0";

        internal static ManualLogSource Log;

        internal static ConfigEntry<bool> Enabled;
        internal static ConfigEntry<bool> ShowKappa;
        internal static ConfigEntry<bool> ShowLightkeeper;
        internal static ConfigEntry<BothMode> Both;
        internal static ConfigEntry<bool> ShowLockout;

        internal static ConfigEntry<Color> KappaColor;
        internal static ConfigEntry<Color> LightkeeperColor;
        internal static ConfigEntry<Color> LockoutColor;
        internal static ConfigEntry<float> LapSeconds;
        internal static ConfigEntry<float> BeamLength;
        internal static ConfigEntry<float> Thickness;
        internal static ConfigEntry<float> Glow;
        internal static ConfigEntry<float> OutlineOpacity;

        private void Awake()
        {
            Log = Logger;

            Enabled = Config.Bind("1. Quests", "Enabled", true,
                "Show the moving outline on Kappa, Lightkeeper and lock-out quests.");
            ShowKappa = Config.Bind("1. Quests", "Mark Kappa quests", true,
                "Gold outline on quests needed for the Collector (Kappa container).");
            ShowLightkeeper = Config.Bind("1. Quests", "Mark Lightkeeper quests", true,
                "Blue outline on quests needed to unlock Lightkeeper.");
            Both = Config.Bind("1. Quests", "Quests needed for both", BothMode.GoldAndBlue,
                "GoldAndBlue: a gold and a blue beam chase each other round the box. "
                + "GoldOnly / BlueOnly: just the one colour.");
            ShowLockout = Config.Bind("1. Quests", "Mark lock-out quests", true,
                "Red outline on quests that fail for good if you complete a rival quest, "
                + "such as Big Customer, Out of Curiosity and Chemical - Part 4. Pick before you hand one in.");

            KappaColor = Config.Bind("2. Look", "Kappa colour", new Color(1f, 0.78f, 0.25f, 1f),
                "Colour of the Kappa beam.");
            LightkeeperColor = Config.Bind("2. Look", "Lightkeeper colour", new Color(0.25f, 0.62f, 1f, 1f),
                "Colour of the Lightkeeper beam.");
            LockoutColor = Config.Bind("2. Look", "Lock-out colour", new Color(1f, 0.27f, 0.22f, 1f),
                "Colour of the lock-out beam.");
            LapSeconds = Config.Bind("2. Look", "Seconds per lap", 4f,
                new ConfigDescription("How long the beam takes to go once round the box.",
                    new AcceptableValueRange<float>(1f, 20f)));
            BeamLength = Config.Bind("2. Look", "Beam length", 0.6f,
                new ConfigDescription("Length of the beam and its tail, as a share of the box outline.",
                    new AcceptableValueRange<float>(0.05f, 0.9f)));
            Thickness = Config.Bind("2. Look", "Line thickness", 2f,
                new ConfigDescription("Width of the beam's head in pixels. The tail thins out behind it.",
                    new AcceptableValueRange<float>(1f, 6f)));
            Glow = Config.Bind("2. Look", "Glow size", 4f,
                new ConfigDescription("How far the soft glow spreads outside the box. 0 turns it off.",
                    new AcceptableValueRange<float>(0f, 12f)));
            OutlineOpacity = Config.Bind("2. Look", "Resting outline opacity", 0.5f,
                new ConfigDescription("A faint steady outline drawn under the beam. 0 turns it off.",
                    new AcceptableValueRange<float>(0f, 1f)));

            var harmony = new Harmony(Guid);
            try
            {
                Patches.Apply(harmony);
            }
            catch (Exception e)
            {
                harmony.UnpatchSelf();
                Logger.LogError($"{Name} {Version} failed to patch the quest UI, nothing will be shown: {e}");
                return;
            }

            Logger.LogInfo($"{Name} {Version} loaded ({QuestList.Count} quests in the built-in list).");
            QuestList.Fetch();
        }
    }
}
