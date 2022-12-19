using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using ServerSync;
using System;
using System.IO;
using System.Linq;
using UnityEngine;

namespace NoSmokeStayLit
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class NoSmokeStayLit : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("tastychickenlegs.NoSmokeStayLit");
        internal const string ModName = "NoSmokeStayLit";
        internal const string ModVersion = "1.1.5";
        internal const string Author = "tastychickenlegs";
        private const string ModGUID = Author + "." + ModName;
        private static string ConfigFileName = ModGUID + ".cfg";
        private static string ConfigFileFullPath = Paths.ConfigPath + Path.DirectorySeparatorChar + ConfigFileName;
        internal static string ConnectionError = "";
        public static bool configVerifyClient => _configVerifyClient.Value;
        private readonly Harmony _harmony = new(ModGUID);

        public static readonly ManualLogSource ServerSyncModTemplateLogger =
            BepInEx.Logging.Logger.CreateLogSource(ModName);

        private static readonly ConfigSync ConfigSync = new(ModGUID)
        { DisplayName = ModName, CurrentVersion = ModVersion, MinimumRequiredVersion = ModVersion };

        public enum Toggle
        {
            On = 1,
            Off = 0
        }

        private static string[] affectedSources = new string[]
        {
            "piece_brazierfloor01",
            "piece_brazierceiling01",
            "fire_pit",
            "hearth",
            "bonfire"
        };

        public void Awake()
        {
            _serverConfigLocked = config("1 - General", "Lock Configuration", Toggle.On,
                "If on, the configuration is locked and can be changed by server admins only.");
            _ = ConfigSync.AddLockingConfigEntry(_serverConfigLocked);

            _configEnabled = config("General", "Mod Enabled", true, "Sets the mod to be enabled or not.");

            if (_configEnabled.Value)
            {
                _configNexusID = config("General", "NexusID", 2027, "Nexus mod ID for 'Nexus Update Check' mod compatibility.");
                _configVerifyClient = config("General", "Verify Clients", false, "Enable this to turn on the client verification and version checks.");
                _configNoFuelNeeded = config("Fuel", "NoFuelNeeded", true, "If true, will keep fire lit and not require fuel)");
                _configAffectedSources = config("General", "AffectedFireplaceSources", string.Join(",", affectedSources), "List of 'Fireplace' sources to be affected by the mod. By default firepit and hearth are turned on.");
                affectedSources = _configAffectedSources.Value.Split(',');
                _configStackSmelters = config("General", "Stack Smelters", true, "Removes most of the smoke and the blocking check so smelters and kilns can be stacked.");
            }

            _harmony.PatchAll();
            SetupWatcher();
        }

        private void OnDestroy()
        {
            Config.Save();
            _harmony.UnpatchSelf();
        }

        private void SetupWatcher()
        {
            FileSystemWatcher watcher = new(Paths.ConfigPath, ConfigFileName);
            watcher.Changed += ReadConfigValues;
            watcher.Created += ReadConfigValues;
            watcher.Renamed += ReadConfigValues;
            watcher.IncludeSubdirectories = true;
            watcher.SynchronizingObject = ThreadingHelper.SynchronizingObject;
            watcher.EnableRaisingEvents = true;
        }

        private void ReadConfigValues(object sender, FileSystemEventArgs e)
        {
            if (!File.Exists(ConfigFileFullPath)) return;
            try
            {
                ServerSyncModTemplateLogger.LogDebug("ReadConfigValues called");
                Config.Reload();
            }
            catch
            {
                ServerSyncModTemplateLogger.LogError($"There was an issue loading your {ConfigFileName}");
                ServerSyncModTemplateLogger.LogError("Please check your config entries for spelling and format!");
            }
        }

        #region ConfigOptions

        private static ConfigEntry<Toggle> _serverConfigLocked = null!;
        private static ConfigEntry<int> _configNexusID;
        private static ConfigEntry<bool> _configEnabled;
        private static ConfigEntry<string> _configAffectedSources;
        private static ConfigEntry<bool> _configNoFuelNeeded;
        private static ConfigEntry<bool> _configStackSmelters;
        private static ConfigEntry<bool> _configVerifyClient;

        private ConfigEntry<T> config<T>(string group, string name, T value, ConfigDescription description,
            bool synchronizedSetting = true)
        {
            ConfigDescription extendedDescription =
                new(
                    description.Description +
                    (synchronizedSetting ? " [Synced with Server]" : " [Not Synced with Server]"),
                    description.AcceptableValues, description.Tags);
            ConfigEntry<T> configEntry = Config.Bind(group, name, value, extendedDescription);
            //var configEntry = Config.Bind(group, name, value, description);

            SyncedConfigEntry<T> syncedConfigEntry = ConfigSync.AddConfigEntry(configEntry);
            syncedConfigEntry.SynchronizedConfig = synchronizedSetting;

            return configEntry;
        }

        private ConfigEntry<T> config<T>(string group, string name, T value, string description,
            bool synchronizedSetting = true)
        {
            return config(group, name, value, new ConfigDescription(description), synchronizedSetting);
        }

        private class ConfigurationManagerAttributes
        {
            public bool? Browsable = false;
        }

        private class AcceptableShortcuts : AcceptableValueBase
        {
            public AcceptableShortcuts() : base(typeof(KeyboardShortcut))
            {
            }

            public override object Clamp(object value) => value;

            public override bool IsValid(object value) => true;

            public override string ToDescriptionString() =>
                "# Acceptable values: " + string.Join(", ", KeyboardShortcut.AllKeyCodes);
        }

        #endregion ConfigOptions

        //checks to see if items use fuel and configures the interface accordingly
        [HarmonyPatch(typeof(Fireplace), nameof(Fireplace.GetHoverText))]
        private class FireplaceGetHoverText_Patch
        {
            private static void Postfix(Fireplace __instance, ref string __result, ref ZNetView ___m_nview, ref string ___m_name)
            {
                if (affectedSources.Contains(Utils.GetPrefabName(__instance.gameObject)))
                {
                    if (_configNoFuelNeeded.Value)

                    {
                        __result = Localization.instance.Localize(___m_name + "\n <color=yellow>No Fuel Required</color>" + "\n NoSmoke StayLit");
                    }
                }
            }
        }

        [HarmonyPatch(typeof(Fireplace), nameof(Fireplace.Interact))]
        private class FireplaceInteract_Patch
        {
            private static bool Prefix(Fireplace __instance, ref bool __result)
            {
                if (affectedSources.Contains(Utils.GetPrefabName(__instance.gameObject)))
                {
                    if (_configNoFuelNeeded.Value)
                    {
                        __result = false;
                        return false;
                    }
                    else
                    {
                        __result = true;
                        return true;
                    }
                }
                __result = true;
                return true;
            }
        }

        [HarmonyPatch(typeof(Fireplace), nameof(Fireplace.IsBurning))]
        private class FireplaceIsBurning_Patch
        {
            private static void Postfix(Fireplace __instance, ref bool __result, ref GameObject ___m_enabledObjectHigh, ref ZNetView ___m_nview)
            {
                if (affectedSources.Contains(Utils.GetPrefabName(__instance.gameObject)))
                {
                    if (_configNoFuelNeeded.Value)
                    {
                        //torches don't have smoke. Checking for null so no errors but still want to turn off smoke for braziers
                        if (__instance.m_smokeSpawner != null)
                        {
                            //turns off the smoke and keeps the fire lit in the rain
                            __instance.m_smokeSpawner.enabled = false;
                            __instance.m_wet = false;
                            __result = true;

                            return;
                        }
                        // sets the wet check false so the fire will stay lit in the rain
                        __instance.m_wet = false;

                        //Searches the parent item and gets the child to set the Smokespawner false.  Thank you Azumatt you are a God//

                        Utils.FindChild(__instance.transform, "SmokeSpawner").gameObject.GetComponent<SmokeSpawner>().enabled = false;
                        __result = true;
                        return;
                    }
                    //checks the fuel level and if fuel is configured.  If so and there is no fuel it turns off the torch
                    if ((int)Math.Ceiling(__instance.GetComponent<ZNetView>().GetZDO().GetFloat("fuel")) == 0 && !_configNoFuelNeeded.Value)

                    {
                        __result = false;
                        return;
                    }
                    //torches don't have smoke. Checking for null so no errors but still want to turn off smoke for braziers and firepits
                    if (__instance.m_smokeSpawner != null)
                    {
                        __instance.m_wet = false;
                        __instance.m_smokeSpawner.enabled = false;
                        __result = true;
                        return;
                    }
                    else
                    {
                        //Searches the parent item and gets the child to set the Smokespawner false.  Thank you Azumatt you are a God//
                        Utils.FindChild(__instance.transform, "SmokeSpawner").gameObject.GetComponent<SmokeSpawner>().enabled = false;
                        __instance.m_wet = false;
                        __result = true;
                        return;
                    }
                }
            }
        }

        //-----------------------------------------------------------------
        //This takes away the blockedSmoke check so smelters can be stacked.
        //------------------------------------------------------------------
        [HarmonyPatch(typeof(Smelter), "UpdateSmoke")]
        private class SmelterUpdateSmoke_Patch
        {
            private static void Postfix(Smelter __instance)

            {
                if (__instance.m_smokeSpawner != null)
                {
                    //checks to see if stack smelters is true and kills off the smoke blocked check
                    if (_configStackSmelters.Value)
                    {
                        __instance.m_smokeSpawner.enabled = false;
                        __instance.m_blockedSmoke = false;
                        return;
                    }
                }
               
            }
        }
    }
}