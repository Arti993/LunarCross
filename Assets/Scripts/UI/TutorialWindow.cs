using System;
using Infrastructure.Services.Factories.GameplayFactory;
using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States;
using Reflex.Attributes;
using UnityEngine;
using Vehicle;

namespace UI
{
    public class TutorialWindow : UiWindow
    {
        private IUiStateMachine _uiStateMachine;
        private IGameplayFactory _gameplayFactory;
        
        [Inject]
        private void Construct(IUiStateMachine uiStateMachine, IGameplayFactory gameplayFactory)
        {
            _uiStateMachine = uiStateMachine;
            _gameplayFactory = gameplayFactory;
        }
        
        protected virtual void Awake()
        {
            Vector3 startPosition = PanelRect.localPosition;

            startPosition.y = PanelBottomPosY;

            PanelRect.localPosition = startPosition;
        }

        public void Open()
        {
            Time.timeScale = 0f;

            PanelIntro();
        }

        public void Close()
        {
            PanelOutro();

            Time.timeScale = 1f;
        }

        public void Exit()
        {
            _uiStateMachine.SetState<UiStatePauseButton>();
        }

        protected CatchZoneViewer GetCatchZoneViewer()
        {
            GameObject vehicle = _gameplayFactory.GetPlayerInstance();

            if (vehicle.TryGetComponent(out CatchZoneViewer catchZoneViewer) == false)
                throw new InvalidOperationException();

            return catchZoneViewer;
        }
    }
}