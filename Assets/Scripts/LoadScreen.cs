using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScreen : MonoBehaviour
{
    private static bool _isFirstAwakened;
    
    private void Awake()
    {
        if (_isFirstAwakened) 
            return;
        
        DontDestroyOnLoad(gameObject);
        
        SceneManager.sceneLoaded += OnSceneLoaded;

        _isFirstAwakened = true;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        AllServicesContainer.Instance.GetService<IScreenFader>().FadeIn();
    }
}
