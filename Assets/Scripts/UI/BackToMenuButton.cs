using Data;
using Infrastructure;
using Infrastructure.Services.ScreenFader;
using Reflex.Attributes;

namespace UI
{
    public class BackToMenuButton : CustomButton
    {
        private IScreenFader _screenFader;
        
        [Inject]
        private void Construct(IScreenFader screenFader)
        {
            _screenFader = screenFader;
        }
        
        public void GoToMainMenu()
        {
            _screenFader.FadeOutAndLoadScene((int) SceneIndex.MainMenu);
        }
    }
}
