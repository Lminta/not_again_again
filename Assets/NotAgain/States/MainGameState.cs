using System.Threading.Tasks;
using NotAgain.Core.Levels;
using NotAgain.Core.StateManager;
using NotAgain.Utils;

namespace NotAgain.States
{
    public class MainGameState : IState
    {
        public Task OnStateEnter()
        {
            return ServiceLocator.Get<LevelManager>().LoadScene(SceneID.MAIN_GAME_SCENE);
        }

        public Task OnStateExit()
        {
            return ServiceLocator.Get<LevelManager>().UnloadScene(SceneID.MAIN_GAME_SCENE);
        }
    }
}