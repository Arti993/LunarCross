using Data;
using Infrastructure.Services.Factories.UiFactory;
using UI;
using UnityEngine;

namespace Infrastructure.UIStateMachine.States.TutorialStates
{
    public class UiStateTutorialFinish : UiStateMachineState
    {
        private TutorialWindow _tutorialWindow;

        public override void Enter()
        {
            if (_tutorialWindow == null)
            {
                GameObject tutorialWindowObject = DIServicesContainer.Instance.GetService<IUiWindowFactory>()
                    .GetWindow(PrefabsPaths.TutorialFinishWindow, GetUiRoot());

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
