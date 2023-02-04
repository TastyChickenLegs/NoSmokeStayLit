using HarmonyLib;
using System;
using System.Reflection;
using UnityEngine;



namespace NoSmokeStayLit.Patches
{
    [HarmonyPatch(typeof(Fireplace), nameof(Fireplace.IsBurning))]
    internal class FireplaceIsBurning_Patch
    {
        private static void Postfix(Fireplace __instance, ref bool __result,
                 ref GameObject ___m_enabledObjectHigh, ref ZNetView ___m_nview)
        {
            float dayFraction = (float)typeof(EnvMan).GetField("m_smoothDayFraction",
                     BindingFlags.Instance | BindingFlags.NonPublic).GetValue(EnvMan.instance);

            bool shouldBeLit = true;
            __result = true;
       
            //checks the fuel level and if fuel is configured.  If so and there is no fuel it turns off the torch

            if ((int)Math.Ceiling(__instance.GetComponent<ZNetView>().GetZDO().GetFloat("fuel")) == 0 && !Configs.ConfigCheck(__instance.name))

            {
                __result = false;
                return;
            }

            if (Configs.ConfigCheckTimerOne(__instance.name))
            {
                //NoSmokeStayLit.TastyUtilsLogger.LogInfo(Configs.ConfigCheckTimerOne(__instance.name));
                EnvSetup currentEnvironment = EnvMan.instance.GetCurrentEnvironment();
                bool isAlwaysDarkBiome = currentEnvironment != null && currentEnvironment.m_alwaysDark;
                if (NoSmokeStayLitPlugin.timerOffFloatTime > NoSmokeStayLitPlugin.timerOnFloatTime)
                {
                    //NoSmokeStayLit.TastyUtilsLogger.LogInfo(NoSmokeStayLit.timerOffFloatTime);
                    //NoSmokeStayLit.TastyUtilsLogger.LogInfo(NoSmokeStayLit.timerOnFloatTime);
                   
                    if ((dayFraction <= NoSmokeStayLitPlugin.timerOnFloatTime && dayFraction >= 1f) || dayFraction >= NoSmokeStayLitPlugin.timerOffFloatTime)
                    {

                        if (!shouldBeLit || (isAlwaysDarkBiome && Configs.configAlwaysOnInDarkBiomes.Value))
                        {
                            __result = true;
                            return;
                        }
                        shouldBeLit = false;
                        __result = false;
                    }
                }
                else if (dayFraction <= NoSmokeStayLitPlugin.timerOnFloatTime && dayFraction >= NoSmokeStayLitPlugin.timerOffFloatTime)
                {
                    //NoSmokeStayLit.TastyUtilsLogger.LogInfo(dayFraction);
                    
                    if (!shouldBeLit || (isAlwaysDarkBiome && Configs.configAlwaysOnInDarkBiomes.Value))
                    {
                        __result = true;
                        return;
                    }
                    shouldBeLit = false;
                    __result = false;
                }


            }

            if (Configs.ConfigCheckGiveMeSmoke(__instance.name))
            {
                if (__instance.m_smokeSpawner != null)
                    __instance.m_smokeSpawner.enabled = true;
                if (__instance.name.StartsWith("piece_brazier"))
                    Utils.FindChild(__instance.transform, "SmokeSpawner").gameObject.
                                        GetComponent<SmokeSpawner>().enabled = true;
                if (Configs.keepOnInRain.Value) __instance.m_wet = false;
                return;
            }
            else
            {
                if (__instance.m_smokeSpawner != null)

                {  //check if wet and keep on in rain is true
                   //turns off the smoke and keeps the fire lit in the rain
                    __instance.m_smokeSpawner.enabled = false;
                    if (Configs.keepOnInRain.Value) __instance.m_wet = false;
                    return;
                }
                //checks if brazier and stops the smoke, keeps it lit in the rain and returns true

                if (__instance.name.StartsWith("piece_brazier"))
                {
                    Utils.FindChild(__instance.transform, "SmokeSpawner").gameObject.
                        GetComponent<SmokeSpawner>().enabled = false;
                    //check if wet and keep on in rain is true
                    if (Configs.keepOnInRain.Value) __instance.m_wet = false;
                    return;
                }

                if (Configs.keepOnInRain.Value) __instance.m_wet = false;
                return;
            }
        }
    }
}