using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States.TutorialStates;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vehicle;

namespace UI
{
    public class FinishTutorialWindowViewer : MonoBehaviour
    {
        private bool _isShowed;
        private IUiStateMachine _uiStateMachine;

        private void Construct()
        {
            _uiStateMachine = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IUiStateMachine>();
        }

        private void Awake()
        {
            Construct();
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
