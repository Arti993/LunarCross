using UnityEngine;
using IJunior.TypedScenes;

public class MainMenu : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        Gameplay.Load();
    }

    public void OnLevelsChooseButtonCLick()
    {
        LevelsChoose.Load();
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
