using UnityEngine;

public class AssetsProvider : IAssetsProvider
{
    public GameObject Instantiate(string path)
    {
        GameObject prefab = Resources.Load<GameObject>(path);

        return Object.Instantiate(prefab);
    }

    public GameObject Instantiate(string path, Vector3 position)
    {
        GameObject prefab = Resources.Load<GameObject>(path);

        return Object.Instantiate(prefab, position, Quaternion.identity);
    }

    public GameObject Instantiate(string path, Vector3 position, Quaternion rotation)
    {
        GameObject prefab = Resources.Load<GameObject>(path);

        return Object.Instantiate(prefab, position, rotation);
    }

    public GameObject Instantiate(string path, Transform parent)
    {
        GameObject prefab = Resources.Load<GameObject>(path);

        return Object.Instantiate(prefab, parent);
    }
}
