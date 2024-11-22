using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.UIStateMachine
{
    public class UiStateMachine : IUiStateMachine
    {
        private UiStateMachineState _currentState;

        private List<UiStateMachineState> _states = new List<UiStateMachineState>();

        public void AddState(UiStateMachineState state)
        {
            _states.Add(state);
        }

        public void SetState<T>() where T : UiStateMachineState
        {
            var newState = _states.FirstOrDefault(state => state is T);

            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
    }
}
