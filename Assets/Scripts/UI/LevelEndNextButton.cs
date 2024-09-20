public class LevelEndNextButton : CustomButton
{
    protected override void OnEnable()
    {
        DIServicesContainer.Instance.GetService<IScreenFader>().FadingComplete += OnScreenFaderDisable;
        DIServicesContainer.Instance.GetService<IScreenFader>().FadingStart += OnScreenFaderEnable;
    }

    public void SetInterractable()
    {
        IsClickable = true;
    }
    
    public void SetNotInterractable()
    {
        IsClickable = false;
    }
}
