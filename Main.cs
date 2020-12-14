using Harmony;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;
using WorldPredownload.Components;
using WorldPredownload.UI;

namespace WorldPredownload
{
    public class Main : MelonMod
    {
        public static HarmonyInstance harmonyInstance { get; } = HarmonyInstance.Create("gompo.worldpredownload");

        public static bool autoFollowInvites { get; set; } = false;
        public static bool autoFollowFriends { get; set; } = false;
        public static bool tryUseAdvancedInvitePopup { get; set; } = false;

        public static bool AdvancedInvites { get; set; } = false;

        public override void OnApplicationStart()
        {
            if (Utilities.HasMod("AdvancedInvites")) AdvancedInvites = true;

            MelonPrefs.RegisterCategory("WorldPredownload", "WorldPredownload");
            MelonPrefs.RegisterBool("WorldPredownload", "AutoFollowInvites", autoFollowInvites, "Auto Accept Invite Predownloads");
            MelonPrefs.RegisterBool("WorldPredownload", "AutoFollowFriends", autoFollowFriends, "Auto Join Friend Predownloads");
            if (AdvancedInvites) MelonPrefs.RegisterBool("WorldPredownload", "UseAdvancedInvitesPopup", tryUseAdvancedInvitePopup, "Accept invites using AdvancedInvites popup");

            OnModSettingsApplied();
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

        public override void OnModSettingsApplied()
        {
            autoFollowInvites = MelonPrefs.GetBool("WorldPredownload", "AutoFollowInvites");
            autoFollowFriends = MelonPrefs.GetBool("WorldPredownload", "AutoFollowFriends");
            if(AdvancedInvites) tryUseAdvancedInvitePopup = MelonPrefs.GetBool("WorldPredownload", "UseAdvancedInvitesPopup");
        }

        public static void OnNotificationMenuOpen() => InviteButton.UpdateText();
    }
}
