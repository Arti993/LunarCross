using Data;
using Infrastructure;
using Infrastructure.Services.ScreenFader;
using Reflex.Attributes;
using Reflex.Extensions;
using UnityEngine.SceneManagement;

namespace UI
{
    public class BackToMenuButton : CustomButton
    {
        private IScreenFader _screenFader;
        
        protected override void Construct()
        {
            base.Construct();
            
            _screenFader = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IScreenFader>();
        }
        
        public void GoToMainMenu()
        {
            _screenFader.FadeOutAndLoadScene((int) SceneIndex.MainMenu);
        }
    }
}
