using HarmonyLib;
using System;
using System.Collections.Generic;

namespace NoSmokeStayLit
{
    [HarmonyPatch(typeof(ZNet), nameof(ZNet.OnNewConnection))]
    public static class RegisterAndCheckVersion

    {
        private static void Prefix(ZNetPeer peer, ref ZNet __instance)
        {
            if (NoSmokeStayLitMain.configVerifyClient)
            // Register version check call
            {
                NoSmokeStayLitMain.TastyUtilsLogger.LogDebug("Registering version RPC handler");
                peer.m_rpc.Register($"{NoSmokeStayLitMain.ModName}_VersionCheck",
                    new Action<ZRpc, ZPackage>(RpcHandlers.RPC_ServerSyncModTemplate_Version));

                // Make calls to check versions
                NoSmokeStayLitMain.TastyUtilsLogger.LogInfo("Invoking version check");
                ZPackage zpackage = new();
                zpackage.Write(NoSmokeStayLitMain.ModVersion);
                peer.m_rpc.Invoke($"{NoSmokeStayLitMain.ModName}_VersionCheck", zpackage);
            }
        }
    }

    [HarmonyPatch(typeof(ZNet), nameof(ZNet.RPC_PeerInfo))]
    public static class VerifyClient
    {
        private static bool Prefix(ZRpc rpc, ZPackage pkg, ref ZNet __instance)
        {
            if (NoSmokeStayLitMain.configVerifyClient)
            {
                if (!__instance.IsServer() || RpcHandlers.ValidatedPeers.Contains(rpc)) return true;
                // Disconnect peer if they didn't send mod version at all
                NoSmokeStayLitMain.TastyUtilsLogger.LogWarning(
                    $"Peer ({rpc.m_socket.GetHostName()}) never sent version or couldn't due to previous disconnect, disconnecting");
                rpc.Invoke("Error", 3);
                return false; // Prevent calling underlying method
            }
            return true;
        }

        private static void Postfix(ZNet __instance)
        {
            if (NoSmokeStayLitMain.configVerifyClient)
            {
                ZRoutedRpc.instance.InvokeRoutedRPC(ZRoutedRpc.instance.GetServerPeerID(), "RequestAdminSync",
                new ZPackage());
            }
        }
    }

    [HarmonyPatch(typeof(FejdStartup), nameof(FejdStartup.ShowConnectError))]
    public class ShowConnectionError
    {
        private static void Postfix(FejdStartup __instance)

        {
            if (NoSmokeStayLitMain.configVerifyClient)
            {
                if (__instance.m_connectionFailedPanel.activeSelf)
                {
                    __instance.m_connectionFailedError.resizeTextMaxSize = 25;
                    __instance.m_connectionFailedError.resizeTextMinSize = 15;
                    __instance.m_connectionFailedError.text += "\n" + NoSmokeStayLitMain.ConnectionError;
                }
            }
        }
    }

    [HarmonyPatch(typeof(ZNet), nameof(ZNet.Disconnect))]
    public static class RemoveDisconnectedPeerFromVerified
    {
        private static void Prefix(ZNetPeer peer, ref ZNet __instance)

        {
            if (NoSmokeStayLitMain.configVerifyClient)
            {
                if (!__instance.IsServer()) return;
                // Remove peer from validated list
                NoSmokeStayLitMain.TastyUtilsLogger.LogInfo(
                    $"Peer ({peer.m_rpc.m_socket.GetHostName()}) disconnected, removing from validated list");
                _ = RpcHandlers.ValidatedPeers.Remove(peer.m_rpc);
            }
        }
    }

    public static class RpcHandlers
    {
        public static readonly List<ZRpc> ValidatedPeers = new();

        public static void RPC_ServerSyncModTemplate_Version(ZRpc rpc, ZPackage pkg)
        {
            if (NoSmokeStayLitMain.configVerifyClient)
            {
                string? version = pkg.ReadString();
                NoSmokeStayLitMain.TastyUtilsLogger.LogInfo("Version check, local: " +
                                                                                NoSmokeStayLitMain.ModVersion +
                                                                                ",  remote: " + version);
                if (version != NoSmokeStayLitMain.ModVersion)
                {
                    NoSmokeStayLitMain.ConnectionError =
                        $"{NoSmokeStayLitMain.ModName} Installed: {NoSmokeStayLitMain.ModVersion}\n Needed: {version}";
                    if (!ZNet.instance.IsServer()) return;
                    // Different versions - force disconnect client from server
                    NoSmokeStayLitMain.TastyUtilsLogger.LogWarning(
                        $"Peer ({rpc.m_socket.GetHostName()}) has incompatible version, disconnecting");
                    rpc.Invoke("Error", 3);
                }
                else
                {
                    if (!ZNet.instance.IsServer())
                    {
                        // Enable mod on client if versions match
                        NoSmokeStayLitMain.TastyUtilsLogger.LogInfo(
                            "Received same version from server!");
                    }
                    else
                    {
                        // Add client to validated list
                        NoSmokeStayLitMain.TastyUtilsLogger.LogInfo(
                            $"Adding peer ({rpc.m_socket.GetHostName()}) to validated list");
                        ValidatedPeers.Add(rpc);
                    }
                }
            }
        }
    }
}