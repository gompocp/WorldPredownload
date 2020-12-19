using System;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace WorldPredownload.Components
{
    class NotificationMenuListener : MonoBehaviour
    {
        [method:HideFromIl2Cpp]
        public event Action? OnEnabled;
        
        [method:HideFromIl2Cpp]
        public event Action? OnDisabled;
        
        public NotificationMenuListener(IntPtr obj0) : base(obj0)
        {
        }

        private void OnEnable()
        {
            OnEnabled?.Invoke();
        }

        private void OnDisable()
        {
            OnDisabled?.Invoke();
        }
    }
}
