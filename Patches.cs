using System;
using Il2CppSystem.Collections;
using System.Linq;
using System.Reflection;
using System.Text;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using Harmony;
using Il2CppSystem.Collections.Generic;
using Il2CppSystem.Diagnostics.Tracing;
using MelonLoader;
using UnhollowerBaseLib;
using UnityEngine.UI;
using VRC.Core;
using VRC.SDKInternal;
using VRC.UI;
using WorldPredownload.Cache;
using WorldPredownload.DownloadManager;
using WorldPredownload.UI;
using Boolean = Il2CppSystem.Boolean;
using Byte = Il2CppSystem.Byte;
using InfoType = VRC.UI.PageUserInfo.EnumNPublicSealedvaNoOnOfSeReBlInFa9vUnique;
using ListType = UiUserList.EnumNPublicSealedvaNoInFrOnOfSeInFa9vUnique;
using String = Il2CppSystem.String;

namespace WorldPredownload
{
    [HarmonyPatch(typeof(NetworkManager), "OnJoinedRoom")]
    class OnJoinedRoomPatch
    {
        static void Prefix() => MelonCoroutines.Start(CacheManager.UpdateDirectoriesBackground());
    }

    [HarmonyPatch(typeof(NetworkManager), "OnLeftRoom")]
    class OnLeftRoomPatch
    {
        static void Prefix() => WorldDownloadManager.CancelDownload();
    }

    [HarmonyPatch(typeof(PageWorldInfo), "Method_Public_Void_ApiWorld_ApiWorldInstance_Boolean_Boolean_0")]
    class SetupWorldInfoPatch
    {
        static void Postfix(ApiWorld __0 = null)
        {
            if (__0 != null)
            {
                MelonLogger.Log("Not Null");
                WorldButton.UpdateText(__0);
            }
            else MelonLogger.Log("Null");
        }
    }
    
    class SetupSocialMenuPatch
    {
        public static void Patch()
        {
            WorldPredownload.HarmonyInstance.Patch(typeof(PageUserInfo).GetMethods().Single(
                    m => m.ReturnType == typeof(void)
                         && m.GetParameters().Length == 3
                         && m.GetParameters()[0].ParameterType == typeof(APIUser)
                         && m.GetParameters()[1].ParameterType == typeof(InfoType)
                         && m.GetParameters()[2].ParameterType == typeof(ListType)
                ),
                null,
                new HarmonyMethod(typeof(SetupSocialMenuPatch).GetMethod(nameof(Postfix)))
            );
        }

        public static void Postfix(APIUser __0) //, InfoType __1, ListType __2 = ListType.None
        {
            if(__0.location != null) Logger.Log(__0.location);
            if (!__0.isFriend || 
                Utilities.isInSameWorld(__0) || 
                __0.location.ToLower().Equals("private") || 
                __0.location.ToLower().Equals("offline")
            )
                FriendButton.button.SetActive(false);
            else
            {
                FriendButton.button.SetActive(true);
                MelonCoroutines.Start(FriendButton.UpdateText());
            }


        }
    }

}