using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private List<ChunkWithObstacles> _chunksWithObstacles;
        [SerializeField] private List<TornadoChunk> _tornadoChunks;
        
        public override void InstallBindings()
        {
            Container.Bind<IReadOnlyList<ChunkWithObstacles>>().FromInstance(_chunksWithObstacles).AsSingle();
            Container.Bind<IReadOnlyList<TornadoChunk>>().FromInstance(_tornadoChunks).AsSingle();
        }
    }
}
