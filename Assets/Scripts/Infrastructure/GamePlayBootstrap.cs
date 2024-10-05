using System;
using UnityEngine;

public class GamePlayBootstrap : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;

    private void Awake()
    {
        GameObject uiRootObject = DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();
        
        GameObject cameraObject = DIServicesContainer.Instance.GetService<IGameplayFactory>().CreateGameCamera();

        GameObject playerObject = DIServicesContainer.Instance.GetService<IGameplayFactory>().CreatePlayer(_startPoint.position);

        GameObject spawnerObject = DIServicesContainer.Instance.GetService<IGameplayFactory>().CreateSpawner();

        SetCameraForCanvas(uiRootObject, cameraObject);

        SetPlayerTransformForGameCamera(cameraObject, playerObject);

        SetPlayerTransformForSpawner(spawnerObject, playerObject);

        PrepareUI(uiRootObject);
        
        DIServicesContainer.Instance.GetService<IAudioPlayback>().PlayLevelTheme();
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
        if(cameraObject.TryGetComponent(out PlayerFollowCamera playerFollowCamera))
            playerFollowCamera.SetPlayerTransform(playerObject.transform);
        else
            throw new InvalidOperationException();
    }

    private void SetPlayerTransformForSpawner(GameObject spawnerObject, GameObject playerObject)
    {
        if(spawnerObject.TryGetComponent(out ChunkPlacer chunkPlacer))
            chunkPlacer.SetPlayerTransform(playerObject.transform);
        else
            throw new InvalidOperationException();
    }
    
    private void PrepareUI(GameObject uiRoot)
    {
        DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetWindow(PrefabsPaths.LevelNumberTitle, uiRoot);
        
        DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStatePauseButton>();
    }
}