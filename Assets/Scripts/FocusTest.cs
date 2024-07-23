using System;
using UnityEngine;
using Agava.WebUtility;

public class FocusTest : MonoBehaviour
{
    //[SerializeField] private AudioSource _audioSource;

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
        //_audioSource.volume = value ? 0 : 1;
    }

    private void PauseGame(bool value)
    {
        Time.timeScale = value ? 0 : 1;
    }
}
