using System;
using Data;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.Factories.UiFactory;
using PlayersInput;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.Factories.GameplayFactory
{
    public class GameplayFactory : IGameplayFactory
    {
        private readonly IAssetsProvider _provider;
        private GameObject _playerInstance;
        private IUiWindowFactory _uiWindowFactory;

        public GameplayFactory(IAssetsProvider provider, IUiWindowFactory uiWindowFactory)
        {
            _provider = provider;
            _uiWindowFactory = uiWindowFactory;
        }

        public GameObject CreatePlayer(Vector3 position)
        {
            _playerInstance = _provider.Instantiate(PrefabsPaths.Rover, position);

            return _playerInstance;
        }

        public GameObject GetPlayerInstance()
        {
            if (_playerInstance == null)
                throw new InvalidOperationException();

            return _playerInstance;
        }

        public GameObject CreateGameCamera()
        {
            return _provider.Instantiate(PrefabsPaths.GameCamera);
        }

        public GameObject CreateSpawner()
        {
            int currentsceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (currentsceneIndex == (int) SceneIndex.Tutorial)
                return _provider.Instantiate(PrefabsPaths.TutorialSpawner);
            else
                return _provider.Instantiate(PrefabsPaths.Spawner);
        }

        public UIControlInput GetUiControlInput()
        {
            GameObject uiRoot = _uiWindowFactory.GetUIRoot();

            GameObject uiPlayerInputObject = _uiWindowFactory.GetWindow(PrefabsPaths.UiControlInput, uiRoot);

            return uiPlayerInputObject.GetComponent<UIControlInput>();
        }

        public DesktopControlInput GetDesktopControlInput(Transform parent)
        {
            GameObject controlInputObject = _provider.Instantiate(PrefabsPaths.DesktopControlInput, parent);

            return controlInputObject.GetComponent<DesktopControlInput>();
        }
    }
}
