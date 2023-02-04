using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PrivilegeManager;

namespace NoSmokeStayLit.Patches
{
    internal class Interface_Patch
    {

        //checks to see if items use fuel and configures the interface accordingly
        [HarmonyPatch(typeof(Fireplace), nameof(Fireplace.GetHoverText))]
        private class FireplaceGetHoverText_Patch
        {
            private static void Postfix(Fireplace __instance, ref string __result, ref ZNetView ___m_nview, ref string ___m_name)
            {
                if  (Configs.ConfigCheck(__instance.name))
         

                    {
                        __result = Localization.instance.Localize(___m_name + "\n <color=yellow>" +
                            "No Fuel Required</color>" + "\n NoSmoke StayLit");
                    }
                
            }
        }

        [HarmonyPatch(typeof(Fireplace), nameof(Fireplace.Interact))]
        private class FireplaceInteract_Patch
        {
            private static bool Prefix(Fireplace __instance, ref bool __result)
            {
                if (Configs.ConfigCheck(__instance.name))
                {
             
                        __result = false;
                        return false;
                    
                }
                __result = true;
                return true;
            }
        }
    }
}
