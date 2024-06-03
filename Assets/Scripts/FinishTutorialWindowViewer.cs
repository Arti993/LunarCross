using UnityEngine;

public class FinishTutorialWindowViewer : MonoBehaviour
{
    private bool _isShowed;
    
    private void OnTriggerEnter(Collider other)
    {
        if (_isShowed == false && other.gameObject.TryGetComponent(out VehicleCatchBehaviour _vehicle))
            Show();
    }

    private void Show()
    {
        UIRoot uiRoot = AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot().GetComponent<UIRoot>();

        TutorialUIViewer tutorialUIViewer = uiRoot.GetTutorialUIViewer();
        
        tutorialUIViewer.ShowTutorialFinishWindow();

        _isShowed = true;
    }
}
