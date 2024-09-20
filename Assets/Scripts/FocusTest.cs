using UnityEngine;
using Agava.WebUtility;
using UnityEngine.SceneManagement;

public class FocusTest : MonoBehaviour
{
    private bool _isUnfocusePaused;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        Application.focusChanged += OnInBackGroundChangeApp;
        WebApplication.InBackgroundChangeEvent += OnInBackGroundChangeWeb;
    }

    private void OnDisable()
    {
        Application.focusChanged -= OnInBackGroundChangeApp;
        WebApplication.InBackgroundChangeEvent -= OnInBackGroundChangeWeb;
    }

    private void OnInBackGroundChangeApp(bool inApp)
    {
        MuteAudio(!inApp);
        PauseGame(!inApp);
    }

    private void OnInBackGroundChangeWeb(bool isBackground)
    {
        MuteAudio(isBackground);
        PauseGame(isBackground);
    }

    private void MuteAudio(bool value)
    {
        if (value)
            DIServicesContainer.Instance.GetService<IAudioPlayback>().MuteAudio();
        else
        {
            DIServicesContainer.Instance.GetService<IAudioPlayback>().UnMuteAudio();
        }
    }

    private void PauseGame(bool value)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == (int)SceneIndex.Gameplay || currentSceneIndex == (int)SceneIndex.Tutorial)
        {
            if (Time.timeScale != 0 && DIServicesContainer.Instance.GetService<IScreenFader>().IsActive() == false)
            {
                GameObject uiRoot = DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();

                DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetWindow(PrefabsPaths.PauseMenu, uiRoot);
                
                return;
            }
        }

        if (Time.timeScale != 0
            && DIServicesContainer.Instance.GetService<IScreenFader>().IsActive() == false
            && value)
        {
            _isUnfocusePaused = true;
        }

        if (_isUnfocusePaused && value)
            Time.timeScale = 0f;

        if (_isUnfocusePaused && value == false)
        {
            Time.timeScale = 1f;
            _isUnfocusePaused = false;
        }
    }
}