using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelGeneration
{
    public class EntitiesObjectPool
    {
        private Entity _prefab;
        private IReadOnlyList<Entity> _prefabs;
        private List<Entity> _objects;
        private bool _isManyPrefabs;

        public EntitiesObjectPool(Entity prefab, int prewarmObjectsCount)
        {
            _isManyPrefabs = false;

            _prefab = prefab;

            PrepareObjects(prewarmObjectsCount);
        }

        public EntitiesObjectPool(IReadOnlyList<Entity> prefabs, int prewarmObjectsCount)
        {
            if (prefabs.Count == 1)
            {
                _prefab = prefabs.FirstOrDefault();
                _isManyPrefabs = false;
            }
            else
            {
                _prefabs = prefabs;
                _isManyPrefabs = true;
            }

            PrepareObjects(prewarmObjectsCount);
        }

        public Entity Get()
        {
            var obj = _objects.FirstOrDefault(x => x.isActiveAndEnabled == false);

            if (obj == null)
                obj = Create();

            obj.gameObject.SetActive(true);

            return obj;
        }

        private void Release(Entity obj)
        {
            obj.gameObject.SetActive(false);
        }

        private Entity Create()
        {
            Entity obj;

            if (_isManyPrefabs)
                obj = GameObject.Instantiate(GetRandomPrefab());
            else
                obj = GameObject.Instantiate(_prefab);

            _objects.Add(obj);

            obj.Disabled += OnObjectDisabled;

            return obj;
        }

        private void PrepareObjects(int prewarmObjectsCount)
        {
            _objects = new List<Entity>();

            for (int i = 0; i < prewarmObjectsCount; i++)
            {
                var obj = Create();
                Release(obj);
            }
        }

        private void OnObjectDisabled(Entity obj)
        {
            Release(obj);
        }

        private Entity GetRandomPrefab()
        {
            return _prefabs[Random.Range(0, _prefabs.Count)];
        }
    }
}