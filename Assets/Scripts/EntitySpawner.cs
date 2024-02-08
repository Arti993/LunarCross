using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] private EntityBehaviour _collectableEntity;
    [SerializeField] private EntityBehaviour _enemyEntity;
    [SerializeField] private List<EntityBehaviour> _expeditionSimpleEntities;
    [SerializeField] private List<EntityBehaviour> _enemySimpleEntities;

    public void SpawnEntitiesForEnemyChunk(int entitiesCount, Chunk chunk)
    {
        Spawn(_enemyEntity, entitiesCount, chunk);

        for(int i = 0; i < _enemySimpleEntities.Count; i++)
        {
            int simpleEntitiesCount = Random.Range(5, 7);

            Spawn(_enemySimpleEntities[i], simpleEntitiesCount, chunk);
        }
    }
  
    public void SpawnEntitiesForСollectableChunk(int entitiesCount, Chunk chunk)
    {
        Spawn(_collectableEntity, entitiesCount, chunk);

        for (int i = 0; i < _expeditionSimpleEntities.Count; i++)
        {
            int simpleEntitiesCount = Random.Range(5, 7);

            Spawn(_expeditionSimpleEntities[i], simpleEntitiesCount, chunk);
        }
    }

    private void Spawn(EntityBehaviour entity, int entitiesCount, Chunk chunk)
    {
        Renderer surfaceRenderer = chunk.SurfaceRenderer;
        Vector3 surfaceSize = surfaceRenderer.bounds.size;

        for(int i = 0; i < entitiesCount; i++)
        {
            float spawnPositionX = Random.Range(-surfaceSize.x / 2, surfaceSize.x / 2) + surfaceRenderer.transform.position.x;
            float spawnPositionZ = Random.Range(-surfaceSize.z / 2, surfaceSize.z / 2) + surfaceRenderer.transform.position.z;
            Vector3 spawnPosition = new Vector3(spawnPositionX, surfaceRenderer.transform.position.y, spawnPositionZ);

            Instantiate(entity, spawnPosition, Quaternion.identity);
        }
    }
}
