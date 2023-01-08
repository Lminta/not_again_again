using System.Threading.Tasks;

namespace NotAgain.Core.StateManager
{
    public static class StateManager
    {
        static IState _currentState = null;

        public static async Task EnterState(IState state)
        {
            if (_currentState != null)
            {
                if (state.GetType() == _currentState.GetType())
                    return;
                
                await _currentState.OnStateExit();
            }
            
            await state.OnStateEnter();
            _currentState = state;
        }
    }
}