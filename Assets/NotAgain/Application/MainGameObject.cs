using NotAgain.Core.StateManager;
using NotAgain.Core.UI.UIWindow;
using NotAgain.States;
using NotAgain.Utils;
using UnityEngine;

namespace NotAgain.Application
{
    public class MainGameObject : MonoBehaviour
    {
        void Start()
        {
            ApplicationLoad.SetupServices();
            StateManager.EnterState(new MenuState()).IsSuccess();
        }

        void OnDestroy()
        {
            ApplicationLoad.ReleaseServices();
        }
    }
}