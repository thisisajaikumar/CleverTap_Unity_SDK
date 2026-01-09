using UnityEngine;

namespace CleverTap
{
    public class CleverTapToastBehaviour : MonoBehaviour
    {
        public string message;

        public void OnClick()
        {
            CleverTapToast.Show(message);
        }
    }
}
