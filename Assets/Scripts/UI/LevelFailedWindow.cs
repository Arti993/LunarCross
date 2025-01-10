using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    [RequireComponent(typeof(RectTransform))]
    public class LevelFailedWindow : MenuEscapeWindow
    {
        [SerializeField] private float _stopTimeDelay = 0.15f;

        private bool _isFirstFailReacted;

        protected override void Awake()
        {
            base.Awake();
            
            PauseGame();
        }

        public void RestartLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            ScreenFader.FadeOutAndLoadScene(currentSceneIndex);
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
