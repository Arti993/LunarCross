using System;
using System.Collections;
using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States.TutorialStates;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace LevelGeneration
{
    public class TutorialChunkPlacer : ChunkPlacer
    {
        private const float DelayBeforeShowWindow = 2.3f;
        private IUiStateMachine _uiStateMachine;

        protected override void Construct()
        {
            base.Construct();
            
            _uiStateMachine = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IUiStateMachine>();
        }

        protected override void SpawnNextChunkInSequence()
        {
            switch (SpawnedChunks.Count)
            {
                case 1:
                    SpawnEmptyChunk();
                    _ = StartCoroutine(ShowWithDelay(ShowTutorialControlWindow));

                    break;
                case 2:
                    SpawnChunkWithCollectables();

                    break;
                case 3:
                    SpawnEmptyChunk();
                    _ = StartCoroutine(ShowWithDelay(ShowTutorialCollectingWindow));

                    break;
                case 4:
                    SpawnChunkWithEnemies();

                    break;
                case 5:
                    SpawnEmptyChunk();
                    _ = StartCoroutine(ShowWithDelay(ShowTutorialAliensWindow));

                    break;
                case 6:
                    SpawnLandscapeChunk();

                    break;
                case 7:
                    SpawnEmptyChunk();
                    _ = StartCoroutine(ShowWithDelay(ShowTutorialObstaclesWindow));

                    break;
                case 8:
                    SpawnTornadoChunk();

                    break;
                case 9:
                    SpawnFinishChunk();
                    _ = StartCoroutine(ShowWithDelay(ShowTutorialTornadoWindow));

                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        private void ShowTutorialControlWindow()
        {
            if (YandexGame.EnvironmentData.isMobile)
                _uiStateMachine.SetState<UiStateTutorialTouchscreenControl>();
            else
                _uiStateMachine.SetState<UiStateTutorialKeyboardControl>();
        }

        private void ShowTutorialCollectingWindow()
        {
            _uiStateMachine.SetState<UiStateTutorialAstronauts>();
        }

        private void ShowTutorialAliensWindow()
        {
            _uiStateMachine.SetState<UiStateTutorialAliens>();
        }

        private void ShowTutorialObstaclesWindow()
        {
            _uiStateMachine.SetState<UiStateTutorialObstacles>();
        }

        private void ShowTutorialTornadoWindow()
        {
            _uiStateMachine.SetState<UiStateTutorialTornado>();
        }

        private IEnumerator ShowWithDelay(Action showMethod)
        {
            yield return new WaitForSeconds(DelayBeforeShowWindow);
            showMethod.Invoke();
        }
    }
}
