using Infrastructure.Services;

namespace Infrastructure.UIStateMachine
{
    public interface IUiStateMachine : IService
    {
        public void AddState(UiStateMachineState state);
        public void SetState<T>() where T : UiStateMachineState;
        public void ExitCurrentState();
    }
}
