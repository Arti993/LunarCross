using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] private EntityBehaviour _collectableEntity;
    [SerializeField] private EntityBehaviour _enemyEntity;
    [SerializeField] private List<Entity> _expeditionSimpleEntities;
    [SerializeField] private List<Entity> _enemySimpleEntities;

    private EntityCollection _spawnedEntities = new EntityCollection();
    private List<Vector3> _occupiedPositions = new List<Vector3>();
    private float _minSpawnDistanceTreshold = 1f;
    private bool _isOverlap;
    private bool _isPositionFound;

    public void SpawnEntitiesForEnemyChunk(int entitiesCount, Chunk chunk)
    {
        SpawnEntities(entitiesCount, chunk, _enemyEntity, _enemySimpleEntities);
    }

    public void SpawnEntitiesForСollectableChunk(int entitiesCount, Chunk chunk)
    {
        SpawnEntities(entitiesCount, chunk, _collectableEntity, _expeditionSimpleEntities);
    }

    private void SpawnEntities(int entitiesCount, Chunk chunk, EntityBehaviour entity,
        IReadOnlyList<Entity> spaceObjects)
    {
        _occupiedPositions.Clear();

        Spawn(entity, entitiesCount, chunk);

        foreach (var spaceObject in spaceObjects)
        {
            int simpleEntitiesCount = Random.Range(5, 7);

            Spawn(spaceObject, simpleEntitiesCount, chunk);
        }
    }

    private void Spawn(Entity entity, int entitiesCount, Chunk chunk)
    {
        Renderer surfaceRenderer = chunk.SurfaceRenderer;
        Vector3 chunkSurfaceSize = surfaceRenderer.bounds.size;
        Vector3 chunkPosition = surfaceRenderer.transform.position;

        for (int i = 0; i < entitiesCount; i++)
        {
            while (_isPositionFound == false)
            {
                Vector3 spawnPosition = GetRandomPositionForSpawn(chunkSurfaceSize, chunkPosition);

                _isOverlap = false;

                foreach (Vector3 occupiedPosition in _occupiedPositions)
                {
                    if (Vector3.Distance(occupiedPosition, spawnPosition) < _minSpawnDistanceTreshold)
                    {
                        _isOverlap = true;
                        break;
                    }
                }

                if (_isOverlap == false)
                {
                    _isPositionFound = true;
                    
                    _occupiedPositions.Add(spawnPosition);
                    
                    Entity newEntity = Instantiate(entity, spawnPosition, Quaternion.identity);

                    _spawnedEntities.Add(newEntity);
                }
            }

            _isPositionFound = false;
        }
    }

    private Vector3 GetRandomPositionForSpawn(Vector3 chunkSurfaceSize, Vector3 chunkPosition)
    {
        float spawnPositionX = Random.Range(-chunkSurfaceSize.x / 2, chunkSurfaceSize.x / 2) + chunkPosition.x;
        float spawnPositionZ = Random.Range(-chunkSurfaceSize.z / 2, chunkSurfaceSize.z / 2) + chunkPosition.z;
        Vector3 spawnPosition = new Vector3(spawnPositionX, chunkPosition.y, spawnPositionZ);

        return spawnPosition;
    }
}