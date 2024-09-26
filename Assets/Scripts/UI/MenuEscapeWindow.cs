public class MenuEscapeWindow : UIWindow
{
    public void FromGamePlayToMainMenu()
    {
        DIServicesContainer.Instance.GetService<IGameProgress>().ClearSelectedLevel();
        
        DIServicesContainer.Instance.GetService<IFocusTestStateChanger>().DisablePauseMenuOpening();

        DIServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene((int)SceneIndex.MainMenu);
    }
}