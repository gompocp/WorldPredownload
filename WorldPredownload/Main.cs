using Harmony;
using MelonLoader;
using WorldPredownload.UI;
using WorldPredownload.Cache;

namespace WorldPredownload
{
    public static class ModInfo
    {
        public const string Name = "WorldPredownload";
        public const string Author = "gompo";
        public const string Version = "1.4.0";
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
            SocialMenuSetup.Patch();
            WorldInfoSetup.Patch();
            WorldDownloadListener.Patch();
            NotificationMoreActions.Patch();
        }
        
        public override void VRChat_OnUiManagerInit()
        {
            InviteButton.Setup();
            FriendButton.Setup();
            WorldButton.Setup();
            WorldDownloadStatus.Setup();
            HudIcon.Setup();
            CacheManager.UpdateDirectories();
        }
        
        public override void OnPreferencesLoaded() => ModSettings.Apply();

        public override void OnPreferencesSaved() => ModSettings.Apply();
    }
}
