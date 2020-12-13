using AssetBundleDownload = CustomYieldInstructionPublicObAsByStInStCoBoObInUnique;
using OnDownloadComplete = AssetBundleDownloadManager.MulticastDelegateNInternalSealedVoObUnique;
using OnDownloadProgress = AssetBundleDownloadManager.MulticastDelegateNInternalSealedVoUnUnique;
using OnDownloadError = AssetBundleDownloadManager.MulticastDelegateNInternalSealedVoStObStUnique;
using LoadErrorReason = EnumPublicSealedvaNoMiFiUnCoSeAsDuAsUnique;
using UnpackType = AssetBundleDownloadManager.EnumNInternalSealedva3vUnique;
using WorldPredownload.UI;
using UnhollowerRuntimeLib;
using System;
using VRC.Core;
using UnityEngine;
using UnityEngine.UI;
using MelonLoader;
using WorldPredownload.Cache;

namespace WorldPredownload.DownloadManager
{
    public static class WorldDownloadManager
    {
        public static bool downloading { get; set; } = false;
        public static string currentDownloadingID { get; set; } = "";
        private static bool cancelled = false;
        private static ApiWorld world;
        private static DownloadFromType downloadFromType;


        public static void CancelDownload() {
            if(downloading) cancelled = true;
        }

        public static void OnDownloadProgress(UnityEngine.Networking.UnityWebRequest request)
        {
            if (cancelled)
            {
                request.Abort();
                cancelled = false;
                return;
            }
            string size = request.GetResponseHeader("Content-Length");
            if (request.downloadProgress >= 0 && 0.9 >= request.downloadProgress)
            {
                string progress = ((request.downloadProgress / 0.9) * 100).ToString("0") + " % ";
                WorldDownloadStatus.gameObject.SetText("Progress:" + progress);
                if (InviteButton.canChangeText) InviteButton.button.SetText("Cancel: " + progress);
                if (FriendButton.canChangeText) FriendButton.button.SetText("Cancel: " + progress);
                if (WorldButton.canChangeText) WorldButton.button.SetText("Cancel: " + progress);
            }
        }

        public static void OnError(string url, string message, LoadErrorReason reason)
        {
            Utilities.ClearErrors();
            WorldDownloadStatus.gameObject.SetText(Constants.DOWNLOAD_STATUS_IDLE_TEXT);
            downloading = false;
            FriendButton.UpdateTextDownloadStopped();
            WorldButton.UpdateTextDownloadStopped();
            InviteButton.UpdateTextDownloadStopped();
            ResetButtons();
            if (message.Contains("Request aborted")) return;
            MelonLogger.LogWarning(url + " " + message + " " + reason);
            Utilities.ShowDismissPopup(
                Constants.DOWNLOAD_ERROR_TITLE,
                Constants.DOWNLOAD_ERROR_MSG, 
                Constants.DOWNLOAD_ERROR_BTN_TEXT, 
                new Action(delegate {
                    Utilities.HideCurrentPopup();
                })
            );
        }


        public static void OnComplete(AssetBundleDownload download)
        {
            downloading = false;
            CacheManager.AddDirectory(CacheManager.ComputeAssetHash(world.id));
            InviteButton.UpdateTextDownloadStopped();
            FriendButton.UpdateTextDownloadStopped();
            WorldButton.UpdateTextDownloadStopped();
            WorldDownloadStatus.gameObject.SetText(Constants.DOWNLOAD_STATUS_IDLE_TEXT);
            MelonLogger.Log("World Downloaded: " + download.field_Public_String_0);
            switch (downloadFromType)
            {
                case DownloadFromType.Friend:
                    DisplayFriendPopup();
                    break;
                case DownloadFromType.Invite:
                    DisplayInvitePopup();
                    break;
                case DownloadFromType.World:
                    DisplayWorldPopup();
                    break;
            }

        }

        public static void ResetButtons()
        {
            world = null;
            WorldButton.apiWorld = null;
            WorldButton.apiWorldInstance = null;
            WorldButton.worldID = "";
            FriendButton.user = null;
            FriendButton.worldID = "";
            FriendButton.userID = null;
        }

        public static void DisplayWorldPopup()
        {
            if (GameObject.Find("UserInterface/MenuContent/Screens/WorldInfo").active)
            {
                ResetButtons();
                return;
            }
            Utilities.ShowOptionPopup(
                Constants.DOWNLOAD_SUCCESS_TITLE,
                Constants.DOWNLOAD_SUCCESS_MSG,
                Constants.DOWNLOAD_SUCCESS_LEFT_BTN_TEXT,
                new Action(delegate
                {
                    Utilities.HideCurrentPopup();
                    GameObject.Find("UserInterface/QuickMenu/ShortcutMenu/WorldsButton").GetComponent<Button>().onClick.Invoke();
                    Utilities.ShowPage(WorldButton.worldInfo);
                    WorldButton.worldInfo.Method_Public_Void_ApiWorld_ApiWorldInstance_Boolean_Boolean_0(world, WorldButton.apiWorldInstance);
                    ResetButtons();
                }),
                Constants.DOWNLOAD_SUCCESS_RIGHT_BTN_TEXT,
                new Action(delegate
                {
                    Utilities.HideCurrentPopup();
                    ResetButtons();
                })
            );
        }

        public static void DisplayInvitePopup()
        {
            Utilities.ShowDismissPopup(
                Constants.DOWNLOAD_SUCCESS_TITLE,
                Constants.DOWNLOAD_SUCCESS_MSG, 
                Constants.DOWNLOAD_SUCCESS_RIGHT_BTN_TEXT, 
                new Action(delegate {
                    Utilities.HideCurrentPopup();
                    ResetButtons();
                })
            );
        }

        public static void DisplayFriendPopup()
        {
            if (GameObject.Find("UserInterface/MenuContent/Screens/UserInfo").active)
            {
                ResetButtons();
                return;
            }
            Utilities.ShowOptionPopup(
                Constants.DOWNLOAD_SUCCESS_TITLE,
                Constants.DOWNLOAD_SUCCESS_MSG,
                Constants.DOWNLOAD_SUCCESS_LEFT_BTN_TEXT_F,
                new Action(delegate
                {
                    Utilities.HideCurrentPopup();
                    GameObject.Find("UserInterface/QuickMenu/ShortcutMenu/SocialButton").GetComponent<Button>().onClick.Invoke();
                    Utilities.ShowPage(FriendButton.userInfo);
                    FriendButton.userInfo.Method_Public_Void_APIUser_PDM_0(FriendButton.user);
                    ResetButtons();
                }),
                Constants.DOWNLOAD_SUCCESS_RIGHT_BTN_TEXT,
                new Action(delegate
                {
                    Utilities.HideCurrentPopup();
                    ResetButtons();
                })
            );
        }

        public static void DownloadWorld(ApiWorld apiWorld, DownloadFromType downloadType)
        {
            
            if (!downloading)
            {
                downloadFromType = downloadType;
                world = apiWorld;
                currentDownloadingID = string.Copy(apiWorld.id);
                downloading = true;
                Action<UnityEngine.Networking.UnityWebRequest> onProgressDel = OnDownloadProgress;
                Action<AssetBundleDownload> onCompleteDel = OnComplete;
                Action<string, string, LoadErrorReason> OnErrorDel = OnError;
                Utilities.DownloadApiWorld( 
                    apiWorld,
                    DelegateSupport.ConvertDelegate<OnDownloadProgress>(onProgressDel),
                    DelegateSupport.ConvertDelegate<OnDownloadComplete>(onCompleteDel),
                    DelegateSupport.ConvertDelegate<OnDownloadError>(OnErrorDel),
                    true,
                    UnpackType.EnumValue1);
            }
            else
            {
                cancelled = true;
                InviteButton.button.SetText(Constants.BUTTON_IDLE_TEXT);
                WorldButton.button.SetText(Constants.BUTTON_IDLE_TEXT);
                FriendButton.button.SetText(Constants.BUTTON_IDLE_TEXT);
            }
        }
    }
}
