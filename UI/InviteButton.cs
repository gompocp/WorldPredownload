using MelonLoader;
using System;
using Transmtn.DTO.Notifications;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VRC.Core;
using WorldPredownload.Cache;
using WorldPredownload.DownloadManager;

namespace WorldPredownload.UI
{
    public static class InviteButton
    {
        public static GameObject button { get; set; }
        public static bool canChangeText { get; set; } = true;

        private const string PATH_TO_GAMEOBJECT_TO_CLONE = "UserInterface/QuickMenu/NotificationInteractMenu/BlockButton";
        private const string PATH_TO_CLONE_PARENT = "UserInterface/QuickMenu/NotificationInteractMenu";
        private const string UNABLE_TO_CONVERT_WORLDID = "Error Creating ApiWorld From Notification";
        

        public static void Setup()
        {
            button = Utilities.CloneGameObject(PATH_TO_GAMEOBJECT_TO_CLONE, PATH_TO_CLONE_PARENT);
            button.GetRectTrans().SetAnchoredPos(new Vector2(1470f, -630f));
            button.SetName(Constants.INVITE_BUTTON_NAME);
            button.SetText(Constants.BUTTON_IDLE_TEXT);
            button.SetButtonAction(new Action(delegate
            {
                if (WorldDownloadManager.downloading)
                {
                    WorldDownloadManager.CancelDownload();
                    return;
                }
                //Credit: https://github.com/Psychloor/AdvancedInvites/blob/master/AdvancedInvites/InviteHandler.cs
                API.Fetch<ApiWorld>(Utilities.GetSelectedNotification().GetWorldID(),
                new Action<ApiContainer>(
                    container =>
                    {
                        WorldDownloadManager.DownloadWorld(container.Model.Cast<ApiWorld>(), DownloadFromType.Invite);
                    }),
                new Action<ApiContainer>(delegate {
                    MelonLogger.Log(UNABLE_TO_CONVERT_WORLDID);
                }));
            }));
        }

        public static void UpdateTextDownloadStopped()
        {
            //Lazy way to check if the invite menu is up
            try
            {
                if (CacheManager.HasDownloadedWorld(Utilities.GetSelectedNotification().GetWorldID()))
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

        public static void UpdateText()
        {
            if(Utilities.GetSelectedNotification().notificationType.Equals("invite")) {
                button.SetActive(true);
                if (WorldDownloadManager.downloading)
                {
                    if (Utilities.GetSelectedNotification().GetWorldID().Equals(WorldDownloadManager.currentDownloadingID))
                        canChangeText = true;
                    else
                    {
                        canChangeText = false;
                        button.SetText(Constants.BUTTON_BUSY_TEXT);
                    }
                }
                else
                {
                    if (CacheManager.HasDownloadedWorld(Utilities.GetSelectedNotification().GetWorldID())) button.SetText(Constants.BUTTON_ALREADY_DOWNLOADED_TEXT);
                    else button.SetText(Constants.BUTTON_IDLE_TEXT);
                }
            } 
            else
                button.SetActive(false);
        }
    }
}
