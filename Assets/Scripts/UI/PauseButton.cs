public class PauseButton : CustomButton
{
    protected override void OnEnable()
    {
        base.OnEnable();

        Clicked += OnClicked;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        
        Clicked -= OnClicked;
    }

    private void OnClicked()
    {
        DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStatePauseMenu>();
    }
}
