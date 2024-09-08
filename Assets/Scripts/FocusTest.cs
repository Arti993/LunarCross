using UnityEngine;
using Agava.WebUtility;
using UnityEngine.SceneManagement;

public class FocusTest : MonoBehaviour
{
    private bool _isGameAlreadyPaused;

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
            DIServicesContainer.Instance.GetService<IAudioPlayback>().UnMuteAudio();
    }

    private void PauseGame(bool value)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int tutorialSceneIndex = DIServicesContainer.Instance.GetService<IScenesLoader>().GetTutorialSceneIndex();

        int gameplaySceneIndex = DIServicesContainer.Instance.GetService<IScenesLoader>().GetGameplaySceneIndex();

        if (currentSceneIndex == gameplaySceneIndex || currentSceneIndex == tutorialSceneIndex)
        {
            if (Time.timeScale != 0)
            {
                GameObject uiRoot = DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();

                DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetPauseMenuWindow(uiRoot);
            }

            return;
        }

        if (Time.timeScale != 0)
            _isGameAlreadyPaused = false;

        if (Time.timeScale == 0 && value)
            _isGameAlreadyPaused = true;

        if (_isGameAlreadyPaused == false && value)
            Time.timeScale = 0;

        if (_isGameAlreadyPaused == false && value == false)
            Time.timeScale = 1;
    }
}