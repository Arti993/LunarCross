public class MainMenuButton : SimpleButton
{
    public void GoToMainMenu()
    {
        DIServicesContainer.Instance.GetService<IScenesLoader>().LoadMainMenuScene();
    }
}
