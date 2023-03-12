﻿using BepInEx;
using UnboundLib;
using UnboundLib.Cards;
using Stands.Cards;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;

namespace Stands
{
    // These are the mods required for our mod to work
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    // Declares our mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]
    // The game our mod is associated with
    [BepInProcess("Rounds.exe")]
    public class Stands : BaseUnityPlugin
    {
        public static Stands instance { get; private set; }

        public const string ModInitials = "STAND";
        public const string Version = "0.0.1"; // What version are we on (major.minor.patch)?
        
        private const string ModName = "Stands";
        private const string ModId = "com.Mechanolith.rounds.Stands";


        void Awake()
        {
            // Use this to call any harmony patch files your mod may have
            var harmony = new Harmony(ModId);
            harmony.PatchAll();

            instance = this;
        }
        void Start()
        {
            CustomCard.BuildCard<Hamon>();
        }

        public static void Debug(string message)
        {
            if (UnityEngine.Debug.isDebugBuild)
            {
                UnityEngine.Debug.Log(message);
            }
        }
    }

}