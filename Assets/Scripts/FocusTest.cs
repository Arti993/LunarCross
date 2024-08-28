using UnityEngine;
using Agava.WebUtility;

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
        if(value)
            DIServicesContainer.Instance.GetService<IAudioPlayback>().MuteAudio();
        else
            DIServicesContainer.Instance.GetService<IAudioPlayback>().UnMuteAudio();
    }

    private void PauseGame(bool value)
    {
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
