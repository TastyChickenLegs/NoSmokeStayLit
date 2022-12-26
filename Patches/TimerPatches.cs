using HarmonyLib;
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

            if (Configs.ConfigCheckTimerOne(__instance.name))  
            {
                //NoSmokeStayLit.TastyUtilsLogger.LogInfo(Configs.ConfigCheckTimerOne(__instance.name));

                if (Configs.configOffTimeOne.Value < Configs.configOnTimeOne.Value)
                {
                    shouldBeLit = false;
                    __result = false;
                    return;
                }
                else if (dayFraction <= Configs.configOnTimeOne.Value && dayFraction >= Configs.configOffTimeOne.Value)
                {
                    NoSmokeStayLit.TastyUtilsLogger.LogInfo(dayFraction);
                    shouldBeLit = false;
                    __result = false;
                    return;
                }

                EnvSetup currentEnvironment = EnvMan.instance.GetCurrentEnvironment();
                bool isAlwaysDarkBiome = currentEnvironment != null && currentEnvironment.m_alwaysDark;

                if (shouldBeLit || (isAlwaysDarkBiome && Configs.configAlwaysOnInDarkBiomes.Value))
                {
                    
                    shouldBeLit = true;
                }
            }

            if (Configs.ConfigCheck(__instance.name) && shouldBeLit)
            {
                if (__instance.m_smokeSpawner != null)
                {
                    //turns off the smoke and keeps the fire lit in the rain
                    __instance.m_smokeSpawner.enabled = false;  
                   __result = true;
                    //check if wet and keep on in rain is true
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

                    __result = true;
                    return;
                }
                else  //then it must be  torch keep it dry and keep it lit
                {
                    __result = true;

                    //check if wet and keep on in rain is true
                    if (Configs.keepOnInRain.Value) __instance.m_wet = false;

                    return;
                }
            }
        }
    }
}