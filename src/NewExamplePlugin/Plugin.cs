using BepInEx;
using BepInEx.Configuration;
using GameDataEditor;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;
using Debug = UnityEngine.Debug;


namespace NewExamplePlugin
{
    [BepInPlugin(GUID, "Example plugin", version)]
    [BepInProcess("ChronoArk.exe")]
    public class Plugin : BaseUnityPlugin
    {
        public const string GUID = "neo.exampleplugin";
        public const string version = "1.0.0";

        private static readonly Harmony harmony = new Harmony(GUID);

        internal static BepInEx.Logging.ManualLogSource logger;

        private void Awake()
        {
            logger = Logger;
            harmony.PatchAll();

            System.Action action = () => Logger.LogInfo("deeznuts");
            action.Invoke();

            var se1 = new Skill_Extended();
            var se2 = new Skill_Extended();
            
            Logger.LogInfo(se1 == se2);

        }

        private void OnDestroy()
        {
            if (harmony != null)
                harmony.UnpatchSelf();
        }


    }
}
