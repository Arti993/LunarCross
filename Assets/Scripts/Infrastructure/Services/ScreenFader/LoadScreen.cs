using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.ScreenFader
{
    public class LoadScreen : MonoBehaviour
    {
        private IScreenFader _screenFader;
        
        private void Construct()
        {
            _screenFader = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IScreenFader>();
        }
        
        private void Awake()
        {
            Construct();
            
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
