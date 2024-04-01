using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ScreenFader : IScreenFader
{
    private const float FadeDuration = 1f;
    private readonly GameObject _screenFader;
    private readonly Image _blackScreen;
    
    
    public ScreenFader(IAssets provider)
    {
        _screenFader = provider.Instantiate("Prefabs/UI/ScreenFader");

        _blackScreen = _screenFader.GetComponentInChildren<Image>();

        FadeIn();
    }

    public void FadeIn()
    {
        _screenFader.SetActive(true);
        
        _blackScreen.DOFade(0f, FadeDuration).OnComplete(() =>
        {
            _screenFader.SetActive(false);
        });
    }

    public void FadeOutAndLoadScene(int sceneIndex)
    {
        _screenFader.SetActive(true);
        
        _blackScreen.DOFade(1f, FadeDuration).SetUpdate(true).OnComplete(() =>
        {
            Time.timeScale = 1f;
            
            SceneManager.LoadScene(sceneIndex);
        });
    }
}
