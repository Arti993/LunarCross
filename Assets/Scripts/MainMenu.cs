using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private const int GameplaySceneIndex = 1;
    private const int LevelChooseSceneIndex = 2;
    
    public void OnPlayButtonClick()
    {
        AllServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(GameplaySceneIndex);
    }

    public void OnLevelsChooseButtonCLick()
    {
        AllServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(LevelChooseSceneIndex);
    }

    public void OnRestartGameButtonClick()
    {
        AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetRestartGameQuestionWindow(this.gameObject);
    }
    
    public void OnSettingsButtonClick()
    {
        
    }
    
    public void OnQuitGameButtonClick()
    {
        Application.Quit();
    }
}
