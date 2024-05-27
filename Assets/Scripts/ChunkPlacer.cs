using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntitySpawner))]
public class ChunkPlacer : MonoBehaviour
{
    private const int MaxVisibleChunksCount = 4;
    private const int DistanceToSpawnNextChunk = 40;
    
    [SerializeField] private Chunk _firstChunk;

    private EntitySpawner _entitySpawner;
    private Chunk _emptyChunk;
    private Chunk _landscapeChunk;
    private Chunk _tornadoChunk;
    private Chunk _finishChunk;
    private int _collectableEntitiesCount;
    private int _enemyEntitiesCount;
    private bool _isAllChunksSpawned;
    private Transform _playerTransform;

    private List<Chunk> _spawnedChunks = new List<Chunk>();
    private List<Chunk> _currentVisibleChunks = new List<Chunk>();
    private Chunk _newChunk;

    private void Awake()
    {
        _entitySpawner = GetComponent<EntitySpawner>();
        
        _spawnedChunks.Add(_firstChunk);
        
        _currentVisibleChunks.Add(_firstChunk);
    }

    private void Start()
    {
        ApplyLevelSettings();
    }

    private void Update()
    {
        if (_isAllChunksSpawned == false)
        {
            if (_playerTransform.position.z > _spawnedChunks[_spawnedChunks.Count - 1].End.position.z - DistanceToSpawnNextChunk)
                SpawnNextChunkInSequence();
        }
    }
    
    public void SetPlayerTransform(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

    private void ApplyLevelSettings()
    {
        int levelNumber = AllServicesContainer.Instance.GetService<IGameProgress>().GetCurrentLevelNumber();

        Level currentLevel = AllServicesContainer.Instance.GetService<ILevelsSettingsNomenclature>()
            .GetLevelSettings(levelNumber);

        _landscapeChunk = currentLevel.ChunkWithObstacles;
        _tornadoChunk = currentLevel.TornadoChunk;
        _emptyChunk = currentLevel.EmptyChunk;
        _finishChunk = currentLevel.FinishChunk;
        _collectableEntitiesCount = currentLevel.CollectableEntitiesCount;
        _enemyEntitiesCount = currentLevel.EnemiesCount;
        
        _firstChunk.SetMaterials(currentLevel.SurfaceMaterial, currentLevel.StonesMaterial, currentLevel.MountainsMaterial);
        _landscapeChunk.SetMaterials(currentLevel.SurfaceMaterial, currentLevel.StonesMaterial, currentLevel.MountainsMaterial);
        _tornadoChunk.SetMaterials(currentLevel.SurfaceMaterial, currentLevel.StonesMaterial, currentLevel.MountainsMaterial);
        _emptyChunk.SetMaterials(currentLevel.SurfaceMaterial, currentLevel.StonesMaterial, currentLevel.MountainsMaterial);
        _finishChunk.SetMaterials(currentLevel.SurfaceMaterial, currentLevel.StonesMaterial, currentLevel.MountainsMaterial);
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

        newChunk.transform.position =
            _spawnedChunks[_spawnedChunks.Count - 1].End.position - newChunk.Begin.localPosition;

        _spawnedChunks.Add(newChunk);
        _currentVisibleChunks.Add(newChunk);

        HideCompletedChunks();

        return newChunk;
    }

    private void HideCompletedChunks()
    {
        if (_currentVisibleChunks.Count <= MaxVisibleChunksCount) 
            return;
        
        _currentVisibleChunks[0].gameObject.SetActive(false);
        _currentVisibleChunks.RemoveAt(0);
    }
}