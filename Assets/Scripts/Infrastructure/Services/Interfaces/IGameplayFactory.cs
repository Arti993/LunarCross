using UnityEngine;

public interface IGameplayFactory : IService
{
    public GameObject CreatePlayer(Vector3 position);
    
    public GameObject GetPlayerInstance();

    public GameObject CreateGameCamera();

    public GameObject CreateSpawner();
}
