using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    private const int MainMenuSceneIndex = 0;
    
    public void GoToMainMenu()
    {
        AllServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(MainMenuSceneIndex);
    }
}
