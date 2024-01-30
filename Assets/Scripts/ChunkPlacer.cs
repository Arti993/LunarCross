using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;
using Zenject;

public class ChunkPlacer : MonoBehaviour, ISceneLoadHandler<LevelProperties>
{
    [SerializeField] private Chunk _emptyChunk;
    [SerializeField] private Chunk _firstChunk;
    [SerializeField] private Chunk _finishChunk;
    [SerializeField] private EntitySpawner _entitySpawner;
    [SerializeField] private NavMeshRebaker _navMeshRebaker;

    private Chunk _landscapeChunk;
    private Chunk _tornadoChunk;
    private int _collectableEntitiesCount;
    private int _enemyEntitiesCount;
    private int _totalSpawnedChunksCount;
    private bool _isAllChunksSpawned = false;
    private Transform _player;


    private List<Chunk> _spawnedChunks = new List<Chunk>();
    private Chunk _newChunk;

    private void Awake()
    {
        _spawnedChunks.Add(_firstChunk);
        _totalSpawnedChunksCount = 1;
    }

    private void Update()
    {
        if (_isAllChunksSpawned == false)
        {
            if (_player.position.z > _spawnedChunks[_spawnedChunks.Count - 1].End.position.z - 40)
                SpawnNextChunkInSequence();
        }
    }

    public void OnSceneLoaded(LevelProperties levelProperties)
    {
        _landscapeChunk = levelProperties.ChunkWithObstacles;
        _tornadoChunk = levelProperties.TornadoChunk;
        _collectableEntitiesCount = levelProperties.CollectableEntitiesCount;
        _enemyEntitiesCount = levelProperties.EnemiesCount;
    }

    [Inject]
    public void Construct(Player player)
    {
        _player = player.transform;
    }

    private void SpawnNextChunkInSequence()
    {
        switch (_totalSpawnedChunksCount)
        {
            case 1:
                SpawnChunkWithCollectables();

                break;
            case 2:
                SpawnChunkWithEnemies();
                
                break;
            case 3:
                SpawnLandscapeChunk();

                break;
            case 4:
                SpawnTornadoChunk();

                break;
            case 5:
                SpawnChunkWithCollectables();

                break;
            case 6:
                SpawnChunkWithEnemies();

                break;
            case 7:
                SpawnLandscapeChunk();

                break;
            case 8:
                SpawnTornadoChunk();

                break;
            case 9:
                SpawnFinishChunk();

                break;
            default:
                throw new InvalidOperationException();
        }

        _totalSpawnedChunksCount++;
    }

    private void SpawnChunkWithCollectables()
    {
        _newChunk = SpawnChunk(_emptyChunk);

        _entitySpawner.SpawnEntitiesForСollectableChunk(_collectableEntitiesCount, _newChunk);

        _navMeshRebaker.Rebake();
    }

    private void SpawnChunkWithEnemies()
    {
        _newChunk = SpawnChunk(_emptyChunk);

        _entitySpawner.SpawnEntitiesForEnemyChunk(_enemyEntitiesCount, _newChunk);

        _navMeshRebaker.Rebake();
    }

    private void SpawnLandscapeChunk()
    {
        SpawnChunk(_landscapeChunk);
    }

    private void SpawnTornadoChunk()
    {
        SpawnChunk(_tornadoChunk);
    }

    private void SpawnFinishChunk()
    {
        SpawnChunk(_finishChunk);

        _isAllChunksSpawned = true;
    }

    private Chunk SpawnChunk(Chunk chunkPrefab)
    {
        Chunk newChunk = Instantiate(chunkPrefab);

        newChunk.transform.position = _spawnedChunks[_spawnedChunks.Count - 1].End.position - newChunk.Begin.localPosition;

        _spawnedChunks.Add(newChunk);

        if(_spawnedChunks.Count > 3)
        {
            Destroy(_spawnedChunks[0].gameObject);
            _spawnedChunks.RemoveAt(0);
        }

        _navMeshRebaker.Rebake();

        return newChunk;
    }
}
