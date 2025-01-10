using Infrastructure.UIStateMachine.States;

namespace UI
{
    public class GameCompleteWindow : UiWindow
    {
        public void OpenLeaderBoard()
        {
            UiStateMachine.SetState<UiStateLeaderboard>();
        }
    }
}
