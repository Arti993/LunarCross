using Infrastructure.Services.FocusTest;
using Reflex.Attributes;
using UnityEngine;
using YG;

namespace Infrastructure.Services.InterstitialAd
{
    public class InterstitialAd : MonoBehaviour
    {
        private IFocusTestStateChanger _focusTestStateChanger;
        
        [Inject]
        private void Construct(IFocusTestStateChanger focusTestStateChanger)
        {
            _focusTestStateChanger = focusTestStateChanger;
        }
        
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
            _focusTestStateChanger.DisableFocusTest();

            AudioListener.pause = true;
        }

        private void OnCloseCallback()
        {
            AudioListener.pause = false;

            _focusTestStateChanger.EnableFocusTest();
        }
    }
}

