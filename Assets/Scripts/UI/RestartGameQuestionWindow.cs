using Data;
using Infrastructure;
using Infrastructure.Services.GameProgress;
using Infrastructure.Services.ScreenFader;
using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States;
using Reflex.Attributes;

namespace UI
{
    public class RestartGameQuestionWindow : UiWindow
    {
        private IUiStateMachine _uiStateMachine;
        private IGameProgress _gameProgress;
        private IScreenFader _screenFader;

        [Inject]
        private void Construct(IUiStateMachine uiStateMachine, IGameProgress gameProgress,
            IScreenFader screenFader)
        {
            _uiStateMachine = uiStateMachine;
            _gameProgress = gameProgress;
            _screenFader = screenFader;
        }
        
        public void OnYesButtonClick()
        {
            _gameProgress.ClearSaves();

            _screenFader.FadeOutAndLoadScene((int) SceneIndex.Gameplay);
        }

        public void OnNoButtonClick()
        {
            _uiStateMachine.SetState<UiStateMainMenu>();
        }
    }
}
