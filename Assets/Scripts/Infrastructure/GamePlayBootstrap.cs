using System;
using UnityEngine;

public class GamePlayBootstrap : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;

    private void Awake()
    {
        GameObject uiRootObject = AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();

        GameObject playerObject = AllServicesContainer.Instance.GetService<IGameplayFactory>().CreatePlayer(_startPoint.position);

        GameObject cameraObject = AllServicesContainer.Instance.GetService<IGameplayFactory>().CreateGameCamera();
        
        GameObject spawnerObject = AllServicesContainer.Instance.GetService<IGameplayFactory>().CreateSpawner();

        SetCameraForCanvas(uiRootObject, cameraObject);

        SetPlayerTransformForGameCamera(cameraObject, playerObject);

        SetPlayerTransformForSpawner(spawnerObject, playerObject);

        AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetPauseButton(uiRootObject);
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

}