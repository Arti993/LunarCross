using UnityEngine.SceneManagement;

public class GameSettingsWindow : UIWindow
{
    public void Exit()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        if(currentSceneIndex == (int)SceneIndex.MainMenu)
            DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateMainMenu>();

        if (currentSceneIndex == (int) SceneIndex.Gameplay || currentSceneIndex == (int) SceneIndex.Tutorial)
            DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStatePauseMenu>();
    }
}
