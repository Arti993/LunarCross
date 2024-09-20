public class MainMenuButton : CustomButton
{
    public void GoToMainMenu()
    {
        DIServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene((int)SceneIndex.MainMenu);
    }
}
