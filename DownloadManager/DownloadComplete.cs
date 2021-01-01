using System;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine.Networking;
using WorldPredownload.Cache;
using WorldPredownload.UI;
using AssetBundleDownload = CustomYieldInstructionPublicObAsByStInStCoBoObInUnique;
using OnDownloadComplete = AssetBundleDownloadManager.MulticastDelegateNInternalSealedVoObUnique;

namespace WorldPredownload.DownloadManager
{
    public static class DownloadComplete
    {
        private static OnDownloadComplete onCompleteDel;
        public static OnDownloadComplete GetOnCompleteDel
        {
            get
            {
                if (onCompleteDel != null) return onCompleteDel;
                onCompleteDel = 
                    DelegateSupport.ConvertDelegate<OnDownloadComplete>(
                        new Action<AssetBundleDownload>(
                            delegate(AssetBundleDownload download)
                            {
                            
                                WorldDownloadManager.downloading = false;
                                Logger.Log("Adding to cache list");
                                CacheManager.AddDirectory(CacheManager.ComputeAssetHash(WorldDownloadManager.DownloadInfo.ApiWorld.id));
                                Logger.Log("Updating Text");
                                InviteButton.UpdateTextDownloadStopped();
                                FriendButton.UpdateTextDownloadStopped();
                                WorldButton.UpdateTextDownloadStopped();
                                WorldDownloadStatus.gameObject.SetText(Constants.DOWNLOAD_STATUS_IDLE_TEXT);
                                MelonLogger.Log("World Downloaded: " + download.field_Public_String_0);
                                switch (WorldDownloadManager.DownloadInfo.DownloadType)
                                {
                                    case DownloadType.Friend:
                                        if (!ModSettings.autoFollowFriends)
                                           WorldDownloadManager.DisplayFriendPopup();
                                        else
                                            Utilities.GoToWorld(WorldDownloadManager.DownloadInfo.ApiWorld, WorldDownloadManager.DownloadInfo.InstanceIDTags);
                                        WorldDownloadManager.ClearDownload();
                                        break;
                                    case DownloadType.Invite:
                                        if (!ModSettings.autoFollowInvites)
                                            WorldDownloadManager.DisplayInvitePopup();
                                        else
                                            Utilities.GoToWorld(WorldDownloadManager.DownloadInfo.ApiWorld, WorldDownloadManager.DownloadInfo.InstanceIDTags);
                                        WorldDownloadManager.ClearDownload();
                                        break;
                                    case DownloadType.World:
                                        WorldDownloadManager.DisplayWorldPopup();
                                        break;
                                }
                            
                            }));
                return onCompleteDel;
            }
        }
    }
}