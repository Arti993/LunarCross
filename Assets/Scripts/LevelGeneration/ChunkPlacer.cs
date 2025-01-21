using System;
using System.Collections.Generic;
using Infrastructure.Services.GameProgress;
using Infrastructure.Services.LevelSettings;
using Reflex.Extensions;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelGeneration
{
    [RequireComponent(typeof(EntitySpawner))]
    public class ChunkPlacer : MonoBehaviour
    {
        private const int MaxVisibleChunksCount = 4;
        private const int DistanceToSpawnNextChunk = 40;

        [SerializeField] private Chunk _firstChunk;

        protected readonly List<Chunk> SpawnedChunks = new List<Chunk>();
        private List<Chunk> _currentVisibleChunks = new List<Chunk>();
        private EntitySpawner _entitySpawner;
        private Chunk _emptyChunk;
        private Chunk _landscapeChunk;
        private Chunk _tornadoChunk;
        private Chunk _finishChunk;
        private Chunk _newChunk;
        private int _collectableEntitiesCount;
        private int _enemyEntitiesCount;
        private bool _isAllChunksSpawned;
        private Transform _playerTransform;
        private Level _currentLevel;
        private IGameProgress _gameProgress;
        private ILevelsSettingsNomenclature _levelsSettingsNomenclature;

        protected virtual void Construct()
        {
            _gameProgress = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IGameProgress>();
            _levelsSettingsNomenclature = SceneManager.GetActiveScene().GetSceneContainer().Resolve<ILevelsSettingsNomenclature>();
        }

        private void Awake()
        {
            Construct();

            _entitySpawner = GetComponent<EntitySpawner>();

            SpawnedChunks.Add(_firstChunk);

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
                if (_playerTransform.position.z >
                    SpawnedChunks[SpawnedChunks.Count - 1].End.position.z - DistanceToSpawnNextChunk)
                    SpawnNextChunkInSequence();
            }
        }

        public void SetPlayerTransform(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        protected virtual void SpawnNextChunkInSequence()
        {
            switch (SpawnedChunks.Count)
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

        protected void SpawnChunkWithCollectables()
        {
            _newChunk = SpawnChunk(_emptyChunk);

            _entitySpawner.SpawnEntitiesForСollectableChunk(_collectableEntitiesCount, _newChunk);
        }

        protected void SpawnChunkWithEnemies()
        {
            _newChunk = SpawnChunk(_emptyChunk);

            _entitySpawner.SpawnEntitiesForEnemyChunk(_enemyEntitiesCount, _newChunk);
        }

        protected void SpawnLandscapeChunk()
        {
            _ = SpawnChunk(_landscapeChunk);
        }

        protected void SpawnTornadoChunk()
        {
            _ = SpawnChunk(_tornadoChunk);
        }

        protected void SpawnFinishChunk()
        {
            _ = SpawnChunk(_finishChunk);

            _isAllChunksSpawned = true;
        }

        protected void SpawnEmptyChunk()
        {
            _newChunk = SpawnChunk(_emptyChunk);
        }

        private void ApplyLevelSettings()
        {
            if (this is TutorialChunkPlacer)
            {
                _currentLevel = _levelsSettingsNomenclature.GetTutorialLevelSettings();
            }
            else
            {
                int levelNumber = _gameProgress.GetCurrentLevelNumber();

                _currentLevel = _levelsSettingsNomenclature.GetLevelSettings(levelNumber);
            }

            _landscapeChunk = _currentLevel.ChunkWithObstacles;
            _tornadoChunk = _currentLevel.TornadoChunk;
            _emptyChunk = _currentLevel.EmptyChunk;
            _finishChunk = _currentLevel.FinishChunk;
            _collectableEntitiesCount = _currentLevel.CollectableEntitiesCount;
            _enemyEntitiesCount = _currentLevel.EnemiesCount;

            SetChunkMaterials(_firstChunk);
            SetChunkMaterials(_landscapeChunk);
            SetChunkMaterials(_tornadoChunk);
            SetChunkMaterials(_emptyChunk);
            SetChunkMaterials(_finishChunk);
        }

        private void SetChunkMaterials(Chunk chunk)
        {
            chunk.SetMaterials(_currentLevel.SurfaceMaterial, _currentLevel.StonesMaterial,
                _currentLevel.MountainsMaterial);
        }

        private Chunk SpawnChunk(Chunk chunkPrefab)
        {
            Chunk newChunk = Instantiate(chunkPrefab);

            newChunk.transform.position =
                SpawnedChunks[SpawnedChunks.Count - 1].End.position - newChunk.Begin.localPosition;

            SpawnedChunks.Add(newChunk);
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
}