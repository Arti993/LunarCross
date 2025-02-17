using System;
using Data;
using DG.Tweening;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.FocusTest;
using Infrastructure.UIStateMachine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.ScreenFader
{
    public class ScreenFader : IScreenFader
    {
        private const float FadeDuration = 0.5f;
        private const float Delay = 0.1f;
        private GameObject _screenFaderObject;
        private Image _blackScreen;
        private IFocusTestStateChanger _focusTestStateChanger;
        private IUiStateMachine _uiStateMachine;
        private IAssetsProvider _provider;

        public ScreenFader(IAssetsProvider provider, IFocusTestStateChanger focusTestStateChanger, IUiStateMachine uiStateMachine)
        {
            _focusTestStateChanger = focusTestStateChanger;
            _uiStateMachine = uiStateMachine;
            _provider = provider;
        }

        public event Action FadingCompleted;
        public event Action FadingStarted;

        public bool IsActive()
        {
            return _screenFaderObject.activeSelf;
        }

        public void FadeIn()
        {
            if (_screenFaderObject == null)
            {
                _screenFaderObject = _provider.Instantiate(PrefabsPaths.ScreenFader);

                _blackScreen = _screenFaderObject.GetComponentInChildren<Image>();
            }
            
            FadingStarted?.Invoke();

            _screenFaderObject.SetActive(true);

            Sequence sequence = DOTween.Sequence();

            sequence.AppendInterval(Delay);

            sequence.Append(_blackScreen.DOFade(0f, FadeDuration));

            sequence.OnComplete(() =>
            {
                _screenFaderObject.SetActive(false);

                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

                if (currentSceneIndex == (int) SceneIndex.Gameplay || currentSceneIndex == (int) SceneIndex.Tutorial)
                    _focusTestStateChanger.EnablePauseMenuOpening();

                FadingCompleted?.Invoke();

                sequence.Kill();
            });
        }

        public void FadeOutAndLoadScene(int sceneIndex)
        {
            Sequence sequence = DOTween.Sequence();

            sequence.AppendInterval(Delay).SetUpdate(true);

            sequence.AppendCallback(() =>
            {
                FadingStarted?.Invoke();

                _uiStateMachine.ExitCurrentState();

                _screenFaderObject.SetActive(true);
            });

            sequence.Append(_blackScreen.DOFade(1f, FadeDuration).SetUpdate(true)).OnComplete(() =>
            {
                Time.timeScale = 1f;

                Resources.UnloadUnusedAssets();

                FadingCompleted?.Invoke();

                SceneManager.LoadScene(sceneIndex);

                sequence.Kill();
            });
        }
    }
}
