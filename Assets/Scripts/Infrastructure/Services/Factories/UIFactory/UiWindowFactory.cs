using Data;
using Infrastructure.Services.AssetsProvider;
using UnityEngine;

namespace Infrastructure.Services.Factories.UiFactory
{
    public class UiWindowFactory : IUiWindowFactory
    {
        private readonly IAssetsProvider _provider;
        private GameObject _uiRootObject;
        private GameObject _levelCompleteWindow;

        public UiWindowFactory(IAssetsProvider provider)
        {
            _provider = provider;
        }

        public GameObject GetUIRoot()
        {
            return _uiRootObject ? _uiRootObject : _uiRootObject = _provider.Instantiate(PrefabsPaths.UIRoot);
        }

        public void DeleteUIRoot()
        {
            _uiRootObject = null;
        }

        public GameObject GetLevelCompleteWindow(GameObject parent)
        {
            if (_levelCompleteWindow == null)
                _levelCompleteWindow = _provider.Instantiate(PrefabsPaths.LevelCompleteWindow, parent.transform);

            return _levelCompleteWindow;
        }

        public GameObject GetWindow(string path, GameObject parent)
        {
            return _provider.Instantiate(path, parent.transform);
        }

        public void ShowUIObject(string path, GameObject parent)
        {
            _ = _provider.Instantiate(path, parent.transform);
        }
    }
}
