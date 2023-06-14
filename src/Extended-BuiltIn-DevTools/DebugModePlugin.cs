using BepInEx;
using BepInEx.Configuration;
using GameDataEditor;
using HarmonyLib;
using I2.Loc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public static bool rarelearn = false;

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
                    // gives regular skillbooks
                    case "sb0":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        //skillbook normal
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter),
                        });
                        break;

                    // gives special skillbooks
                    case "sb1":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        //infinite skillbook
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookInfinity),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookInfinity),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookInfinity),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookInfinity),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookInfinity),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookInfinity),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookInfinity),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookInfinity),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookInfinity),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookInfinity),
                        //healing 101 book
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
                        });
                        break;

                    // gives lucy skillbooks
                    case "sb2":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        //lucy skillbook
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookLucy),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookLucy),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookLucy),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookLucy),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookLucy),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookLucy),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookLucy),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookLucy),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookLucy),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookLucy),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookLucy),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookLucy),
                        });
                        break;
                    // all common skillbooks
                    case "sb3":
                        __instance.CheatEnabled();
                        var rewards = new List<ItemBase>();


                        PlayData._ALLSKILLLIST.FindAll(s => s.Category.Key == GDEItemKeys.SkillCategory_PublicSkill).ForEach(sd => rewards.Add(ItemBase.GetItem(sd)));
                        SaveManager.savemanager._NowData.unlockList.PublicSkillKey.ForEach(sk => rewards.Add(ItemBase.GetItem(new GDESkillData(sk))));

                        InventoryManager.Reward(rewards);

                        break;

                    //common equipment
                    case "c1":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        List <ItemBase> reward = new List<ItemBase>();
                        int count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Equip && item.ItemClassNum == 0)
                            {
                                reward.Add(item);
                                count++;
                            }
                            if (count == 16)
                            {
                                break;
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    case "c2":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Equip && item.ItemClassNum == 0)
                            {
                                count++;
                                if (count > 16)
                                {
                                    reward.Add(item);
                                }
                                if (count == 32)
                                {
                                    break;
                                }
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    case "c3":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Equip && item.ItemClassNum == 0)
                            {
                                count++;
                                if (count > 32)
                                {
                                    reward.Add(item);
                                }
                                if (count == 48)
                                {
                                    break;
                                }
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    //uncommon equipment
                    case "u1":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Equip && item.ItemClassNum == 1)
                            {
                                reward.Add(item);
                                count++;
                            }
                            if (count == 16)
                            {
                                break;
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    case "u2":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Equip && item.ItemClassNum == 1)
                            {
                                count++;
                                if (count > 16)
                                {
                                    reward.Add(item);
                                }
                                if (count == 32)
                                {
                                    break;
                                }
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    case "u3":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Equip && item.ItemClassNum == 1)
                            {
                                count++;
                                if (count > 32)
                                {
                                    reward.Add(item);
                                }
                                if (count == 48)
                                {
                                    break;
                                }
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    //rare equipment
                    case "r1":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Equip && item.ItemClassNum == 2)
                            {
                                reward.Add(item);
                                count++;
                            }
                            if (count == 16)
                            {
                                break;
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    case "r2":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Equip && item.ItemClassNum == 2)
                            {
                                count++;
                                if (count > 16)
                                {
                                    reward.Add(item);
                                }
                                if (count == 32)
                                {
                                    break;
                                }
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    case "r3":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Equip && item.ItemClassNum == 2)
                            {
                                count++;
                                if (count > 32)
                                {
                                    reward.Add(item);
                                }
                                if (count == 48)
                                {
                                    break;
                                }
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    //heroic equipment
                    case "h1":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Equip && item.ItemClassNum == 3)
                            {
                                reward.Add(item);
                                count++;
                            }
                            if (count == 16)
                            {
                                break;
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    case "h2":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Equip && item.ItemClassNum == 3)
                            {
                                count++;
                                if (count > 16)
                                {
                                    reward.Add(item);
                                }
                                if (count == 32)
                                {
                                    break;
                                }
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    case "h3":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Equip && item.ItemClassNum == 3)
                            {
                                count++;
                                if (count > 32)
                                {
                                    reward.Add(item);
                                }
                                if (count == 48)
                                {
                                    break;
                                }
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    case "h4":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Equip && item.ItemClassNum == 3)
                            {
                                count++;
                                if (count > 48)
                                {
                                    reward.Add(item);
                                }
                                if (count == 64)
                                {
                                    break;
                                }
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    case "h5":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Equip && item.ItemClassNum == 3)
                            {
                                count++;
                                if (count > 64)
                                {
                                    reward.Add(item);
                                }
                                if (count == 80)
                                {
                                    break;
                                }
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    //legendary equipment
                    case "l1":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Equip && item.ItemClassNum == 4)
                            {
                                reward.Add(item);
                                count++;
                            }
                            if (count == 16)
                            {
                                break;
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    case "l2":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Equip && item.ItemClassNum == 4)
                            {
                                count++;
                                if (count > 16)
                                {
                                    reward.Add(item);
                                }
                                if (count == 32)
                                {
                                    break;
                                }
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    case "l3":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Equip && item.ItemClassNum == 4)
                            {
                                count++;
                                if (count > 32)
                                {
                                    reward.Add(item);
                                }
                                if (count == 48)
                                {
                                    break;
                                }
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    case "l4":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Equip && item.ItemClassNum == 4)
                            {
                                count++;
                                if (count > 48)
                                {
                                    reward.Add(item);
                                }
                                if (count == 64)
                                {
                                    break;
                                }
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    case "l5":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Equip && item.ItemClassNum == 4)
                            {
                                count++;
                                if (count > 64)
                                {
                                    reward.Add(item);
                                }
                                if (count == 80)
                                {
                                    break;
                                }
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    //relic items
                    case "re1":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Passive)
                            {
                                count++;
                                reward.Add(item);
                            }
                            if (count == 16)
                            {
                                break;
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    //relic items 2
                    case "re2":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Passive)
                            {
                                count++;
                                if (count > 16)
                                {
                                    reward.Add(item);
                                }
                                if (count == 32)
                                {
                                    break;
                                }
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    //relic items 3
                    case "re3":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Passive)
                            {
                                count++;
                                if (count > 32)
                                {
                                    reward.Add(item);
                                }
                                if (count == 48)
                                {
                                    break;
                                }
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    case "re4":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Passive)
                            {
                                count++;
                                if (count > 48)
                                {
                                    reward.Add(item);
                                }
                                if (count == 64)
                                {
                                    break;
                                }
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    case "re5":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Passive)
                            {
                                count++;
                                if (count > 64)
                                {
                                    reward.Add(item);
                                }
                                if (count == 80)
                                {
                                    break;
                                }
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    case "re6":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Passive)
                            {
                                count++;
                                if (count > 80)
                                {
                                    reward.Add(item);
                                }
                                if (count == 96)
                                {
                                    break;
                                }
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    case "re7":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Passive)
                            {
                                count++;
                                if (count > 96)
                                {
                                    reward.Add(item);
                                }
                                if (count == 112)
                                {
                                    break;
                                }
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    case "re8":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Passive)
                            {
                                count++;
                                if (count > 112)
                                {
                                    reward.Add(item);
                                }
                                if (count == 128)
                                {
                                    break;
                                }
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    case "re9":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Passive)
                            {
                                count++;
                                if (count > 128)
                                {
                                    reward.Add(item);
                                }
                                //if (count == 144)
                                //{
                                //    break;
                                //}
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    //all potions
                    case "p1":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Potions)
                            {
                                reward.Add(item);
                                count++;
                            }
                            if (count == 20)
                            {
                                break;
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    case "p2":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Potions)
                            {
                                count++;
                                if (count > 20)
                                {
                                    reward.Add(item);
                                }
                                if (count == 40)
                                {
                                    break;
                                }
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    //all scrolls
                    case "s1":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Scroll)
                            {
                                reward.Add(item);
                                count++;
                            }
                            if (count == 20)
                            {
                                break;
                            }
                        }
                        // Item_Scroll contains 0 items now for some reason, emergency fix
                        reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Uncurse));
                        reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Enchant));
                        reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Identify));
                        reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Item));
                        reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Mapping));
                        reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Midas));
                        reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Purification));
                        reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Quick));
                        reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Teleport));
                        reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Transfer));
                        reward.Add(ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Vitality));
                        InventoryManager.Reward(reward);
                        break;

                    case "s2":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Scroll)
                            {
                                count++;
                                if (count > 20)
                                {
                                    reward.Add(item);
                                }
                                if (count == 40)
                                {
                                    break;
                                }
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    case "a1":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Active)
                            {
                                reward.Add(item);
                                count++;
                            }
                            if (count == 20)
                            {
                                break;
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    case "a2":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();

                        reward = new List<ItemBase>();
                        count = 0;
                        foreach (ItemBase item in PlayData.ALLITEMLIST)
                        {
                            if (item is Item_Active)
                            {
                                count++;
                                if (count > 20)
                                {
                                    reward.Add(item);
                                }
                                if (count == 40)
                                {
                                    break;
                                }
                            }
                        }
                        InventoryManager.Reward(reward);
                        break;

                    //everything else
                    case "misc":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_Herb),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SodaWater),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SmallBarrierMachine),
                        ItemBase.GetItem(GDEItemKeys.Item_Misc_Item_Key),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_Bread),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_GoldenBread),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_GoldenApple),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_TimeRelic),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_Celestial),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_ArtifactPouch),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_Ilya_PassiveConsume),
                        ItemBase.GetItem(GDEItemKeys.Item_Misc_BlackironMoru),
                        ItemBase.GetItem(GDEItemKeys.Item_Misc_ArtifactPlusInven),
                        });
                        break;

                    //gold + soulstones
                    case "gg":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        ItemBase.GetItem(GDEItemKeys.Item_Misc_Soul, 198),
                        ItemBase.GetItem(GDEItemKeys.Item_Misc_Gold, 99990),
                        });
                        break;

                    //Dodo Masks
                    case "dodo":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RabbitMask),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RabbitMask),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RabbitMask),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RabbitMask),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RabbitMask),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RabbitMask),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RabbitMask),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RabbitMask),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RabbitMask),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RabbitMask),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RabbitMask),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RabbitMask),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RabbitMask),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RabbitMask),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RabbitMask),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RabbitMask),
                        });
                        break;

                    //Replica
                    case "rep":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Replica),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Replica),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Replica),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Replica),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Replica),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Replica),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Replica),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Replica),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Replica),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Replica),
                        });
                        break;

                    case "star":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_Celestial),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_Celestial),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_Celestial),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_Celestial),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_Celestial),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_Celestial),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_Celestial),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_Celestial),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_Celestial),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_Celestial),
                        });
                        break;

                    //keys
                    case "key":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                            ItemBase.GetItem(GDEItemKeys.Item_Misc_Item_Key, 1),
                        });
                        break;

                    case "shield":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                            ItemBase.GetItem(GDEItemKeys.Item_Consume_SmallBarrierMachine, 8),
                        });
                        break;
                    
                    //Crimson Wilderness Enter Item
                    case "???":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                            ItemBase.GetItem(GDEItemKeys.Item_Misc_RWEnterItem),
                        });
                        break;

                    //game 3x speed
                    case "2x":
                        // clear input buffer
                        __instance.CheatEnabled();
                        ToggleTimeScale();
                        break;

                    //game 3x speed
                    case "x2":
                        __instance.CheatEnabled();
                        ToggleTimeScale();
                        break;

                    // summoning bosses
                    case "living":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        FieldSystem.instance.BattleStart(new GDEEnemyQueueData("Queue_MBoss_0"), StageSystem.instance.StageData.BattleMap.Key, false, false, "", "", false);
                        break;

                    case "cerberus":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        FieldSystem.instance.BattleStart(new GDEEnemyQueueData("Garden_Midboss"), StageSystem.instance.StageData.BattleMap.Key, false, false, "", "", false);
                        break;

                    case "golem":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        FieldSystem.instance.BattleStart(new GDEEnemyQueueData("Queue_Golem"), StageSystem.instance.StageData.BattleMap.Key, false, false, "", "", false);
                        break;

                    case "witch":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        FieldSystem.instance.BattleStart(new GDEEnemyQueueData("Queue_Witch"), StageSystem.instance.StageData.BattleMap.Key, false, false, "", "", false);
                        break;

                    case "dorchi":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        FieldSystem.instance.BattleStart(new GDEEnemyQueueData("Queue_DorchiX"), StageSystem.instance.StageData.BattleMap.Key, false, false, "", "", false);
                        break;

                    case "joker":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        FieldSystem.instance.BattleStart(new GDEEnemyQueueData("Queue_S2_Joker"), StageSystem.instance.StageData.BattleMap.Key, false, false, "", "", false);
                        break;

                    case "parade":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        FieldSystem.instance.BattleStart(new GDEEnemyQueueData("Queue_MBoss2_0"), StageSystem.instance.StageData.BattleMap.Key, false, false, "", "", false);
                        break;

                    case "ruby":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        FieldSystem.instance.BattleStart(new GDEEnemyQueueData("Queue_S2_MainBoss_Luby"), StageSystem.instance.StageData.BattleMap.Key, false, false, "", "", false);
                        break;

                    case "time":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        FieldSystem.instance.BattleStart(new GDEEnemyQueueData("Queue_S2_TimeEater"), StageSystem.instance.StageData.BattleMap.Key, false, false, "", "", false);
                        break;

                    case "bomber":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        FieldSystem.instance.BattleStart(new GDEEnemyQueueData("Queue_S2_BombClown"), StageSystem.instance.StageData.BattleMap.Key, false, false, "", "", false);
                        break;

                    case "godo":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        FieldSystem.instance.BattleStart(new GDEEnemyQueueData("CrimsonQueue_GunManBoss"), StageSystem.instance.StageData.BattleMap.Key, false, false, "", "", false);
                        break;

                    case "reaper":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        FieldSystem.instance.BattleStart(new GDEEnemyQueueData("Queue_S3_Reaper"), StageSystem.instance.StageData.BattleMap.Key, false, false, "", "", false);
                        break;

                    case "karaela":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        FieldSystem.instance.BattleStart(new GDEEnemyQueueData("Queue_S3_TheLight"), StageSystem.instance.StageData.BattleMap.Key, false, false, "", "", false);
                        break;

                    case "pharos":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        FieldSystem.instance.BattleStart(new GDEEnemyQueueData("Queue_S3_PharosLeader"), StageSystem.instance.StageData.BattleMap.Key, false, false, "", "", false);
                        break;

                    case "tfk":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        FieldSystem.instance.BattleStart(new GDEEnemyQueueData("Queue_S4_King"), StageSystem.instance.StageData.BattleMap.Key, false, false, "", "", false);
                        break;

                    case "azar":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        FieldSystem.instance.BattleStart(new GDEEnemyQueueData("LBossFirst_Queue"), StageSystem.instance.StageData.BattleMap.Key, false, false, "", "", false);
                        break;

                    // allows golden skillbook to show all rare options
                    case "rare":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        rarelearn = true;
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        //red skillbook
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter_Rare),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_SkillBookCharacter_Rare),
                        });
                        break;

                    // turn off debug mode
                    case "dbof":
                        SaveManager.savemanager.DebugMode = false;
                        break;
                }
            }

            public static void ToggleTimeScale()
            {
                if (Time.timeScale < 2f)
                {
                    Time.timeScale = 3f;
                    logger.LogInfo($"timeScale = {Time.timeScale}");
                }
                else
                {
                    Time.timeScale = 1f;
                    logger.LogInfo($"timeScale = {Time.timeScale}");
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
                    case "asdf": // ends battle instantly but doesn't go through proper end of battle code
                        __instance.CheatEnabled();
                        __instance.BattleEnd();
                        break;
                    case "df": // combines "turn" and "draw" commands
                        __instance.CheatEnabled();
                        for (int j = 0; j < __instance.AllyTeam.Skills.Count; j++)
                        {
                            __instance.AllyTeam.Skills[j].Delete(false);
                            j--;
                        }
                        __instance.AllyTeam.Draw(7);
                        foreach (BattleAlly battleAlly2 in __instance.AllyList)
                        {
                            battleAlly2.ActionCount = 1;
                            battleAlly2.Overload = 0;
                        }
                        __instance.AllyTeam.AP = __instance.AllyTeam.MAXAP;
                        __instance.ActWindow.Window.GetSkillData(__instance.AllyTeam);
                        __instance.AllyTeam.DiscardCount = 1;
                        break;
                    case "die": // set enemy hp to 1
                        __instance.CheatEnabled();
                        using (List<BattleEnemy>.Enumerator enumerator2 = __instance.EnemyList.GetEnumerator())
                        {
                            while (enumerator2.MoveNext())
                            {
                                BattleEnemy battleEnemy5 = enumerator2.Current;
                                battleEnemy5.Info.Hp = 1;
                            }
                            return;
                        }
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
                        var initMethod = AccessTools.Method(typeof(GDEDataManager), nameof(GDEDataManager.Init), new Type[] { typeof(bool) });
                        initMethod.Invoke(null, new object[] { false });
                    }

                }
                else
                {
                    logger.LogInfo("savemanager is null");
                }
            }
        }

        // Start Ark at 3x speed
        [HarmonyPatch(typeof(ArkCode), "Start")]
        class TimeScale2xPatch
        {
            static void Postfix()
            {
                Time.timeScale = 3f;
                //Debug.Log("Sonic Speed");
            }
        }

        // Golden Skillbook show all options
        [HarmonyPatch(typeof(UseItem.SkillBookCharacter_Rare), "Use")]
        class RareOptions
        {
            [HarmonyPrefix]
            static bool Prefix(UseItem.SkillBookCharacter_Rare __instance, ref bool __result)
            {
                if (rarelearn)
                {
                    // Can learn infinite rare
                    PlayData.TSavedata.SpRule = new SpecialRule();
                    PlayData.TSavedata.SpRule.RuleChange.CharacterRareSkillInfinityGet = true;

                    List<Skill> list = new List<Skill>();
                    List<BattleAlly> battleallys = PlayData.Battleallys;
                    BattleTeam tempBattleTeam = PlayData.TempBattleTeam;
                    for (int i = 0; i < PlayData.TSavedata.Party.Count; i++)
                    {
                        bool flag = false;
                        if (PlayData.TSavedata.SpRule == null || !PlayData.TSavedata.SpRule.RuleChange.CharacterRareSkillInfinityGet)
                        {
                            using (List<CharInfoSkillData>.Enumerator enumerator = PlayData.TSavedata.Party[i].SkillDatas.GetEnumerator())
                            {
                                while (enumerator.MoveNext())
                                {
                                    if (enumerator.Current.Skill.Rare)
                                    {
                                        flag = true;
                                    }
                                }
                            }
                            if (PlayData.TSavedata.Party[i].BasicSkill.Rare)
                            {
                                flag = true;
                            }
                        }
                        if (!flag)
                        {
                            // Changed Here
                            List<GDESkillData> gdeskillData = PlayData.GetMySkills(PlayData.TSavedata.Party[i].KeyData, true).GroupBy(x => x.KeyID).Select(x => x.First()).ToList();
                            if (gdeskillData != null)
                            {
                                foreach (GDESkillData skill in gdeskillData)
                                {
                                    list.Add(Skill.TempSkill(skill.KeyID, battleallys[i], tempBattleTeam));
                                    Debug.Log(skill.Name);
                                }
                            }
                        }
                    }
                    if (list.Count == 0)
                    {
                        EffectView.SimpleTextout(FieldSystem.instance.TopWindow.transform, ScriptLocalization.System.CantRareSkill, 1f, false, 1f);
                        __result = false;
                    }
                    foreach (Skill skill in list)
                    {
                        if (!SaveManager.IsUnlock(skill.MySkill.KeyID, SaveManager.NowData.unlockList.SkillPreView))
                        {
                            SaveManager.NowData.unlockList.SkillPreView.Add(skill.MySkill.KeyID);
                        }
                    }
                    PlayData.TSavedata.UseItemKeys.Add(GDEItemKeys.Item_Consume_SkillBookCharacter_Rare);
                    FieldSystem.DelayInput(BattleSystem.I_OtherSkillSelect(list, new SkillButton.SkillClickDel(__instance.SkillAdd), ScriptLocalization.System_Item.SkillAdd, false, true, true, true, true));
                    __result = true;
                    return false;
                }
                else return true;
            }
        }


        //[HarmonyPatch(typeof(CharStatV3), "Update")]
        //class RemoveManaRestriction
        //{
        //    static void Postfix(CharStatV3 __instance)
        //    {
        //        if (SaveManager.savemanager != null && SaveManager.savemanager.DebugMode)
        //        {
        //            if (PlayData.MPUpgradeNum[PlayData.TSavedata.SoulUpgrade.AP] <= PlayData.Soul && BattleSystem.instance == null)
        //            {
        //                __instance.UpgradeButtons[0].interactable = true;
        //                __instance.MPTooltip.enabled = false;
        //            }
        //        }
        //    }
        //}

        //[HarmonyPatch(typeof(WaitButton))]
        //class WaitDisable_Patch
        //{
        //    [HarmonyPatch(nameof(WaitButton.WaitAct))]
        //    [HarmonyPrefix]
        //    static bool Prefix(BattleSystem __instance)
        //    {
        //        if (BattleSystem.instance.ActWindow.On && BattleSystem.instance.AllyTeam.WaitCount >= 1)
        //        {
        //            BattleSystem.instance.AllyTeam.WaitCount--;
        //            BattleSystem.instance.AllyTeam.TurnUseWaitNum++;
        //            BattleSystem.instance.AllyTeam.TurnActionNum++;
        //            foreach (IP_WaitButton ip_WaitButton in BattleSystem.instance.IReturn<IP_WaitButton>())
        //            {
        //                if (ip_WaitButton != null)
        //                {
        //                    ip_WaitButton.UseWaitButton();
        //                }
        //            }
        //            BattleSystem.instance.StartCoroutine(BattleSystem.instance.EnemyTurn(false));
        //        }
        //        return false;
        //    }
        //}

    }
}