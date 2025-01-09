using Reflex.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.ScreenFader
{
    public class LoadScreen : MonoBehaviour
    {
        private IScreenFader _screenFader;
        
        [Inject]
        private void Construct(IScreenFader screenFader)
        {
            _screenFader = screenFader;
        }
        
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
            _screenFader.FadeIn();
        }
    }
}
