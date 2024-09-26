public class GameCompleteWindow : UIWindow
{
    public void OpenLeaderBoard()
    {
        DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateLeaderboard>();
    }
}
