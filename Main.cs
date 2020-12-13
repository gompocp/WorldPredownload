using Harmony;
using MelonLoader;
using System.IO;
using UnhollowerRuntimeLib;
using UnityEngine;
using WorldPredownload.Cache;
using WorldPredownload.Components;
using WorldPredownload.UI;

[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonInfo(typeof(WorldPredownload.Main), "WorldPredownload", "1.2.4", "gompo#6956", null)]


namespace WorldPredownload
{
    public class Main : MelonMod
    {
        public static HarmonyInstance harmonyInstance { get; } = HarmonyInstance.Create("gompo.worldpredownload");

        public override void OnApplicationStart()
        {
            NotificationMenuListener.OnEnableMethod = typeof(Main).GetMethod(nameof(OnNotificationMenuOpen));
            ClassInjector.RegisterTypeInIl2Cpp<NotificationMenuListener>();
            SetupUserInfoPatch.Patch();
            SetupSocialMenuPatch.Patch();
        }

        public override void VRChat_OnUiManagerInit()
        {
            GameObject.Find("UserInterface/QuickMenu/NotificationInteractMenu").AddComponent<NotificationMenuListener>();
            InviteButton.Setup();
            FriendButton.Setup();
            WorldButton.Setup();
            WorldDownloadStatus.Setup();
        }

        public static void OnNotificationMenuOpen() => InviteButton.UpdateText();

    }
}
