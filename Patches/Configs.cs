using BepInEx.Configuration;
using System.Linq;
using UnityEngine;


namespace NoSmokeStayLit.Patches;

internal class Configs
{
    public static ConfigEntry<bool> fe_fire_pit;
    private static ConfigEntry<bool> fe_bonfire;
    private static ConfigEntry<bool> fe_hearth;
    private static ConfigEntry<bool> fe_piece_walltorch;
    private static ConfigEntry<bool> fe_piece_groundtorch;
    private static ConfigEntry<bool> fe_piece_groundtorch_wood;
    private static ConfigEntry<bool> fe_piece_groundtorch_green;
    private static ConfigEntry<bool> fe_piece_groundtorch_blue;
    private static ConfigEntry<bool> fe_piece_brazierfloor01;
    private static ConfigEntry<bool> fe_piece_brazierceiling01;
    private static ConfigEntry<bool> fe_piece_jackoturnip;
    private static ConfigEntry<bool> fe_piece_oven;
    private static ConfigEntry<bool> fe_smelter;
    private static ConfigEntry<bool> fe_blastfurnace;
    private static ConfigEntry<bool> fe_eitrrefinery;
    private static ConfigEntry<bool> fe_piece_bathtub;
    private static ConfigEntry<string> fe_custom_instance;
    public static ConfigEntry<float> configOnTimeOne;
    public static ConfigEntry<float> configOffTimeOne;
   // public static ConfigEntry<string> toggleString;
   // public static ConfigEntry<KeyboardShortcut> toggleKey;
    public static ConfigEntry<bool> configAlwaysOnInDarkBiomes;
    public static ConfigEntry<bool> keepOnInRain;

    public static ConfigEntry<bool> fe_piece_walltorch_timer;
    public static ConfigEntry<bool> fe_piece_groundtorch_timer;
    public static ConfigEntry<bool> fe_piece_groundtorch_wood_timer;
    public static ConfigEntry<bool> fe_piece_groundtorch_green_timer;
    public static ConfigEntry<bool> fe_piece_groundtorch_blue_timer;
    public static ConfigEntry<bool> fe_piece_brazierfloor01_timer;
    public static ConfigEntry<bool> fe_piece_brazierceiling01_timer;
    public static ConfigEntry<bool> fe_piece_jackoturnip_timer;
    private static ConfigEntry<string> fe_custom_instance_timer;
    public static ConfigEntry<int> timerOnMinutes;
    public static ConfigEntry<int> timerOnHours;
    public static ConfigEntry<int> timerOffMinutes;
    public static ConfigEntry<int> timerOffHours;
    public static float timerOnFloatTime;
    public static float timerOffFloatTime;

    public static ConfigEntry<bool> fe_fire_pit_smoke;
    private static ConfigEntry<bool> fe_hearth_smoke;
    private static ConfigEntry<bool> fe_piece_brazierfloor01_smoke;
    private static ConfigEntry<bool> fe_piece_brazierceiling01_smoke;
    private static ConfigEntry<bool> fe_smelter_smoke;
    internal static ConfigEntry<bool> fe_oven;


    //public static float lastFuel;
    //public static int fuelCount;

    internal static void Generate()
    {
        //A portion of this code provided in Fuel Eternal by Markinator  all credit
        //    //This code provided in Fuel Eternal by Markinator  all credit
        //  used here with permission.
        //Items//
        fe_fire_pit = NoSmokeStayLitPlugin.context.config("Fire Things Keep Lit", "Camp Fire Keep Lit", true, "Allow eternal fuel for Campfire");
        fe_bonfire = NoSmokeStayLitPlugin.context.config("Fire Things Keep Lit", "Bonfire Keep Lit", true, "Allow eternal fuel for Bonfire");
        fe_hearth = NoSmokeStayLitPlugin.context.config("Fire Things Keep Lit", "Hearth Keep Lit", true, "Allow eternal fuel for Hearth");
        fe_piece_walltorch = NoSmokeStayLitPlugin.context.config("Fire Things Keep Lit", "Wall Torch Keep Lit", true, "Allow eternal fuel for Sconce");
        fe_piece_groundtorch = NoSmokeStayLitPlugin.context.config("Fire Things Keep Lit", "Iron Torch Keep Lit", true, "Allow eternal fuel for Standing iron torch");
        fe_piece_groundtorch_wood = NoSmokeStayLitPlugin.context.config("Fire Things Keep Lit", "Wood Torch Keep Lit", true, "Allow eternal fuel for Standing wood torch");
        fe_piece_groundtorch_green = NoSmokeStayLitPlugin.context.config("Fire Things Keep Lit", "Green Torch Keep Lit", true, "Allow eternal fuel for Standing green-burning iron torch");
        fe_piece_groundtorch_blue = NoSmokeStayLitPlugin.context.config("Fire Things Keep Lit", "Blue Torch Keep Lit", true, "Allow eternal fuel for Standing blue-burning iron torch");
        fe_piece_brazierfloor01 = NoSmokeStayLitPlugin.context.config("Fire Things Keep Lit", "Standing Brazier Keep Lit", true, "Allow eternal fuel for Standing brazier");
        fe_piece_brazierceiling01 = NoSmokeStayLitPlugin.context.config("Fire Things Keep Lit", "Ceiling Brazier Keep Lit", true, "Allow eternal fuel for Hanging brazier");
        fe_piece_jackoturnip = NoSmokeStayLitPlugin.context.config("Fire Things Keep Lit", "JackOTurnip Keep Lit", true, "Allow eternal fuel for Jack-o-turnip");
        fe_piece_oven = NoSmokeStayLitPlugin.context.config("Smelters Keep Lit", "Stone Oven Keep Fueled", true, "Allow eternal fuel for Stone oven");
        fe_piece_bathtub = NoSmokeStayLitPlugin.context.config("Smelters Keep Lit", "HotTub Keep Fueled", true, "Allow eternal fuel for Hot tub");
        fe_smelter = NoSmokeStayLitPlugin.context.config("Smelters Keep Lit", "Smelter Keep Fueled", false, "Allow eternal fuel for Smelter");
        fe_blastfurnace = NoSmokeStayLitPlugin.context.config("Smelters Keep Lit", "BlastFurnace Keep Fueled", false, "Allow eternal fuel for Blast furnace");
        fe_eitrrefinery = NoSmokeStayLitPlugin.context.config("Smelters Keep Lit", "EitrRefinery Keep Fueled", false, "Allow eternal fuel for Eitr refinery");
        fe_custom_instance = NoSmokeStayLitPlugin.context.config("Custom", "Custom Items Keep Lit", "", "Enable Fuel Eternal to manage fuel for custom items added by other mods, " +
            "comma-separated no spaces (e.g. \"rk_campfire,rk_hearth,rk_brazier\" )");
       
        fe_piece_walltorch_timer = NoSmokeStayLitPlugin.context.config("Timed Torches", "Wall Torch on Timer", true, "Allow timer fuel for Sconce");
        fe_piece_groundtorch_timer = NoSmokeStayLitPlugin.context.config("Timed Torches", "Iron Torch On Timer", true, "Allow timer for Standing iron torch");
        fe_piece_groundtorch_wood_timer = NoSmokeStayLitPlugin.context.config("Timed Torches", "Wood Torch on Timer", true, "Allow timer for Standing wood torch");
        fe_piece_groundtorch_green_timer = NoSmokeStayLitPlugin.context.config("Timed Torches", "Green Torch on Timer", true, "Allow timer for Standing green-burning iron torch");
        fe_piece_groundtorch_blue_timer = NoSmokeStayLitPlugin.context.config("Timed Torches", "Blue Torch on Timer", true, "Allow timer for Standing blue-burning iron torch");
        fe_piece_brazierfloor01_timer = NoSmokeStayLitPlugin.context.config("Timed Torches", "Standing Brazier on Timer", true, "Allow timer for Standing brazier");
        fe_piece_brazierceiling01_timer = NoSmokeStayLitPlugin.context.config("Timed Torches", "Ceiling Brazier on Timer", true, "Allow timer for Hanging brazier");
        fe_piece_jackoturnip_timer = NoSmokeStayLitPlugin.context.config("Timed Torches", "Jackoturnip on Timer", true, "Allow timer for Jack-o-turnip");
        fe_custom_instance_timer = NoSmokeStayLitPlugin.context.config("Custom", "Custom Items on Timers", "", "Enable Timers for items added by other mods, " +
            "comma-separated no spaces (e.g. \"rk_campfire,rk_hearth,rk_brazier\" )");
        keepOnInRain = NoSmokeStayLitPlugin.context.config<bool>("Basic Settings", "Keep on when wet", true, "Keep fires lit even when raining and wet");
        //Timer Settings//
        timerOnHours = NoSmokeStayLitPlugin.context.config("Timer Settings", "Timer On Hours (Night Time)", 17, 
            new ConfigDescription("Time to Turn on at night in 24 hour time.  Example 7pm is 19 hours.", 
            new AcceptableValueRange<int>(0, 24), null, new ConfigurationManagerAttributes { Order = 12 }));
        timerOnMinutes = NoSmokeStayLitPlugin.context.config("Timer Settings", "Timer On Mins (Night Time)", 0, 
            new ConfigDescription("Minutes in the hour.  This will be added to the hours above.", 
            new AcceptableValueRange<int>(0, 60),null,new ConfigurationManagerAttributes { Order = 11}));
        timerOffHours = NoSmokeStayLitPlugin.context.config("Timer Settings", "Timer Off Hours (Day Time)", 5, 
            new ConfigDescription("Time to Turn off the monring in 24 hour time.  Example 7pm is 19 hours.", 
            new AcceptableValueRange<int>(0, 24), null, new ConfigurationManagerAttributes { Order = 10 }));
        timerOffMinutes = NoSmokeStayLitPlugin.context.config("Timer Settings", "Timer Off Mins (Day Time)", 30, 
            new ConfigDescription("Minutes in the hour.  This will be added to the hours above.", 
            new AcceptableValueRange<int>(0, 60), null, new ConfigurationManagerAttributes { Order = 9 }));
        fe_fire_pit_smoke = NoSmokeStayLitPlugin.context.config("Really, I Want Smoke", "Camp Fire Enable Smoke", false, "Enable eternal Smoke for Bonfire");
        fe_hearth_smoke = NoSmokeStayLitPlugin.context.config("Really, I Want Smoke", "Hearth Enable Smoke", false, "Enable Smoke for Hearth");
        fe_piece_brazierfloor01_smoke = NoSmokeStayLitPlugin.context.config("Really, I Want Smoke", "Standing Brazier Enable Smoke", false, "Enable for Standing brazier");
        fe_piece_brazierceiling01_smoke = NoSmokeStayLitPlugin.context.config("Really, I Want Smoke", "Ceiling Brazier Enable Smoke", false, "Enable timer for Hanging brazier");
        fe_smelter_smoke = NoSmokeStayLitPlugin.context.config("Really, I Want Smoke", "Smelter Enable Smoke", false, "Enable Smoke for Smelter.  This disables Smelter Stacking");
        fe_oven = NoSmokeStayLitPlugin.context.config<bool>("Really, I Want Smoke", "Oven Enable Smoke", false, "Enable Smoke for Cooking Station.");

        configAlwaysOnInDarkBiomes = NoSmokeStayLitPlugin.context.config("Basic Settings", "Always On In Dark Biomes", true, 
            "If true, torches will always burn in areas that Valheim considers 'always dark'." +
            " E.g Mistlands or any biome during a storm");
    }

    public static bool ConfigCheck(string instanceName)
    {
        bool EternalFuel = false;
        switch (instanceName)
        {
            case "fire_pit(Clone)":
                EternalFuel = fe_fire_pit.Value;
                break;

            case "bonfire(Clone)":
                EternalFuel = fe_bonfire.Value;
                break;

            case "hearth(Clone)":
                EternalFuel = fe_hearth.Value;
                break;

            case "piece_walltorch(Clone)":
                EternalFuel = fe_piece_walltorch.Value;
                break;

            case "piece_groundtorch(Clone)":
                EternalFuel = fe_piece_groundtorch.Value;
                break;

            case "piece_groundtorch_wood(Clone)":
                EternalFuel = fe_piece_groundtorch_wood.Value;
                break;

            case "piece_groundtorch_green(Clone)":
                EternalFuel = fe_piece_groundtorch_green.Value;
                break;

            case "piece_groundtorch_blue(Clone)":
                EternalFuel = fe_piece_groundtorch_blue.Value;
                break;

            case "piece_brazierfloor01(Clone)":
                EternalFuel = fe_piece_brazierfloor01.Value;
                break;

            case "piece_brazierceiling01(Clone)":
                EternalFuel = fe_piece_brazierceiling01.Value;
                break;

            case "piece_jackoturnip(Clone)":
                EternalFuel = fe_piece_jackoturnip.Value;
                break;

            case "piece_oven(Clone)":
                EternalFuel = fe_piece_oven.Value;
                break;

            case "smelter(Clone)":
                EternalFuel = fe_smelter.Value;
                break;

            case "blastfurnace(Clone)":
                EternalFuel = fe_blastfurnace.Value;
                break;

            case "eitrrefinery(Clone)":
                EternalFuel = fe_eitrrefinery.Value;
                break;

            case "piece_bathtub(Clone)":
                EternalFuel = fe_piece_bathtub.Value;
                break;
        }
        if (fe_custom_instance.Value.Split(',').Contains(instanceName.Remove(instanceName.Length - 7)))
            EternalFuel = true;
        return EternalFuel;
    }

    public static bool ConfigCheckTimerOne(string instanceName)
    {
        bool TimerOne = false;
        switch (instanceName)
        {
            case "piece_walltorch(Clone)":
                TimerOne = fe_piece_walltorch_timer.Value;
                break;

            case "piece_groundtorch(Clone)":
                TimerOne = fe_piece_groundtorch_timer.Value;
                break;

            case "piece_groundtorch_wood(Clone)":
                TimerOne = fe_piece_groundtorch_wood_timer.Value;
                break;

            case "piece_groundtorch_green(Clone)":
                TimerOne = fe_piece_groundtorch_green_timer.Value;
                break;

            case "piece_groundtorch_blue(Clone)":
                TimerOne = fe_piece_groundtorch_blue_timer.Value;
                break;

            case "piece_brazierfloor01(Clone)":
                TimerOne = fe_piece_brazierfloor01_timer.Value;
                break;

            case "piece_brazierceiling01(Clone)":
                TimerOne = fe_piece_brazierceiling01_timer.Value;
                break;

            case "piece_jackoturnip(Clone)":
                TimerOne = fe_piece_jackoturnip_timer.Value;
                break;

        }
        if (fe_custom_instance_timer.Value.Split(',').Contains(instanceName.Remove(instanceName.Length - 7)))
            TimerOne = true;
        return TimerOne;
    }
    public static bool ConfigCheckGiveMeSmoke(string instanceName)
    {
        bool GiveMeSmoke = false;
        switch (instanceName)
        {
            case "fire_pit(Clone)":
                GiveMeSmoke = fe_fire_pit_smoke.Value;
                break;

            case "hearth(Clone)":
                GiveMeSmoke = fe_hearth_smoke.Value;
                break;

            case "piece_brazierfloor01(Clone)":
                GiveMeSmoke = fe_piece_brazierfloor01_smoke.Value;
                break;

            case "piece_brazierceiling01(Clone)":
                GiveMeSmoke = fe_piece_brazierceiling01_smoke.Value;
                break;

            case "smelter(Clone)":
                GiveMeSmoke = fe_smelter_smoke.Value;
                break;

            case "piece_oven(Clone)":
                GiveMeSmoke = fe_oven.Value;
                break;

        }
        return GiveMeSmoke;
    
    }
}