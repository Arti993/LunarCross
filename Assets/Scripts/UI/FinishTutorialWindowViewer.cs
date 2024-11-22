using Infrastructure;
using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States.TutorialStates;
using UnityEngine;
using Vehicle;

namespace UI
{
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
            DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateTutorialFinish>();

            _isShowed = true;
        }
    }
}
