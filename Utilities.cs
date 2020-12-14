using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Transmtn.DTO.Notifications;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using OnDownloadComplete = AssetBundleDownloadManager.MulticastDelegateNInternalSealedVoObUnique;
using OnDownloadProgress = AssetBundleDownloadManager.MulticastDelegateNInternalSealedVoUnUnique;
using OnDownloadError = AssetBundleDownloadManager.MulticastDelegateNInternalSealedVoStObStUnique;
using UnpackType = AssetBundleDownloadManager.EnumNInternalSealedva3vUnique;
using VRC.Core;
using UnityEngine.EventSystems;
using WorldPredownload.UI;
using UnityEngine.UI;

namespace WorldPredownload
{
    public static class Utilities
    {
        private static List<string> downloadWorldKeyWords = new List<string>(new string[] { "vrcw", "Worlds", "Failed to parse world '", "' UnityVersion '" });


        private static DownloadWorldDelegate downloadWorldDelegate;

        private static ClearErrorsDelegate clearErrorsDelegate;

        private static ShowDismissPopupDelegate showDismissPopupDelegate;

        private static ShowOptionsPopupDelegate showOptionsPopupDelegate;

        private static PushUIPageDelegate pushUIPageDelegate;

        private static DownloadWorldDelegate GetDownloadWorldDelegate
        {
            get
            {
                if (downloadWorldDelegate != null) return downloadWorldDelegate;
                foreach (MethodInfo methodInfo in typeof(AssetBundleDownloadManager).GetMethods().Where(m => 
                            m.Name.StartsWith("Method_Internal_Void_") 
                            && CheckXrefStrings(m, downloadWorldKeyWords))
                        )
                {
                    downloadWorldDelegate = (DownloadWorldDelegate)Delegate.CreateDelegate(
                    typeof(DownloadWorldDelegate),
                    AssetBundleDownloadManager.prop_AssetBundleDownloadManager_0,
                    methodInfo);
                    return downloadWorldDelegate;
                }
                return null;
            }
        }

        private static ClearErrorsDelegate GetClearErrorsDelegate
        {
            get
            {
                if (clearErrorsDelegate != null) return clearErrorsDelegate;
                MethodInfo clearErrors = typeof(AssetBundleDownloadManager).GetMethods().Where(
                    m => 
                        m.Name.StartsWith("Method_Internal_Void_") 
                        && !m.Name.Contains("PDM")
                        && m.ReturnType == typeof(void) 
                        && (m.GetParameters().Length == 0)
                    ).First();
                clearErrorsDelegate = (ClearErrorsDelegate)Delegate.CreateDelegate(
                        typeof(ClearErrorsDelegate),
                        AssetBundleDownloadManager.prop_AssetBundleDownloadManager_0,
                        clearErrors
                    );
                return clearErrorsDelegate;
            }
        }



        private static ShowDismissPopupDelegate GetShowDismissPopupDelegate
        {
            get
            {
                if (showDismissPopupDelegate != null) return showDismissPopupDelegate;
                MethodInfo popupMethod = typeof(VRCUiPopupManager).GetMethods(BindingFlags.Public | BindingFlags.Instance).Single(
                    m => 
                    m.GetParameters().Length == 5 
                    && m.XRefScanFor("Popups/StandardPopupV2")
                );

                showDismissPopupDelegate = (ShowDismissPopupDelegate)Delegate.CreateDelegate(
                            typeof(ShowDismissPopupDelegate),
                            VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0,
                            popupMethod
                );
                return showDismissPopupDelegate;
            }
        }

        private static ShowOptionsPopupDelegate GetShowOptionsPopupDelegate
        {
            get
            {
                if (showOptionsPopupDelegate != null) return showOptionsPopupDelegate;
                MethodInfo popupMethod = typeof(VRCUiPopupManager).GetMethods(BindingFlags.Public | BindingFlags.Instance).Single(
                    m => m.GetParameters().Length == 7 && m.XRefScanFor("Popups/StandardPopupV2"));

                showOptionsPopupDelegate = (ShowOptionsPopupDelegate)Delegate.CreateDelegate(
                    typeof(ShowOptionsPopupDelegate),
                    VRCUiPopupManager.field_Private_Static_VRCUiPopupManager_0,
                    popupMethod
                );
                return showOptionsPopupDelegate;
            }
        }

        private static PushUIPageDelegate GetPushUIPageDelegate
        {
            get
            {
                if (pushUIPageDelegate != null) return pushUIPageDelegate;
                MethodInfo pushPageMethod = typeof(VRCUiManager).GetMethods().Single(
                    m => m.GetParameters().Length == 1
                    && m.GetParameters()[0].ParameterType == typeof(VRCUiPage)
                    && !m.Name.Contains("PDM")
                    && m.ReturnType == typeof(VRCUiPage)
                );

                pushUIPageDelegate = (PushUIPageDelegate)Delegate.CreateDelegate(
                    typeof(PushUIPageDelegate),
                    VRCUiManager.prop_VRCUiManager_0,
                    pushPageMethod
                );
                return pushUIPageDelegate;
            }
        }



        public static void DownloadApiWorld(ApiWorld world, OnDownloadProgress onProgress, OnDownloadComplete onSuccess, OnDownloadError onError, bool bypassDownloadSizeLimit, UnpackType unpackType)
        {
            GetDownloadWorldDelegate(world, onProgress, onSuccess, onError, bypassDownloadSizeLimit, unpackType);
        }

        public static void ClearErrors()
        {
            GetClearErrorsDelegate();
        }

        public static void ShowOptionPopup(string title, string body, string leftButtonText, Il2CppSystem.Action leftButtonAction, string rightButtonText, Il2CppSystem.Action rightButtonAction)
        {
            GetShowOptionsPopupDelegate(title, body, leftButtonText, leftButtonAction, rightButtonText, rightButtonAction);
        }

        public static void ShowDismissPopup(string title, string body, string middleButtonText, Il2CppSystem.Action buttonAction)
        {
            GetShowDismissPopupDelegate(title, body, middleButtonText, buttonAction);
        }

        public static void ShowPage(VRCUiPage page)
        {
            GetPushUIPageDelegate(page);
        }

        public static AssetBundleDownloadManager GetAssetBundleDownloadManager()
        {
            return AssetBundleDownloadManager.prop_AssetBundleDownloadManager_0;
        }

        public static void HideCurrentPopup()
        {
            VRCUiManager.prop_VRCUiManager_0.HideScreen("POPUP");
        }

        public static Notification GetSelectedNotification()
        {
            return QuickMenu.prop_QuickMenu_0.field_Private_Notification_0;
        }

        public static GameObject CloneGameObject(string pathToGameObject, string pathToParent)
        {
            return GameObject.Instantiate(GameObject.Find(pathToGameObject).transform, GameObject.Find(pathToParent).transform).gameObject;
        }

        public static void DeselectClickedButton(GameObject button)
        {
            if (EventSystem.current.currentSelectedGameObject == button)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

        public static void GoToWorld(ApiWorld apiWorld, string tags)
        {
            if (Main.tryUseAdvancedInvitePopup && Main.AdvancedInvites)
            {
                try
                {
                    AdvancedInvites.InviteHandler.HandleInvite(InviteButton.notification);
                }
                catch
                {
                    MelonLogger.LogError("Unable to execute Advanced Invite's Invite Handler Func");
                }
            }
            else
                new PortalInternal().Method_Private_Void_String_String_PDM_0(apiWorld.id, tags);
        }

        public static bool isInSameWorld(APIUser user)
        {
            if (user.location.Contains(RoomManager.field_Internal_Static_ApiWorld_0.id))
                return true;
            else
                return false;
        }


        public static void Swap<T>(ref T left, ref T right)
        {
            T temp = left;
            left = right;
            right = temp;
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static bool HasMod(string modName) =>
            MelonHandler.Mods.Any(mod => mod.Info.Name.Equals(modName));

        public static bool CheckXrefStrings(MethodBase m, List<string> keywords)
        {
            try
            {
                foreach (string keyword in keywords)
                {

                    if (!XrefScanner.XrefScan(m).Any(
                    instance => instance.Type == XrefType.Global && instance.ReadAsObject() != null && instance.ReadAsObject().ToString()
                                   .Equals(keyword, StringComparison.OrdinalIgnoreCase)))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch { }
            return false;
        }

        public static bool XRefScanFor(this MethodBase methodBase, string searchTerm)
        {
            return XrefScanner.XrefScan(methodBase).Any(
                xref => xref.Type == XrefType.Global && xref.ReadAsObject()?.ToString().IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private static bool checkXrefNoStrings(MethodBase m)
        {
            try
            {
                foreach (XrefInstance instance in XrefScanner.XrefScan(m))
                {
                    if (instance.Type != XrefType.Global || instance.ReadAsObject() == null) continue;
                    return false;
                }
                return true;
            }
            catch (Exception e) { MelonLogger.Log("For loop failed:" + e); }
            return false;

        }

        public static void QueueHudMessage(string msg)
        {
            VRCUiManager.prop_VRCUiManager_0.field_Private_List_1_String_0.Add(msg);
        }


        private delegate VRCUiPage PushUIPageDelegate(VRCUiPage page); 

        private delegate void ShowOptionsPopupDelegate(
            string title, 
            string body, 
            string leftButtonText, 
            Il2CppSystem.Action leftButtonAction, 
            string rightButtonText, 
            Il2CppSystem.Action rightButtonAction, 
            Il2CppSystem.Action<VRCUiPopup> additionalSetup = null
        );

        private delegate void ShowDismissPopupDelegate(
            string title, 
            string body, 
            string middleButtonText, 
            Il2CppSystem.Action middleButtonAction, 
            Il2CppSystem.Action<VRCUiPopup> additionalSetup = null
        );

        private delegate void ClearErrorsDelegate();

        private delegate void DownloadWorldDelegate(ApiWorld world, OnDownloadProgress onProgress, OnDownloadComplete onSuccess, OnDownloadError onError, bool bypassDownloadSizeLimit, UnpackType unpackType);

    }
}
