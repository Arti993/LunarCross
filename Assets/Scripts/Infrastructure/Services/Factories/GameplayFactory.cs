using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayFactory : IGameplayFactory
{
    private const int TutorialSceneIndex = 4;

    private GameObject _playerInstance;
    
    private readonly IAssetsProvider _provider;

    public GameplayFactory(IAssetsProvider provider)
    {
        _provider = provider;
    }

    public GameObject CreatePlayer(Vector3 position)
    {
        _playerInstance = _provider.Instantiate("Prefabs/RoverT30-1", position);

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
        return _provider.Instantiate("Prefabs/GameCamera");
    }

    public GameObject CreateSpawner()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        if(sceneIndex == TutorialSceneIndex)
            return _provider.Instantiate("Prefabs/TutorialSpawner");
        else
            return _provider.Instantiate("Prefabs/Spawner");
    }
}
