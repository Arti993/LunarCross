public class RestartGameQuestionWindow : UIWindow
{
    private void Awake()
    {
        PanelIntro();
    }

    public void OnYesButtonClick()
    {
        AllServicesContainer.Instance.GetService<IGameProgress>().ClearSaves();
        
        PanelOutro();
    }
    
    public void OnNoButtonClick()
    {
        PanelOutro();
    }
}
