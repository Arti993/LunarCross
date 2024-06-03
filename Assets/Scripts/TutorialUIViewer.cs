using UnityEngine;

public class TutorialUIViewer : MonoBehaviour
{
    public void ShowTutorialKeyboardControlWindow()
    {
        AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetTutorialKeyboardControlWindow(gameObject);
    }

    public void ShowTutorialTouchscreenControlWindow()
    {
        AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetTutorialTouchscreenControlWindow(gameObject);
    }

    public void ShowTutorialCollectingWindow()
    {
        AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetTutorialCollectingWindow(gameObject);
    }

    public void ShowTutorialAliensWindow()
    {
        AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetTutorialAliensWindow(gameObject);
    }

    public void ShowTutorialObstaclesWindow()
    {
        AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetTutorialObstaclesWindow(gameObject);
    }

    public void ShowTutorialTornadoWindow()
    {
        AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetTutorialTornadoWindow(gameObject);
    }

    public void ShowTutorialFinishWindow()
    {
        AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetTutorialFinishWindow(gameObject);
    }
}
