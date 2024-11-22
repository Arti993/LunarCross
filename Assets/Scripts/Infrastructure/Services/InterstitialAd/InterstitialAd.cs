using Infrastructure.Services.FocusTest;
using UnityEngine;
using YG;

namespace Infrastructure.Services.InterstitialAd
{
    public class InterstitialAd : MonoBehaviour
    {
        private void OnEnable()
        {
            YandexGame.OpenFullAdEvent += OnOpenCallback;
            YandexGame.CloseFullAdEvent += OnCloseCallback;
        }

        private void OnDisable()
        {
            YandexGame.OpenFullAdEvent -= OnOpenCallback;
            YandexGame.CloseFullAdEvent -= OnCloseCallback;
        }

        public void Show() => YandexGame.FullscreenShow();

        private void OnOpenCallback()
        {
            DIServicesContainer.Instance.GetService<IFocusTestStateChanger>().DisableFocusTest();

            AudioListener.pause = true;
        }

        private void OnCloseCallback()
        {
            AudioListener.pause = false;

            DIServicesContainer.Instance.GetService<IFocusTestStateChanger>().EnableFocusTest();
        }
    }
}

