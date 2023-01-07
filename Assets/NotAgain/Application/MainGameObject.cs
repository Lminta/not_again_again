using NotAgain.Core.UI.UIWindow;
using NotAgain.UI.UIWindiws;
using NotAgain.Utils;
using UnityEngine;

namespace NotAgain.Application
{
    public class MainGameObject : MonoBehaviour
    {
        void Start()
        {
            ApplicationLoad.SetupServices();
            ServiceLocator.Get<UIWindowManager>().Open<MainMenu>(UIWindowID.MAIN_MENU).IsSuccess();
        }

        void OnDestroy()
        {
            ApplicationLoad.ReleaseServices();
        }
    }
}