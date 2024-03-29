using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public void OnClick()
    {
        GameObject uiRoot = AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();
        
        AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetPauseMenuWindow(uiRoot);
    }
}
