public class NextLevelButton : SimpleButton
{
    protected override void Start()
    {
        DIServicesContainer.Instance.GetService<IScreenFader>().FadingStart += OnScreenFaderEnable;
    }
}
