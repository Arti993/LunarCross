using System.Collections.Generic;
using LevelGeneration.Entities.EntityStateMachine;
using UnityEngine;

namespace LevelGeneration
{
    public class EntitySpawner : MonoBehaviour
    {
        private const float MinSpawnDistanceTreshold = 1f;
        private const int PrewarmedObjectsCount = 3;
        private const int MinSimpleEntitiesCount = 4;
        private const int MaxSimpleEntitiesCount = 7;

        [SerializeField] private EntityBehaviour _collectableEntity;
        [SerializeField] private EntityBehaviour _enemyEntity;
        [SerializeField] private List<Entity> _simpleEntities;
        [SerializeField] private List<Entity> _enemySimpleEntities;

        private List<Vector3> _occupiedPositions = new List<Vector3>();
        private bool _isOverlap;
        private bool _isPositionFound;

        private EntitiesObjectPool _collectablesPool;
        private EntitiesObjectPool _enemiesPool;
        private EntitiesObjectPool _simpleEntitiesPool;
        private EntitiesObjectPool _enemySimpleEntitiesPool;

        private void Awake()
        {
            _collectablesPool = new EntitiesObjectPool(_collectableEntity, PrewarmedObjectsCount);
            _enemiesPool = new EntitiesObjectPool(_enemyEntity, PrewarmedObjectsCount);

            IReadOnlyList<Entity> simpleEntities = _simpleEntities;
            IReadOnlyList<Entity> enemySimpleEntities = _enemySimpleEntities;

            _simpleEntitiesPool = new EntitiesObjectPool(simpleEntities, PrewarmedObjectsCount);
            _enemySimpleEntitiesPool = new EntitiesObjectPool(enemySimpleEntities, PrewarmedObjectsCount);
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

            int simpleEntitiesCount = Random.Range(MinSimpleEntitiesCount, MaxSimpleEntitiesCount);

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
                        if (Vector3.Distance(occupiedPosition, spawnPosition) < MinSpawnDistanceTreshold)
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
}