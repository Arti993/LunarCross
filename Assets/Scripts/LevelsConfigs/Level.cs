using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Levels/Create New Level", order = 51)]
public class Level : ScriptableObject
{
    [SerializeField] private ChunkWithObstacles _chunkWithObstacles;

    [SerializeField] private TornadoChunk _tornadoChunk;
    
    [SerializeField] private Chunk _finishChunk;
    
    [SerializeField] private Chunk _emptyChunk;

    [SerializeField] private int _collectableEntitiesCount;

    [SerializeField] private int _enemiesCount;

    public Chunk ChunkWithObstacles => _chunkWithObstacles;

    public Chunk TornadoChunk => _tornadoChunk;

    public Chunk EmptyChunk => _emptyChunk;

    public Chunk FinishChunk => _finishChunk;

    public int CollectableEntitiesCount => _collectableEntitiesCount;
    
    public int EnemiesCount => _enemiesCount;
}
    
