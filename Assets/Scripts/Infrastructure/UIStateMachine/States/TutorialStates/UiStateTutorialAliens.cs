using Data;
using Infrastructure.Services.Factories.UiFactory;
using UI;
using UnityEngine;

namespace Infrastructure.UIStateMachine.States.TutorialStates
{
    public class UiStateTutorialAliens : UiStateMachineState
    {
        private TutorialWindow _tutorialWindow;

        public override void Enter()
        {
            if (_tutorialWindow == null)
            {
                GameObject tutorialWindowObject = DIServicesContainer.Instance.GetService<IUiWindowFactory>()
                    .GetWindow(PrefabsPaths.TutorialAliensWindow, GetUiRoot());

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
