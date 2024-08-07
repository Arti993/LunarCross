public class ContinueButton : SimpleButton
{
    protected override void Start()
    {
        AllServicesContainer.Instance.GetService<IScreenFader>().FadingStart += OnScreenFaderEnable;
    }
}
