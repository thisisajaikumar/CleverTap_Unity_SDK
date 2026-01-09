#if UNITY_IOS
using System.Runtime.InteropServices;
using UnityEngine;

namespace CleverTap
{
    internal static class IOSToast
    {
        [DllImport("__Internal")]
        private static extern void _ShowIOSAlert(string message);

        public static void Show(string message)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                _ShowIOSAlert(message);
            }
        }
    }
}
#endif
