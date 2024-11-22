using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.ScreenFader
{
    public class LoadScreen : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            DIServicesContainer.Instance.GetService<IScreenFader>().FadeIn();
        }
    }
}
