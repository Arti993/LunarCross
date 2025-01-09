using UI;
using UnityEngine;

namespace Infrastructure.UIStateMachine.States.TutorialStates
{
    public abstract class UiStateMachineTutorialState : UiStateMachineState
    {
        private TutorialWindow _tutorialWindow;
        
        public override void Enter()
        {
            if (_tutorialWindow == null)
            {
                GameObject tutorialWindowObject = GetUiObject();

                _tutorialWindow = tutorialWindowObject.GetComponent<TutorialWindow>();
            }

            _tutorialWindow.Open();
        }

        public override void Exit()
        {
            _tutorialWindow.Close();
        }
    }
}
