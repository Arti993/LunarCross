public interface IUiStateMachine : IService
{
    public void AddState(UiStateMachineState state);
    public void SetState<T>() where T : UiStateMachineState;
}
