#if UNITY_ANDROID
using UnityEngine;

namespace CleverTap
{
    internal static class AndroidToast
    {
        public static void Show(string message)
        {
            if (Application.platform != RuntimePlatform.Android)
            {
                Debug.Log($"[AndroidToast] {message}");
                return;
            }

            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            try
            {
                using (AndroidJavaClass unityPlayer =
                       new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                {
                    AndroidJavaObject activity =
                        unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

                    // CRITICAL: activity can be null during lifecycle changes
                    if (activity == null)
                    {
                        Debug.LogWarning("[AndroidToast] currentActivity is null");
                        return;
                    }

                    activity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                    {
                        try
                        {
                            using (AndroidJavaClass toastClass =
                                   new AndroidJavaClass("android.widget.Toast"))
                            {
                                AndroidJavaObject toast =
                                    toastClass.CallStatic<AndroidJavaObject>(
                                        "makeText",
                                        activity,
                                        message,
                                        toastClass.GetStatic<int>("LENGTH_SHORT")
                                    );

                                toast?.Call("show");
                            }
                        }
                        catch (System.Exception e)
                        {
                            Debug.LogError("[AndroidToast] UI thread error: " + e);
                        }
                    }));
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("[AndroidToast] Failed: " + e);
            }
        }
    }
}
#endif
