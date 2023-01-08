using System.Threading.Tasks;

namespace NotAgain.Core.StateManager
{
    public interface IState
    {
        Task OnStateEnter();
        Task OnStateExit();
    }
}