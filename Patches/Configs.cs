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
    //public static float lastFuel;
    //public static int fuelCount;

    internal static void Generate()
    {
        //A portion of this code provided in Fuel Eternal by Markinator  all credit
        //    //This code provided in Fuel Eternal by Markinator  all credit
        //  used here with permission.
        //Items//
        fe_fire_pit = NoSmokeStayLit.context.config("Fireplaces", "Camp Fire Keep Lit", true, "Allow eternal fuel for Campfire");
        fe_bonfire = NoSmokeStayLit.context.config("Fireplaces", "Bonfire Keep Lit", true, "Allow eternal fuel for Bonfire");
        fe_hearth = NoSmokeStayLit.context.config("Fireplaces", "Hearth Keep Lit", true, "Allow eternal fuel for Hearth");
        fe_piece_walltorch = NoSmokeStayLit.context.config("Fireplaces", "Wall Torch Keep Lit", true, "Allow eternal fuel for Sconce");
        fe_piece_groundtorch = NoSmokeStayLit.context.config("Fireplaces", "Iron Torch Keep Lit", true, "Allow eternal fuel for Standing iron torch");
        fe_piece_groundtorch_wood = NoSmokeStayLit.context.config("Fireplaces", "Wood Torch Keep Lit", true, "Allow eternal fuel for Standing wood torch");
        fe_piece_groundtorch_green = NoSmokeStayLit.context.config("Fireplaces", "Green Torch Keep Lit", true, "Allow eternal fuel for Standing green-burning iron torch");
        fe_piece_groundtorch_blue = NoSmokeStayLit.context.config("Fireplaces", "Blue Torch Keep Lit", true, "Allow eternal fuel for Standing blue-burning iron torch");
        fe_piece_brazierfloor01 = NoSmokeStayLit.context.config("Fireplaces", "Floor Brazier Keep Lit", true, "Allow eternal fuel for Standing brazier");
        fe_piece_brazierceiling01 = NoSmokeStayLit.context.config("Fireplaces", "Hanging Brazier Keep Lit", true, "Allow eternal fuel for Hanging brazier");
        fe_piece_jackoturnip = NoSmokeStayLit.context.config("Fireplaces", "JackOTurnip Keep Lit", true, "Allow eternal fuel for Jack-o-turnip");
        fe_piece_oven = NoSmokeStayLit.context.config("Smelters", "Stone Oven Keep Fueled", true, "Allow eternal fuel for Stone oven");
        fe_piece_bathtub = NoSmokeStayLit.context.config("Smelters", "HotTub Keep Fueled", true, "Allow eternal fuel for Hot tub");
        fe_smelter = NoSmokeStayLit.context.config("Smelters", "Smelter Keep Fueled", false, "Allow eternal fuel for Smelter");
        fe_blastfurnace = NoSmokeStayLit.context.config("Smelters", "BlastFurnace Keep Fueled", false, "Allow eternal fuel for Blast furnace");
        fe_eitrrefinery = NoSmokeStayLit.context.config("Smelters", "EitrRefinery Keep Fueled", false, "Allow eternal fuel for Eitr refinery");
        fe_custom_instance = NoSmokeStayLit.context.config("Custom", "Custom Items Keep Lit", "", "Enable Fuel Eternal to manage fuel for custom items added by other mods, " +
            "comma-separated no spaces (e.g. \"rk_campfire,rk_hearth,rk_brazier\" )");
      
        fe_piece_walltorch_timer = NoSmokeStayLit.context.config("Timers", "Wall Torch on Timer", true, "Allow timer fuel for Sconce");
        fe_piece_groundtorch_timer = NoSmokeStayLit.context.config("Timers", "Metal Torch On Timer", true, "Allow timer for Standing iron torch");
        fe_piece_groundtorch_wood_timer = NoSmokeStayLit.context.config("Timers", "Wood Torch on Timer", true, "Allow timer for Standing wood torch");
        fe_piece_groundtorch_green_timer = NoSmokeStayLit.context.config("Timers", "Green Torch on Timer", true, "Allow timer for Standing green-burning iron torch");
        fe_piece_groundtorch_blue_timer = NoSmokeStayLit.context.config("Timers", "Blue Torch on Timer", true, "Allow timer for Standing blue-burning iron torch");
        fe_piece_brazierfloor01_timer = NoSmokeStayLit.context.config("Timers", "Floor Brazier on Timer", true, "Allow timer for Standing brazier");
        fe_piece_brazierceiling01_timer = NoSmokeStayLit.context.config("Timers", "Ceiling Brazier on Timer", true, "Allow timer for Hanging brazier");
        fe_piece_jackoturnip_timer = NoSmokeStayLit.context.config("Timers", "Jackoturnip on Timer", true, "Allow timer for Jack-o-turnip");
        fe_custom_instance_timer = NoSmokeStayLit.context.config("Custom", "Custom Items on Timers", "", "Enable Timers for items added by other mods, " +
            "comma-separated no spaces (e.g. \"rk_campfire,rk_hearth,rk_brazier\" )");
        keepOnInRain = NoSmokeStayLit.context.config<bool>("Basic", "Keep on when wet", true, "Keep fires lit even when raining and wet");
        //Timer Settings//

        configOnTimeOne = NoSmokeStayLit.context.config("Timers", "On Time", 0.6875f, "To find time.(This is not exact but close enough)  Convert desired time to military time (24hr) and /24.  Example 6:30am is 6.30 hours /24 = .67");
        configOffTimeOne = NoSmokeStayLit.context.config("Timers", "Off Time", 0.27f, "To find time. (This is not exact but close enough) Convert desired time to military time (24hr) and /24.  Example 8:30pm is 16.30 hours /24 = .67");

        //General Torch Settings//

        configAlwaysOnInDarkBiomes = NoSmokeStayLit.context.config("Basic", "Always On In Dark Biomes", true, "If true, torches will always burn in areas that Valheim considers 'always dark'. E.g Mistlands or any biome during a storm");
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
}