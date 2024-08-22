using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public void OnClick()
    {
        GameObject uiRoot = DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();
        
        DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetPauseMenuWindow(uiRoot);
    }
}
