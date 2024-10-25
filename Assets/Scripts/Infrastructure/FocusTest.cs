using UnityEngine;
using Agava.WebUtility;
using UnityEngine.SceneManagement;

public class FocusTest : MonoBehaviour
{
    private bool _isUnfocusePaused;
    private bool _isFocused;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _isFocused = true;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Application.focusChanged += OnInBackGroundChangeApp;
        WebApplication.InBackgroundChangeEvent += OnInBackGroundChangeWeb;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Application.focusChanged -= OnInBackGroundChangeApp;
        WebApplication.InBackgroundChangeEvent -= OnInBackGroundChangeWeb;
    }

    public bool IsFocused()
    {
        return _isFocused;
    }
    
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        if (currentSceneIndex == (int) SceneIndex.Gameplay || currentSceneIndex == (int) SceneIndex.Tutorial)
            DIServicesContainer.Instance.GetService<IFocusTestStateChanger>().DisablePauseMenuOpening();
        
        if (_isFocused == false)
        {
            MuteAudio(true);
            PauseGame(true);
        }
    }

    private void OnInBackGroundChangeApp(bool inApp)
    {
        _isFocused = inApp;
        MuteAudio(inApp == false);
        PauseGame(inApp == false);
    }

    private void OnInBackGroundChangeWeb(bool isBackground)
    {
        _isFocused = !isBackground;
        MuteAudio(isBackground);
        PauseGame(isBackground);
    }

    private void MuteAudio(bool value)
    {
        if (value)
        {
            DIServicesContainer.Instance.GetService<IAudioPlayback>().MuteAudio();
        }
        else
        {
            DIServicesContainer.Instance.GetService<IAudioPlayback>().UnMuteAudio();
        }
    }

    private void PauseGame(bool value)
    {
        if (value && Time.timeScale != 0)
        {
            if (DIServicesContainer.Instance.GetService<IFocusTestStateChanger>().IsNeedToOpenPauseMenu)
            {
                DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStatePauseMenu>();
                
                return;
            }
            
            _isUnfocusePaused = true;
            Time.timeScale = 0f;
        }

        if (_isUnfocusePaused && value == false)
        {
            Time.timeScale = 1f;
            _isUnfocusePaused = false;
        }
    }
}