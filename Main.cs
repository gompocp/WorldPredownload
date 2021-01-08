using Harmony;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;
using WorldPredownload.Components;
using WorldPredownload.UI;

namespace WorldPredownload
{
    public static class ModInfo
    {
        public const string Name = "WorldPredownload";
        public const string Author = "gompo";
        public const string Version = "1.3.5";
        public const string DownloadLink = "https://github.com/gompocp/WorldPredownload/releases";
    }

    public class WorldPredownload : MelonMod
    {
        private static MelonMod Instance;
        public static HarmonyInstance HarmonyInstance => Instance.harmonyInstance;

        public override void OnApplicationStart()
        {
            Instance = this;
            ModSettings.RegisterSettings();
            ModSettings.Apply();
            ClassInjector.RegisterTypeInIl2Cpp<EnableDisableListener>();
            SetupSocialMenuPatch.Patch();
        }

        public override void VRChat_OnUiManagerInit()
        {
            GameObject.Find("UserInterface/QuickMenu/NotificationInteractMenu").AddComponent<EnableDisableListener>().OnEnabled += delegate { InviteButton.UpdateText(); };
            InviteButton.Setup();
            FriendButton.Setup();
            WorldButton.Setup();
            WorldDownloadStatus.Setup();
            HudIcon.Setup();
        }

        public override void OnModSettingsApplied() => ModSettings.Apply();
        
    }
}