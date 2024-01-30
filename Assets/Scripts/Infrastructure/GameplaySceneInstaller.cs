using Zenject;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Player _playerPrefab;

        public override void InstallBindings()
        {
            BindPlayer();
        }

        private void BindPlayer()
        {
            Player player = Container.InstantiatePrefabForComponent<Player>(_playerPrefab, _startPoint.position, Quaternion.identity, null);

            Container.Bind<Player>().FromInstance(player).AsSingle();
            
            //changed
        }
    }
}
