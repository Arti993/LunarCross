using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States;
using Reflex.Attributes;

namespace UI
{
    public class GameCompleteWindow : UiWindow
    {
        private IUiStateMachine _uiStateMachine;

        [Inject]
        private void Construct(IUiStateMachine uiStateMachine)
        {
            _uiStateMachine = uiStateMachine;
        }
        
        public void OpenLeaderBoard()
        {
            _uiStateMachine.SetState<UiStateLeaderboard>();
        }
    }
}
