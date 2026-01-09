using UnityEngine;

namespace CleverTap
{
    public static class CleverTapToast
    {
        public static void Show(string message)
        {
#if UNITY_ANDROID
            AndroidToast.Show(message);
#elif UNITY_IOS
            IOSToast.Show(message);
#else
            Debug.Log("[CleverTapToast] " + message);
#endif
        }
    }
}
