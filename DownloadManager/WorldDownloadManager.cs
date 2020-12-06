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


        public static void CancelDownload() => cancelled = true;

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
                string progress = ((request.downloadProgress / 0.9) * 100).ToString("0.00") + " % ";
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
            if (message.Contains("Request aborted")) return;
            MelonLogger.LogError(url + " " + message + " " + reason);
            VRCUiPopupManager.prop_VRCUiPopupManager_0.Method_Public_Void_String_String_String_Action_Action_1_VRCUiPopup_0("World Preload Failed", "There was an error preloading the world", "Dismiss", new Action(delegate { VRCUiPopupManager.prop_VRCUiPopupManager_0.Method_Public_Void_0(); }));
        }


        public static void OnComplete(AssetBundleDownload download)
        {
            downloading = false;
            CacheManager.AddDirectory(CacheManager.ComputeAssetHash(world.id));
            try
            {
                InviteButton.UpdateTextDownloadStopped();
                FriendButton.UpdateTextDownloadStopped();
                WorldButton.UpdateTextDownloadStopped();

            }
            catch { MelonLogger.Log("Failed to update Text"); }
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

        public static void DisplayWorldPopup()
        {
            if (GameObject.Find("UserInterface/MenuContent/Screens/WorldInfo").active) return;
            VRCUiPopupManager.prop_VRCUiPopupManager_0.Method_Public_Void_String_String_String_Action_String_Action_Action_1_VRCUiPopup_1(
                "World Download Complete",
                "Your world has finished downloading, you can now go to the world if you wish so",
                "Go to World Page",
                new Action(delegate
                {
                    VRCUiPopupManager.prop_VRCUiPopupManager_0.Method_Public_Void_0();
                    GameObject.Find("UserInterface/QuickMenu/ShortcutMenu/WorldsButton").GetComponent<Button>().onClick.Invoke();
                    //VRCUiManager.prop_VRCUiManager_0.Me
                    VRCUiManager.prop_VRCUiManager_0.Method_Public_VRCUiPage_VRCUiPage_0(WorldButton.worldInfo);
                    WorldButton.worldInfo.Method_Public_Void_ApiWorld_ApiWorldInstance_Boolean_Boolean_0(world, WorldButton.apiWorldInstance);

                }),
                "Dismiss",
                new Action(delegate
                {
                    VRCUiPopupManager.prop_VRCUiPopupManager_0.Method_Public_Void_0();
                })
            );
        }

        public static void DisplayInvitePopup()
        {
            VRCUiPopupManager.prop_VRCUiPopupManager_0.Method_Public_Void_String_String_String_Action_Action_1_VRCUiPopup_0("World Download Complete", "Your world has finished downloading, you can now go to the world if you wish so", "Dismiss", new Action(delegate { VRCUiPopupManager.prop_VRCUiPopupManager_0.Method_Public_Void_0(); }));
        }

        public static void DisplayFriendPopup()
        {
            if (GameObject.Find("UserInterface/MenuContent/Screens/UserInfo").active) return;
            VRCUiPopupManager.prop_VRCUiPopupManager_0.Method_Public_Void_String_String_String_Action_String_Action_Action_1_VRCUiPopup_1(
                "World Download Complete",
                "Your world has finished downloading, you can now go to the world if you wish so",
                "Go to Friend Page",
                new Action(delegate
                {
                    VRCUiPopupManager.prop_VRCUiPopupManager_0.Method_Public_Void_0();
                    GameObject.Find("UserInterface/QuickMenu/ShortcutMenu/SocialButton").GetComponent<Button>().onClick.Invoke();
                    VRCUiManager.prop_VRCUiManager_0.Method_Public_VRCUiPage_VRCUiPage_0(FriendButton.userInfo);
                    FriendButton.userInfo.Method_Public_Void_APIUser_PDM_0(FriendButton.user);

                }),
                "Dismiss",
                new Action(delegate
                {
                    VRCUiPopupManager.prop_VRCUiPopupManager_0.Method_Public_Void_0();
                })
            );
        }

        public static void DownloadWorld(ApiWorld apiWorld, DownloadFromType downloadType)
        {
            downloadFromType = downloadType;
            world = apiWorld;
            if (!downloading)
            {
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
