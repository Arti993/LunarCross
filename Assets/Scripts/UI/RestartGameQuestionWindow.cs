using Data;
using Infrastructure.Services.GameProgress;
using Infrastructure.Services.ScreenFader;
using Infrastructure.UIStateMachine.States;
using Reflex.Extensions;
using UnityEngine.SceneManagement;

namespace UI
{
    public class RestartGameQuestionWindow : UiWindow
    {
        private IGameProgress _gameProgress;
        private IScreenFader _screenFader;
        
        protected override void Construct()
        {
            base.Construct();
            
            _gameProgress = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IGameProgress>();

            _screenFader = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IScreenFader>();
        }
        
        public void OnYesButtonClick()
        {
            _gameProgress.ClearSaves();

            _screenFader.FadeOutAndLoadScene((int) SceneIndex.Gameplay);
        }

        public void OnNoButtonClick()
        {
            UiStateMachine.SetState<UiStateMainMenu>();
        }
    }
}
