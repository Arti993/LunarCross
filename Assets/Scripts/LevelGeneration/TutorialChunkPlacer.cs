using System;
using System.Collections;
using UnityEngine;
using YG;

public class TutorialChunkPlacer : ChunkPlacer
{
    private const float DelayBeforeShowWindow = 2.3f;
    
    protected override void SpawnNextChunkInSequence()
    {
        switch (SpawnedChunks.Count)
        {
            case 1:
                SpawnEmptyChunk();
                StartCoroutine(ShowWithDelay(ShowTutorialControlWindow));

                break;
            case 2:
                SpawnChunkWithCollectables();

                break;
            case 3:
                SpawnEmptyChunk();
                StartCoroutine(ShowWithDelay(ShowTutorialCollectingWindow));

                break;
            case 4:
                SpawnChunkWithEnemies();

                break;
            case 5:
                SpawnEmptyChunk();
                StartCoroutine(ShowWithDelay(ShowTutorialAliensWindow));

                break;
            case 6:
                SpawnLandscapeChunk();

                break;
            case 7:
                SpawnEmptyChunk();
                StartCoroutine(ShowWithDelay(ShowTutorialObstaclesWindow));

                break;
            case 8:
                SpawnTornadoChunk();

                break;
            case 9:
                SpawnFinishChunk();
                StartCoroutine(ShowWithDelay(ShowTutorialTornadoWindow));

                break;
            default:
                throw new InvalidOperationException();
        }
    }

    private void ShowTutorialControlWindow()
    {
        if (YandexGame.EnvironmentData.isMobile)
            DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateTutorialTouchscreenControl>();
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
