using Harmony;
using MelonLoader;
using System.Linq;
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
        static void Postfix(ApiWorld __0) => WorldButton.UpdateText(__0);
    }

    class SetupUserInfoPatch
    {
        public static void Patch()
        {
            Main.harmonyInstance.Patch(typeof(PageUserInfo).GetMethods().Where(m => m.ReturnType == typeof(void)
                && m.GetParameters().Length == 2
                && m.GetParameters()[0].ParameterType == typeof(string)
                && m.GetParameters()[1].ParameterType == typeof(bool)).ToList()[1],
                new HarmonyMethod(typeof(SetupUserInfoPatch).GetMethod(nameof(Postfix))
            ));
        }

        public static void Postfix(ref string __0)
        {
            if (__0.Equals("Join"))
                MelonCoroutines.Start(FriendButton.UpdateText());
        }
    }


    class SetupSocialMenuPatch
    {
        public static void Patch()
        {
            Main.harmonyInstance.Patch(typeof(PageUserInfo).GetMethods().Single(
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
            if (!__0.isFriend)
                FriendButton.button.SetActive(false);
            else if (Utilities.isInSameWorld(__0))
                FriendButton.button.SetActive(false);


        }
    }


}
