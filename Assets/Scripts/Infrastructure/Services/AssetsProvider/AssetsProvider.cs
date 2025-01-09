using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services.AssetsProvider
{
    public class AssetsProvider : IAssetsProvider
    {
        private Dictionary<string, GameObject> _loadedPrefabs = new Dictionary<string, GameObject>();
        private GameObject _prefabToInstantiate;

        public GameObject Instantiate(string path)
        {
            return GameObject.Instantiate(GetPrefab(path));
        }

        public GameObject Instantiate(string path, Vector3 position)
        {
            return GameObject.Instantiate(GetPrefab(path), position, Quaternion.identity);
        }
        
        public GameObject Instantiate(string path, Transform parent)
        {
            return GameObject.Instantiate(GetPrefab(path), parent);
        }

        private GameObject GetPrefab(string path)
        {
            if (_loadedPrefabs.ContainsKey(path))
            {
                _ = _loadedPrefabs.TryGetValue(path, out GameObject prefab);

                _prefabToInstantiate = prefab;
            }
            else
            {
                _prefabToInstantiate = Resources.Load<GameObject>(path);

                _loadedPrefabs.Add(path, _prefabToInstantiate);
            }

            return _prefabToInstantiate;
        }
    }
}