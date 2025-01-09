using Data;
using Infrastructure;
using Infrastructure.Services.FocusTest;
using Infrastructure.Services.GameProgress;
using Infrastructure.Services.ScreenFader;
using Reflex.Attributes;

namespace UI
{
    public class MenuEscapeWindow : UiWindow
    {
        private IFocusTestStateChanger _focusTestStateChanger;
        private IGameProgress _gameProgress;
        private IScreenFader _screenFader;

        [Inject]
        private void Construct(IFocusTestStateChanger focusTestStateChanger, IGameProgress gameProgress,
             IScreenFader screenFader)
        {
            _focusTestStateChanger = focusTestStateChanger;
            _gameProgress = gameProgress;
            _screenFader = screenFader;
        }
        
        public void FromGamePlayToMainMenu()
        {
            _gameProgress.ClearSelectedLevel();

            _focusTestStateChanger.DisablePauseMenuOpening();

            _screenFader.FadeOutAndLoadScene((int) SceneIndex.MainMenu);
        }
    }
}