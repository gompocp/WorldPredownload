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

            MelonPrefs.RegisterCategory(categoryName, categoryName);
            //MelonPrefs.RegisterBool(categoryName, "AutoFollowInvites", autoFollowInvites, "Auto Follow Invite Predownloads");
            MelonPrefs.RegisterBool(categoryName, "AutoFollowWorlds", autoFollowInvites, "Auto Join World Predownloads");
            MelonPrefs.RegisterBool(categoryName, "AutoFollowFriends", autoFollowFriends, "Auto Join Friend Predownloads");
            MelonPrefs.RegisterBool(categoryName, "ShowStatusOnQM", showStatusOnQM, "Display download status on QM");
            MelonPrefs.RegisterBool(categoryName, "ShowStatusOnHud", showStatusOnHud, "Display download status on HUD");
            MelonPrefs.RegisterBool(categoryName, "ShowHudMessages", showHudMessages, "Show Hud Messages");
            MelonPrefs.RegisterBool(categoryName, "ShowPopupsOnComplete", showPopupsOnComplete, "Show Popup On Complete");
            
            //if (AdvancedInvites) 
            //    MelonPrefs.RegisterBool(categoryName, "UseAdvancedInvitesPopup", tryUseAdvancedInvitePopup, "Accept invites using AdvancedInvites popup");
        }

        public static void Apply()
        {
            //autoFollowInvites = MelonPrefs.GetBool(categoryName, "AutoFollowInvites");
            autoFollowWorlds = MelonPrefs.GetBool(categoryName, "AutoFollowWorlds");
            autoFollowFriends = MelonPrefs.GetBool(categoryName, "AutoFollowFriends");
            showStatusOnQM = MelonPrefs.GetBool(categoryName, "ShowStatusOnQM");
            showStatusOnHud = MelonPrefs.GetBool(categoryName, "ShowStatusOnHud");
            showHudMessages = MelonPrefs.GetBool(categoryName, "ShowHudMessages");
            showPopupsOnComplete = MelonPrefs.GetBool(categoryName, "ShowPopupsOnComplete");
            //if(AdvancedInvites)
                //tryUseAdvancedInvitePopup = MelonPrefs.GetBool(categoryName, "UseAdvancedInvitesPopup");
            if (showStatusOnQM)
                WorldDownloadStatus.Enable();
            else 
                WorldDownloadStatus.Disable();
                
        }
    }
}