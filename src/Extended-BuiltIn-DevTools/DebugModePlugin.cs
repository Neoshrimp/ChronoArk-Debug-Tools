using BepInEx;
using BepInEx.Configuration;
using GameDataEditor;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Extended_BuiltIn_DevTools
{
    [BepInPlugin(GUID, "Extended debug mode", version)]
    [BepInProcess("ChronoArk.exe")]
    public class DebugModePlugin : BaseUnityPlugin
    {
        public const string GUID = "org.neo.chronoark.debugtools.debugmode";
        public const string version = "0.1.0";


        private static readonly Harmony harmony = new Harmony(GUID);

        private static ConfigEntry<KeyCode> debugLoadKey;
        private static ConfigEntry<KeyCode> debugSaveKey;
        private static ConfigEntry<bool> enableDebug;
        private static ConfigEntry<KeyCode> reloadGdataKey;


        private static BepInEx.Logging.ManualLogSource logger;

        void Awake()
        {

            logger = Logger;

            debugLoadKey = Config.Bind("Keybinds",
                "debugLoadKey",
                KeyCode.F4,
                "Opens debug load interface. Currently doesn't work in main menu (UIManager is not instantiated)");
            debugSaveKey = Config.Bind("Keybinds",
                "debugSaveKey",
                KeyCode.F5,
                "Debug save. Save only while on stage map. Saves made in battle, ark, menu might not behave as expected.");
            reloadGdataKey = Config.Bind("Keybinds",
                "reloadGdataKey",
                KeyCode.F8,
                "Reinitializes and reloads gdata json.");

            enableDebug = Config.Bind("Debug",
                "debugEnabled",
                true,
                "enables/disables debug mod");

            harmony.PatchAll();
        }
        void OnDestroy()
        {
            if (harmony != null)
                harmony.UnpatchAll(GUID);
        }


        //enable in-game developer debug mode
        [HarmonyPatch(typeof(SaveManager), "Awake")]
        class Debug_mode_Enable_Patch
        {
            static void Postfix(ref bool ___DebugMode)
            {
                logger.LogInfo("debug mode before: " + ___DebugMode);
                if (enableDebug.Value)
                    ___DebugMode = true;
                logger.LogInfo("debug mode after: " + ___DebugMode);

            }
        }

        [HarmonyPatch(typeof(StageSystem), nameof(StageSystem.CheatChack))]
        class Extra_Debug_Reward_Patch
        {
            static void Postfix(StageSystem __instance)
            {
                string cheatChat = PlayData.CheatChat;
                switch (cheatChat)
                {
                    // gives skillbooks
                    case "sb1":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        //white skillbook
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter),
                        //healing 101 book
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookSuport),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookSuport),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookSuport),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookSuport),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookSuport),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookSuport),
                        //red skillbook
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter_Rare),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter_Rare),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter_Rare),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter_Rare),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter_Rare),
                        //unimplemented purple skillbook for learning random Lucy's skill presumably 
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookLucy),
                        });
                        break;
                }
            }
        }

        [HarmonyPatch(typeof(BattleSystem))]
        class BattleSystem_Patch
        {
            [HarmonyPatch(nameof(BattleSystem.CheatChack))]
            [HarmonyPostfix]
            static void CheatChackPostfix(BattleSystem __instance)
            {
                string cheatChat = __instance.CheatChat;
                switch (cheatChat)
                {
                    case "fbend": // ends battle instantly but doesn't go through proper end of battle code
                        __instance.CheatEnabled();
                        __instance.BattleEnd();
                        break;
                    case "bend": // better way to end battle but doesn't clear enemies
                        __instance.CheatEnabled();
                        __instance.ClearEnabled = true;
                        __instance.StartCoroutine(typeof(BattleSystem).GetMethod("ClearBattle", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(__instance, null) as IEnumerator);
                        break;

                }
            }
        }

        [HarmonyPatch(typeof(UIManager), "Update")]
        class UIManager_Patch
        {
            static void Postfix()
            {
                if (SaveManager.savemanager != null)
                {
                    if (Input.GetKeyDown(debugLoadKey.Value) && SaveManager.savemanager.DebugMode)
                    {
                        UIManager.InstantiateActive(UIManager.inst.DebugLoad);
                    }
                    if (Input.GetKeyDown(debugSaveKey.Value) && SaveManager.savemanager.DebugMode)
                    {
                        try
                        {
                            SaveManager.savemanager.ProgressOneSaveDebug();
                            logger.LogInfo("Saved!");
                        }
                        catch
                        {
                            logger.LogInfo("no save");
                        }
                    }

                    if (Input.GetKeyDown(reloadGdataKey.Value) && SaveManager.savemanager.DebugMode)
                    {
                        Debug.Log("Reloading gdata");
                        GDEDataManager.Init("gdata", false);
                    }

                }
                else
                {
                    logger.LogInfo("savemanager is null");
                }
            }
        }


        [HarmonyPatch(typeof(CharStatV3), "Update")]
        class RemoveManaRestriction
        {
            static void Postfix(CharStatV3 __instance)
            {
                if (SaveManager.savemanager != null && SaveManager.savemanager.DebugMode)
                {
                    if (PlayData.MPUpgradeNum[PlayData.TSavedata.SoulUpgrade.AP] <= PlayData.Soul && BattleSystem.instance == null)
                    {
                        __instance.UpgradeButtons[0].interactable = true;
                        __instance.MPTooltip.enabled = false;
                    }
                }
            }
        }

    }
}
