using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public void OnClick()
    {
        AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetPauseMenuWindow();
    }
}
