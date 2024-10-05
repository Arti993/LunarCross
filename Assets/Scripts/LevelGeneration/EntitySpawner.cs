using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    [SerializeField] private EntityBehaviour _collectableEntity;
    [SerializeField] private EntityBehaviour _enemyEntity;
    [SerializeField] private List<Entity> _simpleEntities;
    [SerializeField] private List<Entity> _enemySimpleEntities;
    
    private EntityCollection _spawnedEntities = new EntityCollection();
    private List<Vector3> _occupiedPositions = new List<Vector3>();
    private float _minSpawnDistanceTreshold = 1f;
    private int _prewarmedObjectsCount = 3;
    private int _minSimpleEntitiesCount = 4;
    private int _maxSimpleEntitiesCount = 7;
    private bool _isOverlap;
    private bool _isPositionFound;
    
    private EntitiesObjectPool _collectablesPool;
    private EntitiesObjectPool _enemiesPool;
    private EntitiesObjectPool _simpleEntitiesPool;
    private EntitiesObjectPool _enemySimpleEntitiesPool;
    
    private void Awake()
    {
        _collectablesPool = new EntitiesObjectPool(_collectableEntity, _prewarmedObjectsCount);
        _enemiesPool = new EntitiesObjectPool(_enemyEntity, _prewarmedObjectsCount);

        IReadOnlyList<Entity> simpleEntities = _simpleEntities;
        IReadOnlyList<Entity> enemySimpleEntities = _enemySimpleEntities;
        
        _simpleEntitiesPool = new EntitiesObjectPool(simpleEntities, _prewarmedObjectsCount);
        _enemySimpleEntitiesPool = new EntitiesObjectPool(enemySimpleEntities, _prewarmedObjectsCount);
    }

    public void SpawnEntitiesForEnemyChunk(int entitiesCount, Chunk chunk)
    {
        SpawnEntities(entitiesCount, chunk, _enemiesPool, _enemySimpleEntitiesPool);
    }

    public void SpawnEntitiesForСollectableChunk(int entitiesCount, Chunk chunk)
    {
        SpawnEntities(entitiesCount, chunk, _collectablesPool, _simpleEntitiesPool);
    }

    private void SpawnEntities(int entitiesCount, Chunk chunk, EntitiesObjectPool entitiesPool,
        EntitiesObjectPool simpleEntitiesPool)
    {
        _occupiedPositions.Clear();

        Spawn(entitiesPool, entitiesCount, chunk);

        int simpleEntitiesCount = Random.Range(_minSimpleEntitiesCount, _maxSimpleEntitiesCount);

        Spawn(simpleEntitiesPool, simpleEntitiesCount, chunk);
      
    }

    private void Spawn(EntitiesObjectPool entitiesObjectPool, int entitiesCount, Chunk chunk)
    {
        Renderer surfaceRenderer = chunk.SurfaceRenderer;
        Vector3 chunkSurfaceSize = surfaceRenderer.bounds.size;
        Vector3 chunkPosition = surfaceRenderer.transform.position;

        for (int i = 0; i < entitiesCount; i++)
        {
            _isPositionFound = false;
            
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

                    Entity newEntity = entitiesObjectPool.Get();

                    newEntity.transform.position = spawnPosition;
                    newEntity.transform.rotation = Quaternion.identity;

                    _spawnedEntities.Add(newEntity);
                }
            }
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