using System;
using System.IO;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using ServerSync;
using UnityEngine;
using NoSmokeStayLit.Patches;

namespace NoSmokeStayLit
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class NoSmokeStayLitPlugin : BaseUnityPlugin
    {
        internal const string ModName = "NoSmokeStayLit";
        internal const string ModVersion = "2.2.6";
        internal const string Author = "TastyChickenLegs";
        private const string ModGUID = Author + "." + ModName;
        private static string ConfigFileName = ModGUID + ".cfg";
        private static string ConfigFileFullPath = Paths.ConfigPath + Path.DirectorySeparatorChar + ConfigFileName;
        public static bool configVerifyClient => _configVerifyClient.Value;
        
        public static NoSmokeStayLitPlugin context;
        private static ConfigEntry<Toggle> _serverConfigLocked = null!;
        private static ConfigEntry<bool> _configEnabled;
        private static ConfigEntry<bool> _configVerifyClient;
        public static readonly ManualLogSource TastyUtilsLogger =
            BepInEx.Logging.Logger.CreateLogSource(ModName);
        public static float timerOnFloatTime;
        public static float timerOffFloatTime;

        internal static string ConnectionError = "";

        private readonly Harmony harmony = new(ModGUID);

    

        private static readonly ConfigSync ConfigSync = new(ModGUID)
            { DisplayName = ModName, CurrentVersion = ModVersion, MinimumRequiredVersion = ModVersion };

        public enum Toggle
        {
            On = 1,
            Off = 0
        }

        public void Awake()
        {
            context = this;
            _serverConfigLocked = config("1 - General", "Lock Configuration", Toggle.On,
                "If on, the configuration is locked and can be changed by server admins only.");
            _ = ConfigSync.AddLockingConfigEntry(_serverConfigLocked);
            _configVerifyClient = config("Basic Settings", "Verify Clients", true, "Enable this to turn on the client verification and version checks.");
            _configEnabled = config("Basic Settings", "Mod Enabled", true, "Sets the mod to be enabled or not.");

            if (!_configEnabled.Value)
            {
                return;
            }

            //Generate the Configs
            Configs.Generate();
            GogetTime();
            harmony.PatchAll();
            SetupWatcher();
        }

        private void OnDestroy()
        {
            Config.Save();
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
                TastyUtilsLogger.LogDebug("ReadConfigValues called");
                Config.Reload();
                GogetTime();
            }
            catch
            {
                TastyUtilsLogger.LogError($"There was an issue loading your {ConfigFileName}");
                TastyUtilsLogger.LogError("Please check your config entries for spelling and format!");
            }
        }


        #region ConfigOptions

        

        internal ConfigEntry<T> config<T>(string group, string name, T value, ConfigDescription description,
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

        internal ConfigEntry<T> config<T>(string group, string name, T value, string description,
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

        #endregion
        [HarmonyPatch(typeof(Terminal), "InputText")]
        private static class InputText_Patch
        {
            private static bool Prefix(Terminal __instance)
            {
                if (!_configEnabled.Value)
                    return true;
                string text = __instance.m_input.text;
                if (text.ToLower().Equals($"{typeof(NoSmokeStayLitPlugin).Namespace.ToLower()} reset"))
                {
                    context.Config.Reload();
                    context.Config.Save();

                    __instance.AddString(text);
                    __instance.AddString($"{context.Info.Metadata.Name} config reloaded");
                    return false;
                }
                return true;
            }
        }
        private static void GogetTime()
        {
            float timerValOnHours = Convert.ToSingle(Configs.timerOnHours.Value);
            timerValOnHours = timerValOnHours / 24;
            TastyUtilsLogger.LogDebug(timerValOnHours);

            float timerValOnMins = Convert.ToSingle(Configs.timerOnMinutes.Value);
            timerValOnMins = (timerValOnMins / 60) / 24;
            TastyUtilsLogger.LogDebug(timerValOnMins);

            timerOnFloatTime = (timerValOnMins + timerValOnHours);
            TastyUtilsLogger.LogDebug(timerOnFloatTime);

            float timerValOffHours = Convert.ToSingle(Configs.timerOffHours.Value);
            timerValOffHours = timerValOffHours / 24;
            TastyUtilsLogger.LogDebug(timerValOffHours);

            float timerValOffMins = Convert.ToSingle(Configs.timerOffMinutes.Value);
            timerValOffMins = (timerValOffMins / 60) / 24;
            TastyUtilsLogger.LogDebug(timerValOffMins);

            timerOffFloatTime = (timerValOffMins + timerValOffHours);
            TastyUtilsLogger.LogDebug(timerOffFloatTime);
        }
    }
}