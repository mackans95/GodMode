using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using Pathea.ActorNs;
using UnityEngine;
using System.Reflection;

namespace GodMode
{
    [BepInPlugin("MarGus.GodMode", "GodMode Mod", "1.0.0")]
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
            isDebug = Config.Bind("General", "IsDebug", true, "Enable debug logs");

            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);

            Dbgl("GodMode Mod Loaded!");
        }

        // Patch Player.Update to fill health and stamina every frame
        [HarmonyPatch(typeof(Player), "Update")]
        static class Player_Update_Patch
        {
            static void Prefix(Player __instance)
            {
                if (!modEnabled.Value || __instance.actor == null)
                    return;

                // === Fill HP ===
                float currentHp = __instance.actor.GetAttr(ActorRunTimeAttrType.Hp);
                float maxHp = __instance.actor.GetAttr(ActorAttrType.HpMax);

                if (currentHp < maxHp)
                {
                    float diffHp = maxHp - currentHp;
                    __instance.actor.ApplyAttrChange(ActorRunTimeAttrType.Hp, diffHp);
                    __instance.actor.ShowHpChangeUI(diffHp);
                    Dbgl($"Refilled {diffHp} HP to Player. Current HP: {__instance.actor.GetAttr(ActorRunTimeAttrType.Hp)}");
                }

                // === Fill Stamina ===
                float currentSp = __instance.actor.GetAttr(ActorRunTimeAttrType.Sp);
                float maxSp = __instance.actor.GetAttr(ActorAttrType.SpMax);

                if (currentSp < maxSp)
                {
                    float diffSp = maxSp - currentSp;
                    __instance.actor.ApplyAttrChange(ActorRunTimeAttrType.Sp, diffSp);
                    __instance.actor.ShowHpChangeUI(diffSp); // You can optionally make a separate UI for stamina
                    Dbgl($"Refilled {diffSp} Stamina to Player. Current SP: {__instance.actor.GetAttr(ActorRunTimeAttrType.Sp)}");
                }
            }
        }
    }
}