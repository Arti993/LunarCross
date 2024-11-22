using Infrastructure;
using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States;

namespace UI
{
    public class GameCompleteWindow : UIWindow
    {
        public void OpenLeaderBoard()
        {
            DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateLeaderboard>();
        }
    }
}
