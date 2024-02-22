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

    public void OnAuthorsButtonClick()
    {
        
    }
    
    public void OnSettingsButtonClick()
    {
        
    }
    
    public void OnQuitGameButtonClick()
    {
        Application.Quit();
    }
}
