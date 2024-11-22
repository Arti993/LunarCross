using UnityEngine;

namespace Infrastructure.Services.AssetsProvider
{
    public interface IAssetsProvider : IService
    {
        GameObject Instantiate(string path);

        GameObject Instantiate(string path, Vector3 position);

        GameObject Instantiate(string path, Vector3 position, Quaternion rotation);

        GameObject Instantiate(string path, Transform parent);
    }
}
