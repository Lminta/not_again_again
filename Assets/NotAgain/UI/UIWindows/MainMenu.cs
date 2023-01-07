using System.Threading.Tasks;
using NotAgain.Core.Levels;
using NotAgain.Core.UI.UIWindow;
using NotAgain.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace NotAgain.UI.UIWindiws
{
    public class MainMenu : UIWindow
    {
        public void OnClick()
        {
            ServiceLocator.Get<LevelManager>().LoadScene(SceneID.MAIN_GAME_SCENE).IsSuccess();
            ServiceLocator.Get<UIWindowManager>().CloseCurrent().IsSuccess();
        }
    }
}