using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using NoSmokeStayLit.Patches;
using ServerSync;
using System;
using System.IO;
using UnityEngine;

namespace NoSmokeStayLit
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class NoSmokeStayLit : BaseUnityPlugin
    {
        public static NoSmokeStayLit context;
        private readonly Harmony harmony = new Harmony("tastychickenlegs.NoSmokeStayLit");
        internal const string ModName = "NoSmokeStayLit";
        internal const string ModVersion = "2.1.0";
        internal const string Author = "tastychickenlegs";
        private const string ModGUID = Author + "." + ModName;
        private static string ConfigFileName = ModGUID + ".cfg";
        private static string ConfigFileFullPath = Paths.ConfigPath + Path.DirectorySeparatorChar + ConfigFileName;
        internal static string ConnectionError = "";
        public static bool configVerifyClient => _configVerifyClient.Value;
        private readonly Harmony _harmony = new(ModGUID);

        public static readonly ManualLogSource TastyUtilsLogger =
            BepInEx.Logging.Logger.CreateLogSource(ModName);
        public static float timerOnFloatTime;
        public static float timerOffFloatTime;
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
            _serverConfigLocked = config("", "Lock Configuration", Toggle.On,
                "If on, the configuration is locked and can be changed by server admins only.");
            _ = ConfigSync.AddLockingConfigEntry(_serverConfigLocked);

            _configEnabled = config("Basic Settings", "Mod Enabled", true, "Sets the mod to be enabled or not.");

            if (_configEnabled.Value)
            {
                //_configNexusID = config("Basic", "NexusID", 2027, "Nexus mod ID for 'Nexus Update Check' mod compatibility.");
                _configVerifyClient = config("Basic Settings", "Verify Clients", false, "Enable this to turn on the client verification and version checks.");
            }
           
            //Generate the Configs
            Configs.Generate();
            GogetTime();
            _harmony.PatchAll();
            
            SetupWatcher();
            
        }
        private void Update()
        {

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
                NoSmokeStayLit.TastyUtilsLogger.LogDebug("ReadConfigValues called");
                
                Config.Reload();
                GogetTime();
            }
            catch
            {
                NoSmokeStayLit.TastyUtilsLogger.LogError($"There was an issue loading your {ConfigFileName}");
                NoSmokeStayLit.TastyUtilsLogger.LogError("Please check your config entries for spelling and format!");
            }
        }

        #region ConfigOptions

        private static ConfigEntry<Toggle> _serverConfigLocked = null!;
        //private static ConfigEntry<int> _configNexusID;
        private static ConfigEntry<bool> _configEnabled;
        private static ConfigEntry<bool> _configVerifyClient;

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

        #endregion ConfigOptions

        [HarmonyPatch(typeof(Terminal), "InputText")]
        private static class InputText_Patch
        {
            private static bool Prefix(Terminal __instance)
            {
                if (!_configEnabled.Value)
                    return true;
                string text = __instance.m_input.text;
                if (text.ToLower().Equals($"{typeof(NoSmokeStayLit).Namespace.ToLower()} reset"))
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
            NoSmokeStayLit.TastyUtilsLogger.LogDebug(timerValOnHours);

            float timerValOnMins = Convert.ToSingle(Configs.timerOnMinutes.Value);
            timerValOnMins = (timerValOnMins / 60) / 24;
            NoSmokeStayLit.TastyUtilsLogger.LogDebug(timerValOnMins);

            timerOnFloatTime = (timerValOnMins + timerValOnHours);
            NoSmokeStayLit.TastyUtilsLogger.LogDebug(timerOnFloatTime);

            float timerValOffHours = Convert.ToSingle(Configs.timerOffHours.Value);
            timerValOffHours = timerValOffHours / 24;
            NoSmokeStayLit.TastyUtilsLogger.LogDebug(timerValOffHours);

            float timerValOffMins = Convert.ToSingle(Configs.timerOffMinutes.Value);
            timerValOffMins = (timerValOffMins / 60) / 24;
            NoSmokeStayLit.TastyUtilsLogger.LogDebug(timerValOffMins);

            timerOffFloatTime = (timerValOffMins + timerValOffHours);
            NoSmokeStayLit.TastyUtilsLogger.LogDebug(timerOffFloatTime);
        }
    }
}