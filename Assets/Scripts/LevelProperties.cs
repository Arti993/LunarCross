using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProperties
{
    public LevelProperties(Chunk chunkWithObstacles, Chunk tornadoCHunk, int collectableEntitiesCount, int enemiesCount)
    {
        ChunkWithObstacles = chunkWithObstacles;
        TornadoChunk = tornadoCHunk;
        CollectableEntitiesCount = collectableEntitiesCount;
        EnemiesCount = enemiesCount;
    }

    public Chunk ChunkWithObstacles { get; private set; }

    public Chunk TornadoChunk { get; private set; }

    public int CollectableEntitiesCount { get; private set; }

    public int EnemiesCount { get; private set; }

}
