using MelonLoader;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Transmtn.DTO.Notifications;
using UnhollowerRuntimeLib.XrefScans;
using UnityEngine;
using AssetBundleDownload = CustomYieldInstructionPublicObAsByStInStCoBoObInUnique;
using OnDownloadComplete = AssetBundleDownloadManager.MulticastDelegateNInternalSealedVoObUnique;
using OnDownloadProgress = AssetBundleDownloadManager.MulticastDelegateNInternalSealedVoUnUnique;
using OnDownloadError = AssetBundleDownloadManager.MulticastDelegateNInternalSealedVoStObStUnique;
using LoadErrorReason = EnumPublicSealedvaNoMiFiUnCoSeAsDuAsUnique;
using UnpackType = AssetBundleDownloadManager.EnumNInternalSealedva3vUnique;
using UnityEngine.UI;
using VRC.Core;
using WorldPredownload.UI;

namespace WorldPredownload
{
    public static class Utilities
    {
        private static List<string> downloadWorldKeyWords = new List<string>(new string[] { "vrcw", "Worlds", "Failed to parse world '", "' UnityVersion '" });


        private static DownloadWorldDelegate downloadWorldDelegate;

        private static ClearErrorsDelegate clearErrorsDelegate;

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

        public static void DownloadApiWorld(ApiWorld world, OnDownloadProgress onProgress, OnDownloadComplete onSuccess, OnDownloadError onError, bool bypassDownloadSizeLimit, UnpackType unpackType)
        {
            GetDownloadWorldDelegate(world, onProgress, onSuccess, onError, bypassDownloadSizeLimit, unpackType);
        }

        public static void ClearErrors()
        {
            GetClearErrorsDelegate();
        }

        public static AssetBundleDownloadManager GetAssetBundleDownloadManager()
        {
            return AssetBundleDownloadManager.prop_AssetBundleDownloadManager_0;
        }

        public static Notification GetSelectedNotification()
        {
            return QuickMenu.prop_QuickMenu_0.field_Private_Notification_0;
        }

        public static GameObject CloneGameObject(string pathToGameObject, string pathToParent)
        {
            return GameObject.Instantiate(GameObject.Find(pathToGameObject).transform, GameObject.Find(pathToParent).transform).gameObject;
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

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
       
        private delegate void ClearErrorsDelegate();

        private delegate void DownloadWorldDelegate(ApiWorld world, OnDownloadProgress onProgress, OnDownloadComplete onSuccess, OnDownloadError onError, bool bypassDownloadSizeLimit, UnpackType unpackType);

    }
}
