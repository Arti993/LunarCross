using System;
using System.Collections;
using Infrastructure;
using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States.TutorialStates;
using UnityEngine;
using YG;


namespace LevelGeneration
{
    public class TutorialChunkPlacer : ChunkPlacer
    {
        private const float DelayBeforeShowWindow = 2.3f;

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
                DIServicesContainer.Instance.GetService<IUiStateMachine>()
                    .SetState<UiStateTutorialTouchscreenControl>();
            else
                DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateTutorialKeyboardControl>();
        }

        private void ShowTutorialCollectingWindow()
        {
            DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateTutorialAstronauts>();
        }

        private void ShowTutorialAliensWindow()
        {
            DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateTutorialAliens>();
        }

        private void ShowTutorialObstaclesWindow()
        {
            DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateTutorialObstacles>();
        }

        private void ShowTutorialTornadoWindow()
        {
            DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateTutorialTornado>();
        }

        private IEnumerator ShowWithDelay(Action showMethod)
        {
            yield return new WaitForSeconds(DelayBeforeShowWindow);
            showMethod.Invoke();
        }
    }
}
