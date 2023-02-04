using HarmonyLib;

namespace NoSmokeStayLit.Patches
{
    internal class NoSmokePatch
    {
        //This takes away the blockedSmoke check so smelters can be stacked.
        [HarmonyPatch(typeof(Smelter), "UpdateSmoke")]
        private class SmelterUpdateSmoke_Patch
        {
            private static void Postfix(Smelter __instance)

            {
                if (Configs.ConfigCheckGiveMeSmoke(__instance.name))
                {
                    if (__instance.m_smokeSpawner != null)
                    {
                        //checks to see if stack smelters is true and kills off the smoke blocked check

                        __instance.m_smokeSpawner.enabled = true;
                        //__instance.m_blockedSmoke = true;
                        return;
                    }
                }
                else

                {
                    if (__instance.m_smokeSpawner != null)
                    {
                        //checks to see if stack smelters is true and kills off the smoke blocked check

                        __instance.m_smokeSpawner.enabled = false;
                        __instance.m_blockedSmoke = false;
                        return;
                    }
                }
            }
        }
    }
}