using NotAgain.Core.StateManager;
using NotAgain.Core.UI.UIWindow;
using NotAgain.States;
using NotAgain.Utils;

namespace NotAgain.UI.UIWindows
{
    public class MainMenuWindow : UIWindow
    {
        public void OnClick()
        {
            StateManager.EnterState(new MainGameState()).IsSuccess();
        }
    }
}