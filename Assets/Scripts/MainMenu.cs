using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private const int GameplaySceneIndex = 2;
    private const int LevelChooseSceneIndex = 3;
    private const int TutorialSceneIndex = 4;
    
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

    public void OnTutorialButtonClick()
    {
        AllServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(TutorialSceneIndex);
    }
    
    public void OnSettingsButtonClick()
    {
        
    }
    
    public void OnQuitGameButtonClick()
    {
        Application.Quit();
    }
}
