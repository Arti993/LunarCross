using Data;
using Infrastructure;
using Infrastructure.Services.ScreenFader;

namespace UI
{
    public class BackToMenuButton : CustomButton
    {
        public void GoToMainMenu()
        {
            DIServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene((int) SceneIndex.MainMenu);
        }
    }
}
