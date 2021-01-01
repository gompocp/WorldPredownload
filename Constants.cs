using UnityEngine;

namespace WorldPredownload
{
    public static class Constants
    {
        public const string INVITE_BUTTON_NAME = "PreloadInviteButton";
        public const string FRIEND_BUTTON_NAME = "PreloadFriendButton";
        public const string WORLD_BUTTON_NAME = "PreloadWorldButton";
        public const string DOWNLOAD_STATUS_NAME = "DownloadStatusText";
        public const string DOWNLOAD_STATUS_IDLE_TEXT = "Not Downloading";
        public const string BUTTON_INVITE_OVERWRITE_TEXT = "Accept";
        public const string BUTTON_IDLE_TEXT = "Predownload";
        public const string BUTTON_BUSY_TEXT = "Cancel other download";
        public const string BUTTON_ALREADY_DOWNLOADED_TEXT = "Downloaded";
        public const string DOWNLOAD_ERROR_TITLE = "World Predownload Failed";
        public const string DOWNLOAD_ERROR_MSG = "There was an error predownloading the world";
        public const string DOWNLOAD_ERROR_BTN_TEXT = "Dismiss";
        public const string DOWNLOAD_SUCCESS_TITLE = "World Download Complete";
        public const string DOWNLOAD_SUCCESS_MSG = "Your world has finished downloading, you can now go to the world if you wish so";
        public const string DOWNLOAD_SUCCESS_LEFT_BTN_TEXT = "Go to World Page";
        public const string DOWNLOAD_SUCCESS_RIGHT_BTN_TEXT = "Dismiss";
        public const string DOWNLOAD_SUCCESS_LEFT_BTN_TEXT_F = "Go to Friend Page";

        public static readonly Vector2 INVITE_BUTTON_POS = new Vector2(1470f, -630f);
        public static readonly Vector2 WORLD_BUTTON_POS = new Vector2(200f, -188f);
        public static readonly Vector2 FRIEND_BUTTON_POS = new Vector2(215f, -230f);
        public const float FRIEND_PANEL_YPOS = -350f;
        public const float FRIEND_PANEL_YSCALE = 1.1f;
        public const float SOCIAL_PANEL_YPOS = 384f;
    }
}