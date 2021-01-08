using MelonLoader;

namespace WorldPredownload
{
    public static class ModSettings
    {
        public static bool autoFollowInvites { get; set; } = false;
        public static bool autoFollowFriends { get; set; } = false;
        public static bool autoFollowWorlds { get; set; } = false;
        public static bool showHudMessages { get; set; } = false;
        public static bool showStatusOnHud { get; set; } = false;
        public static bool showPopupsOnComplete { get; set; } = false;
        public static bool tryUseAdvancedInvitePopup { get; set; } = false;
        public static bool AdvancedInvites { get; set; } = false;
        
        public static void RegisterSettings()
        {
            if (Utilities.HasMod("AdvancedInvites")) 
                AdvancedInvites = true;

            MelonPrefs.RegisterCategory("WorldPredownload", "WorldPredownload");
            MelonPrefs.RegisterBool("WorldPredownload", "AutoFollowInvites", autoFollowInvites, "Auto Follow Invite Predownloads");
            MelonPrefs.RegisterBool("WorldPredownload", "AutoFollowWorlds", autoFollowInvites, "Auto Join World Predownloads");
            MelonPrefs.RegisterBool("WorldPredownload", "AutoFollowFriends", autoFollowFriends, "Auto Join Friend Predownloads");
            MelonPrefs.RegisterBool("WorldPredownload", "ShowStatusOnHud", showStatusOnHud, "Display download status on HUD");
            MelonPrefs.RegisterBool("WorldPredownload", "ShowHudMessages", showHudMessages, "Show Hud Messages");
            MelonPrefs.RegisterBool("WorldPredownload", "ShowPopupsOnComplete", showPopupsOnComplete, "Show Popup On Complete");
            if (AdvancedInvites) 
                MelonPrefs.RegisterBool("WorldPredownload", "UseAdvancedInvitesPopup", tryUseAdvancedInvitePopup, "Accept invites using AdvancedInvites popup");
        }

        public static void Apply()
        {
            autoFollowInvites = MelonPrefs.GetBool("WorldPredownload", "AutoFollowInvites");
            autoFollowWorlds = MelonPrefs.GetBool("WorldPredownload", "AutoFollowWorlds");
            autoFollowFriends = MelonPrefs.GetBool("WorldPredownload", "AutoFollowFriends");
            showStatusOnHud = MelonPrefs.GetBool("WorldPredownload", "ShowStatusOnHud");
            showHudMessages = MelonPrefs.GetBool("WorldPredownload", "ShowHudMessages");
            showPopupsOnComplete = MelonPrefs.GetBool("WorldPredownload", "ShowPopupsOnComplete");
            if(AdvancedInvites)
                tryUseAdvancedInvitePopup = MelonPrefs.GetBool("WorldPredownload", "UseAdvancedInvitesPopup");
        }
    }
}