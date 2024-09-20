public class RestartGameQuestionWindow : UIWindow
{
    private void Awake()
    {
        PanelIntro();
    }

    public void OnYesButtonClick()
    {
        DIServicesContainer.Instance.GetService<IGameProgress>().ClearSaves();

        DIServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene((int)SceneIndex.Gameplay);
    }
    
    public void OnNoButtonClick()
    {
        PanelOutro();
    }
}
