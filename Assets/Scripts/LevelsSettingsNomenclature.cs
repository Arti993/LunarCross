using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class LevelsSettingsNomenclature : MonoBehaviour
{
    private IReadOnlyList<ChunkWithObstacles> _chunksWithObstacles;
    private IReadOnlyList<TornadoChunk> _tornadoChunks;
    private readonly int[] _collectableEntitiesCount = new int[] { 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10 };
    private readonly int[] _enemiesCount = new int[] { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13 };

    private List<LevelSettings> _levelsSettings = new List<LevelSettings>();

    [Inject]
    public void Construct(IReadOnlyList<ChunkWithObstacles> chunksWithObstacles,
        IReadOnlyList<TornadoChunk> tornadoChunks)
    {
        _chunksWithObstacles = chunksWithObstacles;
        _tornadoChunks = tornadoChunks;
        
        FillSettings();
    }

    public LevelSettings GetLevelSettings(int levelNumber)
    {
        if(_levelsSettings == null || _levelsSettings.Count < levelNumber)
            throw new InvalidOperationException();

        return _levelsSettings[levelNumber - 1]; 
    }

    private void FillSettings()
    {
        List<int[]> arrays = new List<int[]> {_collectableEntitiesCount, _enemiesCount};
        List<IReadOnlyList<Chunk>> lists = new List<IReadOnlyList<Chunk>> {_chunksWithObstacles, _tornadoChunks};

        bool isEqualLength = arrays.All(array => array.Length == arrays[0].Length) &&
                             lists.All(list => list.Count == lists[0].Count);

        if (isEqualLength == false)
            throw new InvalidOperationException();
        
        for(int i = 0; i < _collectableEntitiesCount.Length; i++)
        {
            _levelsSettings.Add(new LevelSettings(_chunksWithObstacles[i], _tornadoChunks[i], _collectableEntitiesCount[i], _enemiesCount[i]));
        }
    }
}
