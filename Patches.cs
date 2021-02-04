using System.Linq;
using Harmony;
using MelonLoader;
using VRC.Core;
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
        static void Postfix(ApiWorld __0 = null) => WorldButton.UpdateText(__0);
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
                         && !m.Name.Contains("PDM")
                ),
                null,
                new HarmonyMethod(typeof(SetupSocialMenuPatch).GetMethod(nameof(Postfix)))
            );
        }

        public static void Postfix(APIUser __0 = null) //, InfoType __1, ListType __2 = ListType.None
        {
            if (__0 == null) return;
            if (__0.location == null)
            {
                FriendButton.button.SetActive(false);
                return;
            }
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
