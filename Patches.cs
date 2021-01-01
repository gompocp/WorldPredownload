using System;
using System.Linq;
using System.Reflection;
using BestHTTP.SecureProtocol.Org.BouncyCastle.Math.Raw;
using Harmony;
using Il2CppSystem.Xml.Schema;
using MelonLoader;
using UnityEngine.UI;
using VRC.Core;
using VRC.SDKInternal;
using VRC.UI;
using WorldPredownload.Cache;
using WorldPredownload.DownloadManager;
using WorldPredownload.UI;
using InfoType = VRC.UI.PageUserInfo.EnumNPublicSealedvaNoOnOfSeReBlInFa9vUnique;
using ListType = UiUserList.EnumNPublicSealedvaNoInFrOnOfSeInFa9vUnique;

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
        static void Postfix(ApiWorld __0) => WorldButton.UpdateText(__0);
    }
    
    class SetupAcceptNotificationPatch
    {
        public static void Patch()
        {
            MethodInfo acceptNotificationMethod = typeof(QuickMenu).GetMethods(BindingFlags.Public | BindingFlags.Instance).First(
                   m => m.GetParameters().Length == 0 && m.XRefScanFor("AcceptNotification"));

            WorldPredownload.HarmonyInstance.Patch(
                acceptNotificationMethod,
                new HarmonyMethod(typeof(SetupAcceptNotificationPatch).GetMethod(nameof(Prefix))));
        }

        public static bool Prefix()
        {   
            if(!Utilities.GetSelectedNotification().notificationType.Equals("invite", StringComparison.OrdinalIgnoreCase)) return true;
            if (ModSettings.overrideInviteAcceptButton)
            {
                InviteButton.button.GetComponent<Button>().onClick.Invoke();
                return false;
            }
            return true;

        }
    }
    
    
    class SetupAdvancedInvitesPatch
    {
        public static void Patch()
        {
            //                                                                                                                     GetType() was being stupid so yeah...          
            var acceptNotificationMethod = MelonHandler.Mods.First(m => m.Info.Name.Equals("AdvancedInvites")).Assembly.GetTypes().Single(t => t.Name.Equals("AdvancedInviteSystem")).GetMethod("AcceptNotificationPatch", BindingFlags.NonPublic | BindingFlags.Static);
            WorldPredownload.HarmonyInstance.Patch(acceptNotificationMethod, new HarmonyMethod(typeof(SetupAdvancedInvitesPatch).GetMethod(nameof(Prefix))));
        }

        public static bool Prefix()
        {
            
            if (ModSettings.overrideInviteAcceptButton)
            {
                InviteButton.button.GetComponent<Button>().onClick.Invoke();
                return false;
            }
            
            return true;
        }
    }
    

    
    class SetupUserInfoPatch
    {
        public static void Patch()
        {
            WorldPredownload.HarmonyInstance.Patch(typeof(PageUserInfo).GetMethods().Where(m => m.ReturnType == typeof(void)
                && m.GetParameters().Length == 2
                && m.GetParameters()[0].ParameterType == typeof(string)
                && m.GetParameters()[1].ParameterType == typeof(bool)).ToList()[1],
                new HarmonyMethod(typeof(SetupUserInfoPatch).GetMethod(nameof(Postfix))
            ));
        }

        public static void Postfix(ref string __0)
        {
            if (__0.Equals("Join")) ;
            //MelonCoroutines.Start(FriendButton.UpdateText());
            else
                FriendButton.button.SetActive(false);
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
            if (!ModSettings.overrideSocialPageButton)
            {
                Logger.Log(__0.location);
                if (!__0.isFriend || Utilities.isInSameWorld(__0) || __0.location.ToLower().Equals("private"))
                    FriendButton.button.SetActive(false);
                else
                {
                    FriendButton.button.SetActive(true);
                    MelonCoroutines.Start(FriendButton.UpdateText());
                }
            }
            else 
                FriendButton.button.SetActive(false);


        }
    }
    
    
}