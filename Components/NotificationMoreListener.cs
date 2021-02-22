using System;
using UnityEngine;
using WorldPredownload.UI;

namespace WorldPredownload.Components
{
    public class NotificationMoreListener : MonoBehaviour
    {
        public NotificationMoreListener(IntPtr obj0) : base(obj0) {}

        private void OnEnable() => InviteButton.UpdateText();
    }
}