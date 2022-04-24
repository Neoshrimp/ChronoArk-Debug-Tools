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
                    // gives regular skillbooks
                    case "sb0":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        //skillbooks
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
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_GhostBadge),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RoseBadge),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_ShieldBadge),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_TargetBadge),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_OldBible),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_CubicRing),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Slipper),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_WoodenBat),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_WoodenSword),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_CubicNecklace),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Sunmoonstarcurse),
                        });
                        break;

                    //uncommon equipment
                    case "u1":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_SafetyPrayAmulet),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_LuckPrayAmulet),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_LongSword),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_PrayersHand),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_CeremonialGloves),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_NecklaceofLife),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RingofStupidman),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RingofBanalman),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RingofGambler),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RingofFugitive),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_LeavesBelt),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_SunCape),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_CrescentCape),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_LifeStoneRing),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_AmuletofAnger),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_AmuletofStability),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Rustydagger),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_StarRing),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_BastardSword),
                        });
                        break;

                    //rare equipment
                    case "r1":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_CharginTarge),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Ankh),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Taegeukring),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RabbitMask),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Bellofsalvation),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RustyHammer),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_ForestSword_0),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_CrossBrooch),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_SceptreofLife),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RingofMedia),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RingofDeath),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_BibleRevisedEdition),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RingofBlood),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_IronShield),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_EagleEye),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RingofHunt),
                        });
                        break;

                    //rare equipment
                    case "r2":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Rapier),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_EndlessScroll),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_ThrowingDagger),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RoseArmor),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_EnchantedRing),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_VikingsMace),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_WrathfulAxe),
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

                    //unique equipment
                    case "un1":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_FoxOrb),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Featheroflife),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_StickofFaith),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_MagicThread),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_ArmletofGambler),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Vadzerald),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_DochiHat),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_SweetPotato),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_SweetPotato_0),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_LastStand),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_ForbiddenLibram),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Alicesgift),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_FrozenShuriken),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_ScalesArmor),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_ForestSword),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Stellarhand),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_GuardsCertificate),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_DarkCross),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_SacredNecklace),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_HighPriestsLegacy),
                        });
                        break;

                    //unique equipment 2
                    case "un2":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RemialPaint),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_TheEquability),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_OrderofSacrifice),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_OrderofValiancy),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_HuntersNose),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_ReportersFootprint),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_ThePressure),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_OrderofHonor),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_CrescentsReflex),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_OrderofEgis),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_BloodyMary),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_PoisonousBottle),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Deadeye),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_VikingsBlood),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_DemonHunter),
                        });
                        break;

                    //legendary equipment
                    case "l1":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_StraightFlush),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_BlackSpikedArmor),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_WoodenSword13),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_DarkPrestClothes),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_HeartofIceSpirit),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_AgentSunglass),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_King_Armor),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_CrownofThorns),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_LostofDecedent),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_HolySwordKarsaga),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_HandprintofGrimReaper),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_DevilsHorn),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Arrowofangel),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_DolorousStroke),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_RingofAngel),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_MessiahbladesPrototype),
                        });
                        break;

                    //legendary equipment 2
                    case "l2":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_GasMask),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Revenger),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_BlackMoonSword),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_King_Sword),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_King_Cape),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_HalfMask),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_FlameShieldGenerator),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Analyticalscope),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_FlameDarkSword),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_SweetPotato_1),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_FoxOrb_0),
                        });
                        break;

                    //all potions
                    case "p1":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        ItemBase.GetItem(GDEItemKeys.Item_Potions_Potion_Fairy),
                        ItemBase.GetItem(GDEItemKeys.Item_Potions_Potion_heal),
                        ItemBase.GetItem(GDEItemKeys.Item_Potions_Potion_weak),
                        ItemBase.GetItem(GDEItemKeys.Item_Potions_Potion_holywater),
                        ItemBase.GetItem(GDEItemKeys.Item_Potions_Potion_Shield),
                        ItemBase.GetItem(GDEItemKeys.Item_Potions_Potion_Mana),
                        ItemBase.GetItem(GDEItemKeys.Item_Potions_Potion_Oblivion),
                        ItemBase.GetItem(GDEItemKeys.Item_Potions_Potion_Enegy),
                        ItemBase.GetItem(GDEItemKeys.Item_Potions_Potion_Rare),
                        ItemBase.GetItem(GDEItemKeys.Item_Potions_Potion_Battle),
                        ItemBase.GetItem(GDEItemKeys.Item_Potions_Potion_Healer),
                        ItemBase.GetItem(GDEItemKeys.Item_Potions_Potion_Tanker),
                        ItemBase.GetItem(GDEItemKeys.Item_Potions_Potion_Target),
                        ItemBase.GetItem(GDEItemKeys.Item_Potions_Potion_Clone),
                        ItemBase.GetItem(GDEItemKeys.Item_Potions_Potion_Cycle),
                        ItemBase.GetItem(GDEItemKeys.Item_Potions_Potion_Purification),
                        });
                        break;

                    //all scrolls
                    case "s1":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Enchant),
                        ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Identify),
                        ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Item),
                        ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Mapping),
                        ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Midas),
                        ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Purification),
                        ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Quick),
                        ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Teleport),
                        ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Transfer),
                        ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Uncurse),
                        ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Vitality),
                        });
                        break;

                    //all active items
                    case "a1":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        ItemBase.GetItem(GDEItemKeys.Item_Active_GoldenCoin),
                        ItemBase.GetItem(GDEItemKeys.Item_Active_Whetstone),
                        ItemBase.GetItem(GDEItemKeys.Item_Active_MagicCard),
                        ItemBase.GetItem(GDEItemKeys.Item_Active_LunchBox),
                        ItemBase.GetItem(GDEItemKeys.Item_Active_Vaccine),
                        ItemBase.GetItem(GDEItemKeys.Item_Active_DivineSword),
                        ItemBase.GetItem(GDEItemKeys.Item_Active_BlankCard),
                        ItemBase.GetItem(GDEItemKeys.Item_Active_PotLid),
                        ItemBase.GetItem(GDEItemKeys.Item_Active_Megaphone),
                        ItemBase.GetItem(GDEItemKeys.Item_Active_AssassinsTalisman),
                        });
                        break;

                    //relic items
                    case "re1":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_505Error),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_AncientShield),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_BlackMoon),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_blackRabbit),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_BlueRose),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_Bookofmoon),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_Bookofsun),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_BottleOfDemons),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_BrightShield),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_BronzeMotor),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_ChaosHourglass),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_CheeseCake),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_Crossoflight),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_CursedMask),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_DeliciousCarrot),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_EndlessSoul),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_FakeCrown),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_FrozenDebt),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_GolemRelic),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_HipSack),
                        });
                        break;

                    //relic items 2
                    case "re2":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_JokerCard),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_LastFlame),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_Librariansjournal),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_LostResearchJournal),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_MagicBerry),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_MagicLamp),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_ManaBattery),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_ManaBlaze),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_Memoryfragment),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_MindsEye),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_MistTotem),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_OldHourglass),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_OldRule),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_PotionBag),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_QuickCasting),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_RedBlossoms),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_SecretWreath),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_ShadowOrb),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_SharksFin),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_ShineStone),
                        });
                        break;

                    //relic items 3
                    case "re3":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_Sign),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_SixSixSix),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_SkeletonKey),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_Spinyblowfish),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_SurgeStone),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_TankRelic),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_ThornStem),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_TimeStorage),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_ToothBottle),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_Trailofmadness),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_TwinsRelic),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_Twistedlight),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_WarlockSkull),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_WeatherVane),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_WhiteMoon),
                        ItemBase.GetItem(GDEItemKeys.Item_Passive_WitchRelic),
                        });
                        break;

                    //a ton of vitality scrolls
                    case "jump":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Vitality),
                        ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Vitality),
                        ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Vitality),
                        ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Vitality),
                        ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Vitality),
                        ItemBase.GetItem(GDEItemKeys.Item_Scroll_Scroll_Quick),
                        });
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
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_Bread),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_GoldenBread),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_GoldenApple),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_TimeRelic),
                        ItemBase.GetItem(GDEItemKeys.Item_Equip_Sunmoonstarcurse_Quest),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_Celestial),
                        ItemBase.GetItem(GDEItemKeys.Item_Consume_ArtifactPouch),
                        ItemBase.GetItem(GDEItemKeys.Item_Active_ShadowPriest_Thurible),
                        });
                        break;

                    //gold + soulstones
                    case "god":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                        ItemBase.GetItem(GDEItemKeys.Item_Misc_Soul, 198),
                        ItemBase.GetItem(GDEItemKeys.Item_Misc_Gold, 99990),
                        });
                        break;
                       
                    //keys
                    case "key":
                        logger.LogInfo(cheatChat);
                        __instance.CheatEnabled();
                        InventoryManager.Reward(
                        new List<ItemBase>
                        {
                            ItemBase.GetItem(GDEItemKeys.Item_Misc_Item_Key, 99),
                        });
                        break;

                    //game 2x speed. short form of 'playtest'
                    case "2x":
                        // clear input buffer
                        __instance.CheatEnabled();
                        ToggleTimeScale();
                        break;
                        
                    //game 2x speed. short form of 'playtest'
                    case "x2":
                        __instance.CheatEnabled();
                        ToggleTimeScale();
                        break;
                }
            }

            public static void ToggleTimeScale()
            {
                if (Time.timeScale < 2f)
                {
                    Time.timeScale = 2f;
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
                    case "fbend": // ends battle instantly but doesn't go through proper end of battle code
                        __instance.CheatEnabled();
                        __instance.BattleEnd();
                        break;
                    case "bend": // better way to end battle but doesn't clear enemies
                        __instance.CheatEnabled();
                        __instance.ClearEnabled = true;
                        //__instance.StartCoroutine(typeof(BattleSystem).GetMethod("ClearBattle", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(__instance, null) as IEnumerator);
                        __instance.StartCoroutine((IEnumerator)AccessTools.Method(typeof(BattleSystem), "ClearBattle").Invoke(__instance, null));
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
