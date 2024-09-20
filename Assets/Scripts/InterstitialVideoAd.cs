using UnityEngine;

public class InterstitialVideoAd : MonoBehaviour
{
    public void Show() => Agava.YandexGames.InterstitialAd.Show(OnOpenCallback, OnCloseCallback);

    private void OnOpenCallback()
    {
        DIServicesContainer.Instance.GetService<IFocusTestStateChanger>().DisableFocusTest();
        
        AudioListener.volume = 0.01f;
    }

    private void OnCloseCallback(bool value)
    {
        AudioListener.volume = 1f;
        
        DIServicesContainer.Instance.GetService<IFocusTestStateChanger>().EnableFocusTest();
    }
}
