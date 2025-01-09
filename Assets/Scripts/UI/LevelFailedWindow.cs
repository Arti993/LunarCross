using System.Collections;
using Infrastructure;
using Infrastructure.Services.ScreenFader;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    [RequireComponent(typeof(RectTransform))]
    public class LevelFailedWindow : MenuEscapeWindow
    {
        [SerializeField] private float _stopTimeDelay = 0.15f;

        private bool _isFirstFailReacted;
        private IScreenFader _screenFader;
        
        [Inject]
        private void Construct(IScreenFader screenFader)
        {
            _screenFader = screenFader;
        }

        private void Awake()
        {
            PauseGame();
        }

        public void RestartLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            _screenFader.FadeOutAndLoadScene(currentSceneIndex);
        }

        private void PauseGame()
        {
            if (_isFirstFailReacted)
                return;

            _isFirstFailReacted = true;

            _ = StartCoroutine(StopTime());
        }

        private IEnumerator StopTime()
        {
            yield return new WaitForSeconds(_stopTimeDelay);

            Time.timeScale = 0f;
        }
    }
}
