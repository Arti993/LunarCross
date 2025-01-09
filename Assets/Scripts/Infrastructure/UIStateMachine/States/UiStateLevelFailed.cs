using Data;
using Infrastructure.Services.Factories.UiFactory;
using UI;
using UnityEngine;

namespace Infrastructure.UIStateMachine.States
{
    public class UiStateLevelFailed : UiStateMachineState
    {
        public UiStateLevelFailed()
        {
            PrefabPath = PrefabsPaths.LevelFailedWindow;
        }

        public override void Exit()
        {
        }
    }
}
