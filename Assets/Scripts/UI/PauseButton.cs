using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public void OnClick()
    {
        DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStatePauseMenu>();
    }
}
