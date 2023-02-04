using HarmonyLib;
using System.Threading.Tasks;
using UnityEngine;

namespace NoSmokeStayLit.Patches
{
    internal class KeepFueledPatch
    {
        //    //This code provided in Fuel Eternal by Marfinator  all credit
        //  used here with permission.

        [HarmonyPatch]
        class Fireplace_UpdateFireplace_Patch
        {
            [HarmonyPatch(typeof(Fireplace))]
            [HarmonyPatch("UpdateFireplace")]
            [HarmonyPrefix]
            static void Fireplace_UpdateFireplace(Fireplace __instance, ref ZNetView ___m_nview)
            {
                if (Configs.ConfigCheck(__instance.name))
                    ___m_nview.GetZDO().Set("fuel", __instance.m_maxFuel);
            }
        }


        [HarmonyPatch]
        internal class CookingStation_SetFuel_Patch
        {
            [HarmonyPatch(typeof(CookingStation))]
            [HarmonyPatch("SetFuel")]
            [HarmonyPrefix]
            private static void CookingStation_SetFuel(CookingStation __instance, ref float fuel)
            {
                if (Configs.ConfigCheck(__instance.name))
                    fuel = __instance.m_maxFuel;
                //NoSmokeStayLit.TastyUtilsLogger.LogInfo(fuel);
            }
        }

        [HarmonyPatch]
        internal class Smelter_SetFuel_Patch
        {
            [HarmonyPatch(typeof(Smelter))]
            [HarmonyPatch("SetFuel")]
            [HarmonyPrefix]
            private static void Smelter_SetFuel(Smelter __instance, ref float fuel)
            {
                if (Configs.ConfigCheck(__instance.name))
                    fuel = __instance.m_maxFuel;
            }
        }

        [HarmonyPatch]
        internal class CookingStation_Awake_Patch
        {
            [HarmonyPatch(typeof(CookingStation))]
            [HarmonyPatch("Awake")]
            [HarmonyPostfix]
            private static void CookingStation_Awake(CookingStation __instance, ref ZNetView ___m_nview)
            {
                if (!___m_nview.isActiveAndEnabled || Player.m_localPlayer == null || Player.m_localPlayer.IsTeleporting())
                    return;

                if (Configs.ConfigCheck(__instance.name))
                    Refuel(___m_nview);
            }
        }

        [HarmonyPatch]
        internal class Smelter_Awake_Patch
        {
            [HarmonyPatch(typeof(Smelter))]
            [HarmonyPatch("Awake")]
            [HarmonyPostfix]
            private static void Smelter_Awake(Smelter __instance, ref ZNetView ___m_nview)
            {
                if (!___m_nview.isActiveAndEnabled || Player.m_localPlayer == null || Player.m_localPlayer.IsTeleporting())
                    return;


                if (Configs.ConfigCheck(__instance.name))
                    Refuel(___m_nview);

            }
        }

        public static async void Refuel(ZNetView znview)
        {
            await Task.Delay(33);
            znview.InvokeRPC("AddFuel");
        }
}
}