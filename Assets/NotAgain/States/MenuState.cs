using System.Threading.Tasks;
using NotAgain.Core.StateManager;
using NotAgain.Core.UI.UIWindow;
using NotAgain.UI.UIWindows;
using NotAgain.Utils;

namespace NotAgain.States
{
    public class MenuState : IState
    {
        public Task OnStateEnter()
        {
            return ServiceLocator.Get<UIWindowManager>().Open<MainMenuWindow>(UIWindowID.MAIN_MENU);
        }

        public Task OnStateExit()
        {
            return ServiceLocator.Get<UIWindowManager>().CloseCurrent(UIWindowID.MAIN_MENU);
        }
    }
}