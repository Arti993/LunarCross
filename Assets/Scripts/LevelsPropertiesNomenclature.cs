using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelsPropertiesNomenclature : MonoBehaviour 
{
    [SerializeField] private Chunk[] _chunksWithObstacles;
    [SerializeField] private Chunk[] _tornadoChunks;
    private int[] _collectableEntitiesCount = new int[] { 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10 };
    private int[] _enemiesCount = new int[] { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };

    private List<LevelProperties> _levels = new List<LevelProperties>();

    public IReadOnlyList<LevelProperties> Levels => _levels;

    private void Start()
    {
        Array[] settingsArrays = new Array[]
        {
            _chunksWithObstacles,
            _tornadoChunks,
            _collectableEntitiesCount,
            _enemiesCount
        };

        bool isEqualLength = settingsArrays.All(array => array.Length == settingsArrays[0].Length);

        if (isEqualLength == false)
            throw new InvalidOperationException();

        GenerateLevelsProperties();
    }

    public LevelProperties GetLevelProperties(int levelNumber)
    {
        if(Levels == null || Levels.Count < levelNumber)
            throw new InvalidOperationException();

        return Levels[levelNumber - 1]; 
    }

    private void GenerateLevelsProperties()
    {
        for(int i = 0; i < _collectableEntitiesCount.Length; i++)
        {
            _levels.Add(new LevelProperties(_chunksWithObstacles[i], _tornadoChunks[i], _collectableEntitiesCount[i], _enemiesCount[i]));
        }
    }
}
