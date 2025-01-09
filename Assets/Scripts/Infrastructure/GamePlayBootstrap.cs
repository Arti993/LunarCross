using System;
using Data;
using Infrastructure.Services.AudioPlayback;
using Infrastructure.Services.Factories.GameplayFactory;
using Infrastructure.Services.Factories.UiFactory;
using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States;
using LevelGeneration;
using Reflex.Attributes;
using UI;
using UnityEngine;

namespace Infrastructure
{
    public class GamePlayBootstrap : MonoBehaviour
    {
        [SerializeField] private Transform _startPoint;
        
        private IUiStateMachine _uiStateMachine;
        private IUiWindowFactory _uiWindowFactory;
        private IGameplayFactory _gameplayFactory;
        private IAudioPlayback _audioPlayback;

        [Inject]
        private void Construct(IUiStateMachine uiStateMachine, IUiWindowFactory uiWindowFactory,
            IGameplayFactory gameplayFactory, IAudioPlayback audioPlayback)
        {
            _uiStateMachine = uiStateMachine;
            _uiWindowFactory = uiWindowFactory;
            _gameplayFactory = gameplayFactory;
            _audioPlayback = audioPlayback;
        }

        private void Awake()
        {
            GameObject uiRootObject = _uiWindowFactory.GetUIRoot();

            GameObject cameraObject = _gameplayFactory.CreateGameCamera();

            GameObject playerObject = _gameplayFactory.CreatePlayer(_startPoint.position);

            GameObject spawnerObject = _gameplayFactory.CreateSpawner();

            SetCameraForCanvas(uiRootObject, cameraObject);

            SetPlayerTransformForGameCamera(cameraObject, playerObject);

            SetPlayerTransformForSpawner(spawnerObject, playerObject);

            PrepareUI(uiRootObject);

            _audioPlayback.PlayLevelTheme();
        }

        private void SetCameraForCanvas(GameObject uiRootObject, GameObject cameraObject)
        {
            if (uiRootObject.TryGetComponent(out UIRoot uiRoot) && cameraObject.TryGetComponent(out Camera camera))
                uiRoot.SetCamera(camera);
            else
                throw new InvalidOperationException();
        }

        private void SetPlayerTransformForGameCamera(GameObject cameraObject, GameObject playerObject)
        {
            if (cameraObject.TryGetComponent(out PlayerFollowCamera playerFollowCamera))
                playerFollowCamera.SetPlayerTransform(playerObject.transform);
            else
                throw new InvalidOperationException();
        }

        private void SetPlayerTransformForSpawner(GameObject spawnerObject, GameObject playerObject)
        {
            if (spawnerObject.TryGetComponent(out ChunkPlacer chunkPlacer))
                chunkPlacer.SetPlayerTransform(playerObject.transform);
            else
                throw new InvalidOperationException();
        }

        private void PrepareUI(GameObject uiRoot)
        {
            _uiWindowFactory.ShowUIObject(PrefabsPaths.LevelNumberTitle, uiRoot);

            _uiStateMachine.SetState<UiStatePauseButton>();
        }
    }
}