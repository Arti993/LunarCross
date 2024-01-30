public interface IEntityStateSwitcher 
{
    public void SwitchState<T>() where T : EntityBaseState;
}
