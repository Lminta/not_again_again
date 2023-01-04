using System.Threading.Tasks;
using UnityEngine;

namespace NotAgain.Core.UI.UIWindow
{
    public abstract class UIWindow : MonoBehaviour
    {
        public UIWindowID UIWindowID { get; private set; }

        public virtual Task OnCreate(UIWindowID uiWindowID)
        {
            UIWindowID = uiWindowID;
            return Task.CompletedTask;
        }

        public abstract Task Initialize();

        public abstract Task DeInitialize();
        
        public virtual Task OnOpen(UIWindowID previous)
        {
            gameObject.SetActive(true);
            return Task.CompletedTask;
        }

        public virtual Task OnClose(UIWindowID next)
        {
            gameObject.SetActive(false);
            return Task.CompletedTask;
        }
    }
}