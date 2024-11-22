using Ami.BroAudio;
using LevelGeneration;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Level", menuName = "Levels/Create New Level", order = 51)]
    public class Level : ScriptableObject
    {
        [SerializeField] private ChunkWithObstacles _chunkWithObstacles;
        [SerializeField] private TornadoChunk _tornadoChunk;
        [SerializeField] private Chunk _finishChunk;
        [SerializeField] private EmptyChunk _emptyChunk;
        [SerializeField] private int _collectableEntitiesCount;
        [SerializeField] private int _enemiesCount;
        [SerializeField] private int _pointsForFirstStar;
        [SerializeField] private int _pointsForSecondStar;
        [SerializeField] private int _pointsForThirdStar;
        [SerializeField] private Material _surfaceMaterial;
        [SerializeField] private Material _stonesMaterial;
        [SerializeField] private Material _mountainsMaterial;
        [SerializeField] private SoundID _musicTheme;

        public Chunk ChunkWithObstacles => _chunkWithObstacles;
        public Chunk TornadoChunk => _tornadoChunk;
        public Chunk EmptyChunk => _emptyChunk;
        public Chunk FinishChunk => _finishChunk;
        public int CollectableEntitiesCount => _collectableEntitiesCount;
        public int EnemiesCount => _enemiesCount;
        public int PointsForFirstStar => _pointsForFirstStar;
        public int PointsForSecondStar => _pointsForSecondStar;
        public int PointsForThirdStar => _pointsForThirdStar;
        public Material SurfaceMaterial => _surfaceMaterial;
        public Material StonesMaterial => _stonesMaterial;
        public Material MountainsMaterial => _mountainsMaterial;
        public SoundID MusicTheme => _musicTheme;
    }
}
    
