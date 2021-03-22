using System;
using MelonLoader;
using UnityEngine;

namespace WorldPredownload.Components
{
    class SelectedNotificationListener : MonoBehaviour
    {
        public SelectedNotificationListener(IntPtr obj0) : base(obj0) {}
        public static VRCUiContentButton selectedContentButton;
        private void OnEnable() => selectedContentButton = transform.parent.GetComponent<VRCUiContentButton>();
    }
}