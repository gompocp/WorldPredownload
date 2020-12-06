using MelonLoader;
using System;
using System.Collections;
using UnityEngine;
using VRC.Core;
using VRC.UI;
using WorldPredownload.Cache;
using WorldPredownload.DownloadManager;
using Object = System.Object;

namespace WorldPredownload.UI
{
    class FriendButton
    {

        public static GameObject button { get; set; }
        public static bool canChangeText { get; set; } = true;
        public static string userID { get; set; } = "";
        public static APIUser user { get; set; }
        public static string worldID { get; set; } = "";
        public static PageUserInfo userInfo { get; set; }

        private const string PATH_TO_GAMEOBJECT_TO_CLONE = "UserInterface/MenuContent/Screens/UserInfo/User Panel/Playlists";
        private const string PATH_TO_CLONE_PARENT = "UserInterface/MenuContent/Screens/UserInfo/User Panel";
        private const string GAMEOBJECT_NAME = "PreloadUserButton";
        private const string BUTTON_DEFAULT_TEXT = "Preload";
        private const string PATH_TO_GAMEOBJECT_TO_DESTROY = "UserInterface/MenuContent/Screens/UserInfo/User Panel/PreloadWorld/PlaylistsButton/Image/Icon_New";
        private const string PATH_TO_USERINFO = "UserInterface/MenuContent/Screens/UserInfo";
        private const string CLICK_ERROR_MESSAGE = "User may have clicked too quickly";

        public static void Setup()
        {
            button = Utilities.CloneGameObject(PATH_TO_GAMEOBJECT_TO_CLONE, PATH_TO_CLONE_PARENT);
            button.GetRectTrans().SetAnchoredPos(new Vector2(-50f, -50f));  //213f, 315f
            button.SetActive(true);
            button.SetName(GAMEOBJECT_NAME);
            button.SetText(BUTTON_DEFAULT_TEXT);
            button.SetButtonActionInChildren(new Action(delegate
            {
                try
                {
                    if (WorldDownloadManager.downloading || button.GetTextComponentInChildren().text.Equals(Constants.BUTTON_ALREADY_DOWNLOADED_TEXT))
                    {
                        WorldDownloadManager.CancelDownload();
                        return;
                    }
                    user = GetUserInfo().user;
                    userID = user.id;
                    worldID = GetUserInfo().field_Private_ApiWorld_0.id;
                    WorldDownloadManager.DownloadWorld(GetUserInfo().field_Private_ApiWorld_0, DownloadFromType.Friend);
                }
                catch { MelonLogger.LogWarning(CLICK_ERROR_MESSAGE); }
            }));
            GameObject.Destroy(GameObject.Find(PATH_TO_GAMEOBJECT_TO_DESTROY));
        }


        public static void UpdateTextDownloadStopped()
        {
            //Lazy way to check if the user menu is up
            try
            {
                if (CacheManager.HasDownloadedWorld(GetUserInfo().field_Private_ApiWorld_0.id))
                    button.SetText(Constants.BUTTON_ALREADY_DOWNLOADED_TEXT);
                else
                    button.SetText(Constants.BUTTON_IDLE_TEXT);
            }
            catch
            {
                // if it isn't up who the fuck cares what it says
            }
            canChangeText = true;
        }


        public static IEnumerator UpdateText()
        {
            while (GetUserInfo().field_Private_Boolean_0 != true) yield return null;
            FriendButton.button.SetActive(true);
            if (WorldDownloadManager.downloading)
            {
                if (GetUserInfo().user.id.Equals(userID))
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
                if (CacheManager.HasDownloadedWorld(GetUserInfo().field_Private_ApiWorld_0.id))
                    button.SetText(Constants.BUTTON_ALREADY_DOWNLOADED_TEXT);
                else button.SetText(Constants.BUTTON_IDLE_TEXT);
            }
        }

        public static PageUserInfo GetUserInfo()
        {
            return GameObject.Find(PATH_TO_USERINFO).GetComponent<PageUserInfo>();
        }
    }
}
