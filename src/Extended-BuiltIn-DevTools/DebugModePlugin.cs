using BepInEx;
using BepInEx.Configuration;
using GameDataEditor;
using HarmonyLib;
using System.Collections.Generic;
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



        void Awake()
        {

            debugLoadKey = Config.Bind("Keybinds",
                "debugLoadKey",
                KeyCode.F6,
                "Opens debug load interface. Currently doesn't work in main menu (UIManager is not instantiated)");
            debugSaveKey = Config.Bind("Keybinds", 
                "debugSaveKey", 
                KeyCode.F5, 
                "Debug save. Save only while on stage map. Saves made in battle, ark, menu might not behave as expected.");

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
                UnityEngine.Debug.Log("debug mode before: " + ___DebugMode);
                if (enableDebug.Value)
                    ___DebugMode = true;
                UnityEngine.Debug.Log("debug mode after: " + ___DebugMode);

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
                        UnityEngine.Debug.Log(cheatChat);
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

        [HarmonyPatch(typeof(UIManager), "Update")]
        class UIManager_Patch
        {
            static void Postfix()
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
                        Debug.Log("Saved!");
                    }
                    catch
                    {
                        Debug.Log("no save");
                    }
                }
            }
        }


    }
}
