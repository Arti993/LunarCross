using UnityEngine;

public class TutorialUIViewer : MonoBehaviour
{
    public void ShowTutorialControlWindow()
    {
        if(Application.isMobilePlatform)
            DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetTutorialTouchscreenControlWindow(gameObject);
        else
            DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetTutorialKeyboardControlWindow(gameObject);
    }

    public void ShowTutorialCollectingWindow()
    {
        DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetTutorialCollectingWindow(gameObject);
    }

    public void ShowTutorialAliensWindow()
    {
        DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetTutorialAliensWindow(gameObject);
    }

    public void ShowTutorialObstaclesWindow()
    {
        DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetTutorialObstaclesWindow(gameObject);
    }

    public void ShowTutorialTornadoWindow()
    {
        DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetTutorialTornadoWindow(gameObject);
    }

    public void ShowTutorialFinishWindow()
    {
        DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetTutorialFinishWindow(gameObject);
    }
}
