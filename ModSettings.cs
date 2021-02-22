using MelonLoader;
using WorldPredownload.UI;

namespace WorldPredownload
{
    public static class ModSettings
    {
        private static string categoryName  = "WorldPredownload";
        public static bool autoFollowInvites { get; set; } = false;
        public static bool autoFollowFriends { get; set; } = false;
        public static bool autoFollowWorlds { get; set; } = false;
        public static bool showStatusOnQM { get; set; } = true;
        public static bool showHudMessages { get; set; } = true;
        public static bool showStatusOnHud { get; set; } = true;
        public static bool showPopupsOnComplete { get; set; } = true;
        public static bool tryUseAdvancedInvitePopup { get; set; } = true;
        public static bool AdvancedInvites { get; set; } = false;
        
        
        public static void RegisterSettings()
        {
            if (Utilities.HasMod("AdvancedInvites")) 
               AdvancedInvites = true;
            MelonPreferences.CreateCategory(categoryName, categoryName);
            MelonPreferences.CreateEntry(categoryName, "AutoFollowInvites", autoFollowInvites, "Auto Follow Invite Predownloads");
            MelonPreferences.CreateEntry(categoryName, "AutoFollowWorlds", autoFollowInvites, "Auto Join World Predownloads");
            MelonPreferences.CreateEntry(categoryName, "AutoFollowFriends", autoFollowFriends, "Auto Join Friend Predownloads");
            MelonPreferences.CreateEntry(categoryName, "ShowStatusOnQM", showStatusOnQM, "Display download status on QM");
            MelonPreferences.CreateEntry(categoryName, "ShowStatusOnHud", showStatusOnHud, "Display download status on HUD");
            MelonPreferences.CreateEntry(categoryName, "ShowHudMessages", showHudMessages, "Show Hud Messages");
            MelonPreferences.CreateEntry(categoryName, "ShowPopupsOnComplete", showPopupsOnComplete, "Show Popup On Complete");
            if (AdvancedInvites) 
                MelonPreferences.CreateEntry(categoryName, "UseAdvancedInvitesPopup", tryUseAdvancedInvitePopup, "Accept invites using AdvancedInvites popup");
        }

        public static void Apply()
        {
            autoFollowInvites = MelonPreferences.GetEntryValue<bool>(categoryName, "AutoFollowInvites");
            autoFollowWorlds =  MelonPreferences.GetEntryValue<bool>(categoryName, "AutoFollowWorlds");
            autoFollowFriends =  MelonPreferences.GetEntryValue<bool>(categoryName, "AutoFollowFriends");
            showStatusOnQM =  MelonPreferences.GetEntryValue<bool>(categoryName, "ShowStatusOnQM");
            showStatusOnHud =  MelonPreferences.GetEntryValue<bool>(categoryName, "ShowStatusOnHud");
            showHudMessages =  MelonPreferences.GetEntryValue<bool>(categoryName, "ShowHudMessages");
            showPopupsOnComplete =  MelonPreferences.GetEntryValue<bool>(categoryName, "ShowPopupsOnComplete");
            if(AdvancedInvites)
                tryUseAdvancedInvitePopup =  MelonPreferences.GetEntryValue<bool>(categoryName, "UseAdvancedInvitesPopup");
            if (showStatusOnQM)
                WorldDownloadStatus.Enable();
            else 
                WorldDownloadStatus.Disable();
                
        }
    }
}