using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Pathea.ActorNs;
using System.Reflection;
using UnityEngine;

namespace InvincibilityMod
{
    [BepInPlugin("yourname.Invincibility", "Invincibility Mod", "1.0.0")]
    public class BepInExPlugin : BaseUnityPlugin
    {
        private static BepInExPlugin context;

        public static ConfigEntry<bool> modEnabled;
        public static ConfigEntry<bool> isDebug;

        public static void Dbgl(string str = "", LogLevel logLevel = LogLevel.Debug)
        {
            if (isDebug.Value)
                context.Logger.Log(logLevel, str);
        }

        private void Awake()
        {
            context = this;

            modEnabled = Config.Bind("General", "Enabled", true, "Enable this mod");
            isDebug = Config.Bind("General", "IsDebug", false, "Enable debug logs");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);

            Dbgl("Invincibility Mod Loaded");
        }

        // PATCH: Prevent HP from decreasing
        [HarmonyPatch(typeof(Actor), "ApplyAttrChange")]
        static class Actor_ApplyAttrChange_Patch
        {
            static bool Prefix(Actor __instance, ActorRunTimeAttrType type, ref float value)
            {
                if (!modEnabled.Value)
                    return true;

                // Only care about HP damage
                if (type == ActorRunTimeAttrType.Hp && value < 0)
                {
                    // Check if this actor belongs to the player
                    if (__instance == Player.Self?.actor)
                    {
                        Dbgl($"Blocked damage: {value}");
                        return false; // cancel damage
                    }
                }

                return true;
            }
        }
    }
}