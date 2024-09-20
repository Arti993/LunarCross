using System;
using UnityEngine;
using Device = Agava.WebUtility.Device;

public class TutorialChunkPlacer : ChunkPlacer
{
    protected override void SpawnNextChunkInSequence()
    {
        switch (SpawnedChunks.Count)
        {
            case 1:
                SpawnEmptyChunk();
                ShowTutorialControlWindow();
                
                break;
            case 2:
                SpawnChunkWithCollectables();

                break;
            case 3:
                SpawnEmptyChunk();
                ShowTutorialCollectingWindow();

                break;
            case 4:
                SpawnChunkWithEnemies();

                break;
            case 5:
                SpawnEmptyChunk();
                ShowTutorialAliensWindow();

                break;
            case 6:
                SpawnLandscapeChunk();

                break;
            case 7:
                SpawnEmptyChunk();
                ShowTutorialObstaclesWindow();

                break;
            case 8:
                SpawnTornadoChunk();

                break;
            case 9:
                SpawnFinishChunk();
                ShowTutorialTornadoWindow();

                break;
            default:
                throw new InvalidOperationException();
        }
    }

    private void ShowTutorialControlWindow()
    {
        if (Device.IsMobile)
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
}
