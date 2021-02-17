using System;
using MelonLoader;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace WorldPredownload.Components
{
    class SelectedNotificationListener : MonoBehaviour
    {
        public SelectedNotificationListener(IntPtr obj0) : base(obj0) {}
        public static VRCUiContentButton selectedContentButton;
        private void OnEnable() //Awake doesnt work because VRChat does some voodoo stuff and breaks the listener
        {
            selectedContentButton = transform.parent.GetComponent<VRCUiContentButton>();
            /*
            //It just works™
            selectedContentButton = transform.parent.GetComponent<VRCUiContentButton>();
            Button.ButtonClickedEvent onClick = GetComponent<Button>().onClick;
            try
            {
                onClick.RemoveListener(action); //Super lazy way to make sure it isn't added twice
            } catch {}

            onClick.AddListener(action);
            */
        }
        
        public UnityAction action = new Action(delegate
        {
            MelonLogger.Msg($"Selected more options for notification:{selectedContentButton.field_Public_String_0}");
        });


    }
}