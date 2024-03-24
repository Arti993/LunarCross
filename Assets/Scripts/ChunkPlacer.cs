using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntitySpawner))]
[RequireComponent(typeof(LevelsSettingsNomenclature))]
public class ChunkPlacer : MonoBehaviour
{
    //переделать в сервис
    [SerializeField] private Chunk _firstChunk;
    [SerializeField] private GameplaySceneBootstrap _bootstrap;

    private EntitySpawner _entitySpawner;
    private LevelsSettingsNomenclature _levelsSettingsNomenclature;
    private Chunk _emptyChunk;
    private Chunk _landscapeChunk;
    private Chunk _tornadoChunk;
    private Chunk _finishChunk;
    private int _collectableEntitiesCount;
    private int _enemyEntitiesCount;
    private bool _isAllChunksSpawned = false;

    private List<Chunk> _spawnedChunks = new List<Chunk>();
    private List<Chunk> _currentVisibleChunks = new List<Chunk>();
    private Chunk _newChunk;

    private void Awake()
    {
        _entitySpawner = GetComponent<EntitySpawner>();
        _levelsSettingsNomenclature = GetComponent<LevelsSettingsNomenclature>();
       
        ApplyLevelSettings();
        
        _spawnedChunks.Add(_firstChunk);
        
        _currentVisibleChunks.Add(_firstChunk);
    }

    private void Update()
    {
        if (_isAllChunksSpawned == false)
        {
            if (_bootstrap.Player.transform.position.z > _spawnedChunks[_spawnedChunks.Count - 1].End.position.z - 40)
                SpawnNextChunkInSequence();
        }
    }

    private void ApplyLevelSettings()
    {
        int levelNumber = PlayerPrefs.GetInt("SelectedLevelNumber", 0);
        //сделать чтобы после конца уровня selected стал равен нулю, а reached увеличился на 1
        

        if (levelNumber == 0)
            levelNumber = PlayerPrefs.GetInt("ReachedLevelNumber", 1);

        Level currentLevel = _levelsSettingsNomenclature.GetLevelSettings(levelNumber);

        _landscapeChunk = currentLevel.ChunkWithObstacles;
        _tornadoChunk = currentLevel.TornadoChunk;
        _emptyChunk = currentLevel.EmptyChunk;
        _finishChunk = currentLevel.FinishChunk;
        _collectableEntitiesCount = currentLevel.CollectableEntitiesCount;
        _enemyEntitiesCount = currentLevel.EnemiesCount;
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
        if (_currentVisibleChunks.Count > 3)
        {
            _currentVisibleChunks[0].gameObject.SetActive(false);
            _currentVisibleChunks.RemoveAt(0);
        }
    }
}