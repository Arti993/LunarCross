public class MenuEscapeWindow : UIWindow
{
    public void GoToMainMenu()
    {
        DIServicesContainer.Instance.GetService<IGameProgress>().ClearSelectedLevel();
        
        DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateNoWindow>();
        
        DIServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene((int)SceneIndex.MainMenu);
    }
}