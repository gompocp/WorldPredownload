using System;
using UnhollowerRuntimeLib;
using UnityEngine.Networking;
using WorldPredownload.UI;
using OnDownloadProgress = AssetBundleDownloadManager.MulticastDelegateNInternalSealedVoUnUnique;

namespace WorldPredownload.DownloadManager
{
    public static class DownloadProgress
    {
        private static OnDownloadProgress onProgressDel;
        public static OnDownloadProgress GetOnProgressDel
        {
            get
            {
                if (onProgressDel != null) return onProgressDel;
                onProgressDel = 
                    DelegateSupport.ConvertDelegate<OnDownloadProgress>(
                    new Action<UnityWebRequest>(
                        delegate(UnityWebRequest request)
                        {
                            if (WorldDownloadManager.cancelled)
                            {
                                request.Abort();
                                WorldDownloadManager.cancelled = false;
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
                            
                    }));
                return onProgressDel;
            }
        }

    }
}