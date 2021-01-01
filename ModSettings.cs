using Il2CppSystem.Buffers;
using MelonLoader;
using WorldPredownload.UI;

namespace WorldPredownload
{
    public static class ModSettings
    {
        public static bool overrideInviteAcceptButton { get; set; } = false;
        public static bool overrideWorldPageButton { get; set; } = false;
        public static bool overrideSocialPageButton { get; set; } = false;
        public static bool autoFollowInvites { get; set; } = false;
        public static bool autoFollowFriends { get; set; } = false;
        public static bool showHudMessages { get; set; } = false;
        public static bool showPopupsOnComplete { get; set; } = false;
        public static bool tryUseAdvancedInvitePopup { get; set; } = false;
        public static bool AdvancedInvites { get; set; } = false;
        
        public static void RegisterSettings()
        {
            if (Utilities.HasMod("AdvancedInvites")) AdvancedInvites = true;

            MelonPrefs.RegisterCategory("WorldPredownload", "WorldPredownload");
            MelonPrefs.RegisterBool("WorldPredownload", "OverrideInviteAcceptButton", overrideInviteAcceptButton, "Override Invite Accept Button");
            MelonPrefs.RegisterBool("WorldPredownload", "OverrideWorldPageButton", overrideWorldPageButton, "Override World Page Go Button");
            MelonPrefs.RegisterBool("WorldPredownload", "OverrideSocialPageButton", overrideSocialPageButton, "Override Social Page Join Button");
            MelonPrefs.RegisterBool("WorldPredownload", "AutoFollowInvites", autoFollowInvites, "Auto Follow Invite Predownloads");
            MelonPrefs.RegisterBool("WorldPredownload", "AutoFollowFriends", autoFollowFriends, "Auto Join Friend Predownloads");
            MelonPrefs.RegisterBool("WorldPredownload", "ShowHudMessages", showHudMessages, "Show Hud Messages");
            MelonPrefs.RegisterBool("WorldPredownload", "ShowPopupsOnComplete", showPopupsOnComplete, "Show Popup On Complete");
            if (AdvancedInvites) MelonPrefs.RegisterBool("WorldPredownload", "UseAdvancedInvitesPopup", tryUseAdvancedInvitePopup, "Accept invites using AdvancedInvites popup");
        }

        public static void Apply()
        {
            overrideInviteAcceptButton = MelonPrefs.GetBool("WorldPredownload", "OverrideInviteAcceptButton");
            overrideWorldPageButton = MelonPrefs.GetBool("WorldPredownload", "OverrideWorldPageButton");
            overrideSocialPageButton = MelonPrefs.GetBool("WorldPredownload", "OverrideSocialPageButton");
            autoFollowInvites = MelonPrefs.GetBool("WorldPredownload", "AutoFollowInvites");
            autoFollowFriends = MelonPrefs.GetBool("WorldPredownload", "AutoFollowFriends");
            showHudMessages = MelonPrefs.GetBool("WorldPredownload", "ShowHudMessages");
            showPopupsOnComplete = MelonPrefs.GetBool("WorldPredownload", "ShowPopupsOnComplete");
            if(AdvancedInvites)
                tryUseAdvancedInvitePopup = MelonPrefs.GetBool("WorldPredownload", "UseAdvancedInvitesPopup");

            if (InviteButton.button == null || FriendButton.button == null || WorldButton.button == null) return;
            if (overrideInviteAcceptButton)
                InviteButton.button.SetActive(false);
            else
                InviteButton.button.SetActive(true);
            
            if(overrideSocialPageButton )
                FriendButton.button.SetActive(false);
            else 
                FriendButton.button.SetActive(true);
            
            if(overrideWorldPageButton)
                WorldButton.button.SetActive(false);
            else 
                WorldButton.button.SetActive(true);
        }
        
    }
}