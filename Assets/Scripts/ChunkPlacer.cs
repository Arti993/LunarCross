using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using IJunior.TypedScenes;
using Zenject;

public class ChunkPlacer : MonoBehaviour, ISceneLoadHandler<LevelSettings>
{
    [SerializeField] private Chunk _emptyChunk;
    [SerializeField] private Chunk _firstChunk;
    [SerializeField] private Chunk _finishChunk;
    [SerializeField] private EntitySpawner _entitySpawner;
   
    private Chunk _landscapeChunk;
    private Chunk _tornadoChunk;
    private int _collectableEntitiesCount;
    private int _enemyEntitiesCount;
    private bool _isAllChunksSpawned = false;
    private Player _player;

    private List<Chunk> _spawnedChunks = new List<Chunk>();
    private List<Chunk> _currentVisibleChunks = new List<Chunk>();
    private Chunk _newChunk;

    private void Awake()
    {
        _spawnedChunks.Add(_firstChunk);
        _currentVisibleChunks.Add(_firstChunk);
    }

    private void Update()
    {
        if (_isAllChunksSpawned == false)
        {
            if (_player.transform.position.z > _spawnedChunks[_spawnedChunks.Count - 1].End.position.z - 40)
                SpawnNextChunkInSequence();
        }
    }

    public void OnSceneLoaded(LevelSettings levelSettings)
    {
        _landscapeChunk = levelSettings.ChunkWithObstacles;
        _tornadoChunk = levelSettings.TornadoChunk;
        _collectableEntitiesCount = levelSettings.CollectableEntitiesCount;
        _enemyEntitiesCount = levelSettings.EnemiesCount;
    }

    [Inject]
    public void Construct(Player player)
    {
        _player = player;
        _player.LevelFailed += OnLevelFailed;
    }

    private void OnDisable()
    {
        _player.LevelFailed -= OnLevelFailed;
    }

    private void OnLevelFailed()
    {
        foreach (var chunk in _spawnedChunks.Skip(1))
        {
            _spawnedChunks.Remove(chunk);
            Destroy(chunk);
        }

        _spawnedChunks.First().gameObject.SetActive(true);
        _currentVisibleChunks.Clear();
        _currentVisibleChunks.Add(_firstChunk);
    }

    private void SpawnNextChunkInSequence()
    {
        switch (_spawnedChunks.Count)
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
    }

    private void SpawnChunkWithCollectables()
    {
        _newChunk = SpawnChunk(_emptyChunk);

        _entitySpawner.SpawnEntitiesForСollectableChunk(_collectableEntitiesCount, _newChunk);
    }

    private void SpawnChunkWithEnemies()
    {
        _newChunk = SpawnChunk(_emptyChunk);

        _entitySpawner.SpawnEntitiesForEnemyChunk(_enemyEntitiesCount, _newChunk);
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
        _currentVisibleChunks.Add(newChunk);

        HideCompletedChunks();

        return newChunk;
    }

    private void HideCompletedChunks()
    {
        if(_currentVisibleChunks.Count > 3)
        {
            _currentVisibleChunks[0].gameObject.SetActive(false);
            _currentVisibleChunks.RemoveAt(0);
        }
    }
}
