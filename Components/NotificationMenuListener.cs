using System;
using System.Runtime.InteropServices;
using System.Reflection;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace WorldPredownload.Components
{
    class NotificationMenuListener : MonoBehaviour
    {
        public Delegate ReferencedDelegate;
        public IntPtr MethodInfo;
        public NotificationMenuListener(IntPtr obj0) : base(obj0)
        {
            ClassInjector.DerivedConstructorBody(this);
        }

        public NotificationMenuListener(Delegate referencedDelegate, IntPtr methodInfo) : base(ClassInjector.DerivedConstructorPointer<NotificationMenuListener>())
        {
            ClassInjector.DerivedConstructorBody(this);

            ReferencedDelegate = referencedDelegate;
            MethodInfo = methodInfo;
        }
        ~NotificationMenuListener()
        {
            Marshal.FreeHGlobal(MethodInfo);
            MethodInfo = IntPtr.Zero;
            ReferencedDelegate = null;
        }
        private void OnAwake()
        {
            if (OnAwakeMethod == null) return;
            OnAwakeMethod.Invoke(null, new object[0]);
        }
        private void OnEnable()
        {
            if (OnEnableMethod == null) return;
            OnEnableMethod.Invoke(null, new object[0]);
        }
        private void OnDisable()
        {
            if (OnDisableMethod == null) return;
            OnDisableMethod.Invoke(null, new object[0]);
        }
        public static MethodInfo OnEnableMethod { get; set; }
        public static MethodInfo OnDisableMethod { get; set; }
        public static MethodInfo OnAwakeMethod { get; set; }
    }
}