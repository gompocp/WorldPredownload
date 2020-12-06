using System;
using UnityEngine;
using VRC.Core;
using VRC.UI;
using MelonLoader;
using WorldPredownload.Cache;
using WorldPredownload.DownloadManager;

namespace WorldPredownload.UI
{
    public static class WorldButton
    {
        public static GameObject button { get; set; }
        public static bool canChangeText { get; set; } = true;
        public static string worldID { get; set; } = "";
        public static ApiWorld apiWorld { get; set; }
        public static ApiWorldInstance apiWorldInstance { get; set; }
        public static PageWorldInfo worldInfo { get; set; }


        private const string PATH_TO_GAMEOBJECT_TO_CLONE = "UserInterface/MenuContent/Screens/WorldInfo/ReportButton";
        private const string PATH_TO_CLONE_PARENT = "UserInterface/MenuContent/Screens/WorldInfo";
        private const string PATH_TO_WORLDINFO = "UserInterface/MenuContent/Screens/WorldInfo";

        public static void Setup()
        {
            button = Utilities.CloneGameObject(PATH_TO_GAMEOBJECT_TO_CLONE, PATH_TO_CLONE_PARENT);
            button.GetRectTrans().SetAnchoredPos(new Vector2(200f, -188f));
            button.SetActive(true);
            button.SetName(Constants.WORLD_BUTTON_NAME);
            button.SetText(Constants.BUTTON_IDLE_TEXT);
            button.SetButtonAction(new Action(delegate
            {
                try
                {
                    worldInfo = GetWorldInfo();
                    worldID = string.Copy(GetWorldInfo().field_Private_ApiWorld_0.id);
                    ApiWorldInstance apiWorldInstance = GetWorldInfo().worldInstance;
                    WorldDownloadManager.DownloadWorld(GetWorldInfo().prop_ApiWorld_0, DownloadFromType.World);
                }
                catch(Exception e) { MelonLogger.Log($"Exception Occured Here: {e}"); }
            }));
        }

        public static void UpdateText(ApiWorld world)
        {

            if (WorldDownloadManager.downloading)
            {
                if (world.id.Equals(WorldButton.worldID))
                {
                    canChangeText = true;
                }
                else
                {
                    canChangeText = false;
                    button.SetText(Constants.BUTTON_BUSY_TEXT);
                }
            }
            else
            {
                if (CacheManager.HasDownloadedWorld(world.id))
                    button.SetText(Constants.BUTTON_ALREADY_DOWNLOADED_TEXT);
                else
                    button.SetText(Constants.BUTTON_IDLE_TEXT);
            }
        }

        public static void UpdateTextDownloadStopped()
        {
            try
            {
                if (CacheManager.HasDownloadedWorld(GetWorldInfo().field_Private_ApiWorld_0.id))
                    button.SetText(Constants.BUTTON_ALREADY_DOWNLOADED_TEXT);
                else
                    button.SetText(Constants.BUTTON_IDLE_TEXT);
            }
            catch { }
            canChangeText = true;
        }

        private static PageWorldInfo GetWorldInfo()
        {
            return GameObject.Find(PATH_TO_WORLDINFO).GetComponent<VRC.UI.PageWorldInfo>();
        }
    }
}
