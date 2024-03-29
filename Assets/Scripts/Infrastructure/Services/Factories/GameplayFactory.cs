using UnityEngine;

public class GameplayFactory : IGameplayFactory
{
    private readonly IAssets _provider;

    public GameplayFactory(IAssets provider)
    {
        _provider = provider;
    }

    public GameObject CreatePlayer(Vector3 position)
    {
        return _provider.Instantiate("Prefabs/RoverT30-1", position);
    }
    
    public GameObject CreateGameCamera()
    {
        return _provider.Instantiate("Prefabs/GameCamera");
    }

    public GameObject CreateSpawner()
    {
        return _provider.Instantiate("Prefabs/Spawner");
    }
}
