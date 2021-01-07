using System;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace WorldPredownload.Components
{
    //Credit to knah. He's a smart guy https://tinyurl.com/y2f45mvp
    class EnableDisableListener : MonoBehaviour
    {
        [method:HideFromIl2Cpp]
        public event Action? OnEnabled;
        
        [method:HideFromIl2Cpp]
        public event Action? OnDisabled;

        public EnableDisableListener(IntPtr obj0) : base(obj0) {}

        private void OnEnable() => OnEnabled?.Invoke();


        private void OnDisable() => OnDisabled?.Invoke();
    }
}