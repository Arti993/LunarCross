using Data;
using Infrastructure.Services.FocusTest;
using Infrastructure.Services.GameProgress;
using Infrastructure.Services.ScreenFader;
using Reflex.Extensions;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MenuEscapeWindow : UiWindow
    {
        protected IFocusTestStateChanger FocusTestStateChanger;
        protected IGameProgress GameProgress;
        protected IScreenFader ScreenFader;
        
        protected override void Construct()
        {
            base.Construct();
            
            FocusTestStateChanger = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IFocusTestStateChanger>();

            GameProgress = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IGameProgress>();

            ScreenFader = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IScreenFader>();
        }
        
        public void FromGamePlayToMainMenu()
        {
            GameProgress.ClearSelectedLevel();

            FocusTestStateChanger.DisablePauseMenuOpening();

            ScreenFader.FadeOutAndLoadScene((int) SceneIndex.MainMenu);
        }
    }
}