using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayFactory : IGameplayFactory
{
    private GameObject _playerInstance;
    private readonly IAssetsProvider _provider;

    public GameplayFactory(IAssetsProvider provider)
    {
        _provider = provider;
    }

    public GameObject CreatePlayer(Vector3 position)
    {
        _playerInstance = _provider.Instantiate(PrefabsPaths.Rover, position);

        return _playerInstance;
    }

    public GameObject GetPlayerInstance()
    {
        if(_playerInstance == null)
            throw new InvalidOperationException();

        return _playerInstance;
    }
    
    public GameObject CreateGameCamera()
    {
        return _provider.Instantiate(PrefabsPaths.GameCamera);
    }

    public GameObject CreateSpawner()
    {
        int currentsceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        if(currentsceneIndex == (int)SceneIndex.Tutorial)
            return _provider.Instantiate(PrefabsPaths.TutorialSpawner);
        else
            return _provider.Instantiate(PrefabsPaths.Spawner);
    }

    public UIControlInput GetUiControlInput()
    {
        GameObject uiRoot = DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();
        
        GameObject uiPlayerInputObject = DIServicesContainer.Instance.GetService<IUiWindowFactory>()
            .GetWindow(PrefabsPaths.UiControlInput,uiRoot);
        
        return uiPlayerInputObject.GetComponent<UIControlInput>();
    }

    public DesktopControlInput GetDesktopControlInput(Transform parent)
    {
        GameObject controlInputObject = _provider.Instantiate(PrefabsPaths.DesktopControlInput, parent);

        return controlInputObject.GetComponent<DesktopControlInput>();
    }
}
