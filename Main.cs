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
        public const string Version = "1.3.6";
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
            ClassInjector.RegisterTypeInIl2Cpp<SelectedNotificationListener>();
            SetupSocialMenuPatch.Patch();
        }
        
        public override void VRChat_OnUiManagerInit()
        {
            GameObject.Find("UserInterface/QuickMenu/QuickModeMenus/QuickModeNotificationsMenu/ScrollRect/ViewPort/Content/NotificationUiPrefab/Row_NotificationActions").AddComponent<SelectedNotificationListener>();
            InviteButton.Setup();
            FriendButton.Setup();
            WorldButton.Setup();
            WorldDownloadStatus.Setup();
            HudIcon.Setup();
        }
        
        public override void OnPreferencesLoaded() => ModSettings.Apply();

        public override void OnPreferencesSaved() => ModSettings.Apply();
    }
}
