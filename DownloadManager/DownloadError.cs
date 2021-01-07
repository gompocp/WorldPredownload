using System;
using MelonLoader;
using UnhollowerRuntimeLib;
using WorldPredownload.UI;
using OnDownloadError = AssetBundleDownloadManager.MulticastDelegateNInternalSealedVoStObStUnique;
using LoadErrorReason = EnumPublicSealedvaNoMiFiUnCoSeAsDuAsUnique;

namespace WorldPredownload.DownloadManager
{
    public static class DownloadError
    {
        private static OnDownloadError onErrorDel;
        public static OnDownloadError GetOnErrorDel
        {
            get
            {
                //if (onErrorDel != null) return onErrorDel;
                onErrorDel = 
                    DelegateSupport.ConvertDelegate<OnDownloadError>(
                        new Action<string, string, LoadErrorReason>(
                            delegate(string url, string message, LoadErrorReason reason)
                            {
                                WorldDownloadManager.DownloadInfo.complete = true;
                                Utilities.ClearErrors();
                                WorldDownloadStatus.gameObject.SetText(Constants.DOWNLOAD_STATUS_IDLE_TEXT);
                                WorldDownloadManager.downloading = false;
                                FriendButton.UpdateTextDownloadStopped();
                                WorldButton.UpdateTextDownloadStopped();
                                InviteButton.UpdateTextDownloadStopped();
                                WorldDownloadManager.ClearDownload();
                                if (message.Contains("Request aborted")) return;
                                MelonLogger.LogError(url + " " + message + " " + reason);
                                Utilities.ShowDismissPopup(
                                    Constants.DOWNLOAD_ERROR_TITLE,
                                    Constants.DOWNLOAD_ERROR_MSG, 
                                    Constants.DOWNLOAD_ERROR_BTN_TEXT, 
                                    new Action(delegate {
                                        Utilities.HideCurrentPopup();
                                    })
                                );
                            
                            }));
                return onErrorDel;
            }
        }
        
    }
}