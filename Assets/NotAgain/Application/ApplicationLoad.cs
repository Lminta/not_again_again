using NotAgain.Core.Levels;
using NotAgain.Core.UI.UINotify;
using NotAgain.Core.UI.UIWindow;
using NotAgain.Utils;
using UnityEngine;

namespace NotAgain.Application
{
    public static class ApplicationLoad
    {
        public static void SetupServices()
        {
            ServiceLocator.Set(GameObject.FindObjectOfType<UIWindowManager>());
            ServiceLocator.Set(GameObject.FindObjectOfType<UINotifyManager>());
            ServiceLocator.Set(GameObject.FindObjectOfType<LevelManager>());
        }
        
        public static void ReleaseServices()
        {
            ServiceLocator.Release<UIWindowManager>();
            ServiceLocator.Release<UINotifyManager>();
            ServiceLocator.Release<LevelManager>();
        }
    }
}