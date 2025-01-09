using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States.TutorialStates;
using Reflex.Attributes;
using UnityEngine;
using Vehicle;

namespace UI
{
    public class FinishTutorialWindowViewer : MonoBehaviour
    {
        private bool _isShowed;
        private IUiStateMachine _uiStateMachine;

        [Inject]
        private void Construct(IUiStateMachine uiStateMachine)
        {
            _uiStateMachine = uiStateMachine;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isShowed == false && other.gameObject.TryGetComponent(out VehicleCatchBehaviour _vehicle))
                Show();
        }

        private void Show()
        {
            _uiStateMachine.SetState<UiStateTutorialFinish>();

            _isShowed = true;
        }
    }
}
