using UnityEngine;

namespace Infrastructure.Services.Factories.UiFactory
{
    public interface IUiWindowFactory : IService
    {
        public GameObject GetUIRoot();
        public void DeleteUIRoot();
        public GameObject GetLevelCompleteWindow(GameObject parent);
        public GameObject GetWindow(string path, GameObject parent);
        public void ShowUIObject(string path, GameObject parent);
    }
}
