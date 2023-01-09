using System.Collections.Generic;
using System.Threading.Tasks;
using NotAgain.Core.Levels;
using NotAgain.Core.StateManager;
using NotAgain.Core.UI.UIWindow;
using NotAgain.UI.UIWindows;
using NotAgain.Utils;

namespace NotAgain.States
{
    public class MainGameState : IState
    {
        public Task OnStateEnter()
        {
            var tasks = new List<Task>(2)
            {
                ServiceLocator.Get<UIWindowManager>().Open<MainGameWindow>(UIWindowID.MAIN_GAME),
                ServiceLocator.Get<LevelManager>().LoadScene(SceneID.MAIN_GAME_SCENE)
            };
            return Task.WhenAll(tasks);
        }

        public Task OnStateExit()
        {
            var tasks = new List<Task>(2)
            {
                ServiceLocator.Get<UIWindowManager>().CloseCurrent(UIWindowID.MAIN_GAME),
                ServiceLocator.Get<LevelManager>().UnloadScene(SceneID.MAIN_GAME_SCENE)
            };
            return Task.WhenAll(tasks);
        }
    }
}