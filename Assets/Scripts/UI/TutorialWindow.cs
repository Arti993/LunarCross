using System;
using Infrastructure.Services.Factories.GameplayFactory;
using Infrastructure.UIStateMachine.States;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vehicle;

namespace UI
{
    public class TutorialWindow : UiWindow
    {
        private IGameplayFactory _gameplayFactory;
        
        protected override void Construct()
        {
            base.Construct();
            
            _gameplayFactory = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IGameplayFactory>();
        }
        
        protected override void Awake()
        {
            base.Awake();
            
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
            UiStateMachine.SetState<UiStatePauseButton>();
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