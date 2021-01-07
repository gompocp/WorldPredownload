using UnityEngine;
using UnityEngine.UI;

namespace WorldPredownload.UI
{
    public static class WorldDownloadStatus
    {
        public static GameObject gameObject { get; set; }

        private const string PATH_TO_GAMEOBJECT_TO_CLONE = "UserInterface/QuickMenu/ShortcutMenu/PingText";
        private const string PATH_TO_CLONE_PARENT = "UserInterface/QuickMenu/ShortcutMenu";

        public static void Setup()
        {
            gameObject = Utilities.CloneGameObject(PATH_TO_GAMEOBJECT_TO_CLONE, PATH_TO_CLONE_PARENT);
            gameObject.GetRectTrans().SetAnchoredPos(Constants.DWLD_STATUS_POS);
            gameObject.SetActive(true);
            gameObject.SetName(Constants.DOWNLOAD_STATUS_NAME);
            gameObject.GetComponent<VRC.UI.DebugDisplayText>().enabled = false;
            gameObject.GetComponent<Text>().alignment = TextAnchor.UpperRight;
            gameObject.SetText(Constants.DOWNLOAD_STATUS_IDLE_TEXT);
        }
    }
}