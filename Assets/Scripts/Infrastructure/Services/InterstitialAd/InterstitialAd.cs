using Infrastructure.Services.FocusTest;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Infrastructure.Services.InterstitialAd
{
    public class InterstitialAd : MonoBehaviour
    {
        private IFocusTestStateChanger _focusTestStateChanger;
        
        private void Construct()
        {
            _focusTestStateChanger = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IFocusTestStateChanger>();
        }

        private void Awake()
        {
            Construct();
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

