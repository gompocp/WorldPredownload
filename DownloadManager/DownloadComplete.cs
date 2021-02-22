using System;
using System.Diagnostics.CodeAnalysis;
using MelonLoader;
using UnhollowerRuntimeLib;
using WorldPredownload.Cache;
using WorldPredownload.UI;
//using AssetBundleDownload = CustomYieldInstructionPublicObAsByStInStCoBoObInUnique;
using OnDownloadComplete = AssetBundleDownloadManager.MulticastDelegateNInternalSealedVoObUnique;

namespace WorldPredownload.DownloadManager
{
    [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
    [SuppressMessage("ReSharper", "HeuristicUnreachableCode")]
    public static class DownloadComplete
    {
        public static OnDownloadComplete onCompleteDel;
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
                                if(ModSettings.showHudMessages) Utilities.QueueHudMessage("Download Finished");
                                WorldDownloadManager.DownloadInfo.complete = true;
                                WorldDownloadManager.downloading = false;
                                CacheManager.AddDirectory(CacheManager.ComputeAssetHash(WorldDownloadManager.DownloadInfo.ApiWorld.id));
                                HudIcon.Disable();
                                InviteButton.UpdateTextDownloadStopped();
                                FriendButton.UpdateTextDownloadStopped();
                                WorldButton.UpdateTextDownloadStopped();
                                WorldDownloadStatus.gameObject.SetText(Constants.DOWNLOAD_STATUS_IDLE_TEXT);
                                MelonLogger.Log("World Downloaded: " + download.field_Public_String_0);
                                
                                switch (WorldDownloadManager.DownloadInfo.DownloadType)
                                {
                                    case DownloadType.Friend:
                                        if (!ModSettings.autoFollowFriends)
                                        {
                                            if (ModSettings.showPopupsOnComplete) 
                                                WorldDownloadManager.DisplayFriendPopup();
                                        }
                                        else
                                            Utilities.GoToWorld(WorldDownloadManager.DownloadInfo.ApiWorld, WorldDownloadManager.DownloadInfo.InstanceIDTags, false);
                                        break;
                                    case DownloadType.Invite:
                                        if (!ModSettings.autoFollowInvites)
                                        {
                                            if (ModSettings.showPopupsOnComplete)
                                                WorldDownloadManager.DisplayInvitePopup();
                                        }
                                        else
                                            Utilities.GoToWorld(WorldDownloadManager.DownloadInfo.ApiWorld, WorldDownloadManager.DownloadInfo.InstanceIDTags, true);
                                        break;
                                    case DownloadType.World:
                                        if (!ModSettings.autoFollowWorlds)
                                        {
                                            if (ModSettings.showPopupsOnComplete)
                                                WorldDownloadManager.DisplayWorldPopup();
                                        }
                                        else 
                                            Utilities.GoToWorld(WorldDownloadManager.DownloadInfo.ApiWorld, WorldDownloadManager.DownloadInfo.InstanceIDTags, false);
                                        break;
                                }
                            
                            }));
                return onCompleteDel;
            }
        }
    }
}