using Data;
using Infrastructure;
using Infrastructure.Services.GameProgress;
using Infrastructure.Services.ScreenFader;
using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States;

namespace UI
{
    public class RestartGameQuestionWindow : UIWindow
    {
        public void OnYesButtonClick()
        {
            DIServicesContainer.Instance.GetService<IGameProgress>().ClearSaves();

            DIServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene((int) SceneIndex.Gameplay);
        }

        public void OnNoButtonClick()
        {
            DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateMainMenu>();
        }
    }
}
