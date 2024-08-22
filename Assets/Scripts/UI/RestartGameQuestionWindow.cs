public class RestartGameQuestionWindow : UIWindow
{
    private void Awake()
    {
        PanelIntro();
    }

    public void OnYesButtonClick()
    {
        DIServicesContainer.Instance.GetService<IGameProgress>().ClearSaves();

        DIServicesContainer.Instance.GetService<IScenesLoader>().LoadGameplayScene();
    }
    
    public void OnNoButtonClick()
    {
        PanelOutro();
    }
}
