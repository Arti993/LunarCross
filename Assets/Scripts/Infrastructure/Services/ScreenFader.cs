using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFader : IScreenFader
{
    private const float FadeDuration = 0.5f;
    private const float Delay = 0.1f;
    private readonly GameObject _screenFaderObject;
    private readonly Image _blackScreen;
    private readonly LoadScreen _loadScreen;

    
    public ScreenFader(IAssetsProvider provider)
    {
        _screenFaderObject = provider.Instantiate(PrefabsPaths.ScreenFader);

        _blackScreen = _screenFaderObject.GetComponentInChildren<Image>();

        _screenFaderObject.TryGetComponent(out LoadScreen loadScreen);

        if (loadScreen != null)
            _loadScreen = loadScreen;

        FadeIn();
    }

    public event Action FadingComplete;
    public event Action FadingStart;

    public bool IsActive()
    {
        return _screenFaderObject.activeSelf;
    }

    public void FadeIn()
    {
        FadingStart?.Invoke();
        
        _screenFaderObject.SetActive(true);
        
        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(Delay);

        sequence.Append(_blackScreen.DOFade(0f, FadeDuration));
        
        sequence.OnComplete(() =>
        {
            _screenFaderObject.SetActive(false);
            
            FadingComplete?.Invoke();
        });
    }

    public void FadeOutAndLoadScene(int sceneIndex)
    {
        FadingStart?.Invoke();
        
        _screenFaderObject.SetActive(true);
        
        _blackScreen.DOFade(1f, FadeDuration).SetUpdate(true).OnComplete(() =>
        {
            Time.timeScale = 1f;
            
            Resources.UnloadUnusedAssets();

            SceneManager.LoadScene(sceneIndex);
        });
    }
}
