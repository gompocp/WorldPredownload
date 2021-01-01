using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
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
        public const string Version = "1.3.3";
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
            
            ClassInjector.RegisterTypeInIl2Cpp<NotificationMenuListener>();

            SetupUserInfoPatch.Patch();
            SetupSocialMenuPatch.Patch();
            if (!ModSettings.AdvancedInvites) SetupAcceptNotificationPatch.Patch();
            else SetupAdvancedInvitesPatch.Patch();

        }

        public override void VRChat_OnUiManagerInit()
        {
            ModSettings.Apply();
            GameObject.Find("UserInterface/QuickMenu/NotificationInteractMenu").AddComponent<NotificationMenuListener>().OnEnabled += delegate { InviteButton.UpdateText(); };
            InviteButton.Setup(!ModSettings.overrideInviteAcceptButton);
            FriendButton.Setup(!ModSettings.overrideSocialPageButton);
            WorldButton.Setup(!ModSettings.overrideWorldPageButton);
            WorldDownloadStatus.Setup();
        }

        public override void OnModSettingsApplied() => ModSettings.Apply();

        public override void OnUpdate()
        {
            if (Input.GetKeyUp(KeyCode.F8))
            {
                FriendButton.button.SetActive(false);
            }
        }
    }
}