using UnityEngine;

namespace NotAgain.UI.UIManager
{
    public abstract class UIWindow : MonoBehaviour
    {
        public virtual void OnOpen()
        {
            gameObject.SetActive(true);
        }

        public virtual void OnClose()
        {
            gameObject.SetActive(false);
        }
    }
}
