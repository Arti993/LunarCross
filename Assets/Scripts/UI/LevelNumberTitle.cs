using UnityEngine;
using DG.Tweening;
using Infrastructure;
using Infrastructure.Services.GameProgress;
using TMPro;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LevelNumberTitle : MonoBehaviour
    {
        private const float AnimateTime = 1.2f;
        private const float MaxScale = 2;
        private const int TutorialSceneIndex = 4;

        [SerializeField] private TMP_Text _levelNumber;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _tutorial;

        private Sequence _sequence;


        private void Start()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (sceneIndex == TutorialSceneIndex)
            {
                _level.gameObject.SetActive(false);
                _tutorial.gameObject.SetActive(true);
                _levelNumber.text = "";

                Animate(_tutorial);
            }
            else
            {
                _level.gameObject.SetActive(true);
                _tutorial.gameObject.SetActive(false);

                int levelNumber = DIServicesContainer.Instance.GetService<IGameProgress>().GetCurrentLevelNumber();

                _levelNumber.text = $"{levelNumber}";

                Animate(_level, _levelNumber);
            }
        }

        private void OnDisable()
        {
            _sequence.Kill();
        }

        private void Animate(TMP_Text text)
        {
            _sequence = DOTween.Sequence();

            _sequence.Append(text.transform.DOScale(MaxScale, AnimateTime));
            _sequence.Append(text.DOFade(0f, AnimateTime));

            _sequence.OnComplete(() => { gameObject.SetActive(false); });
        }

        private void Animate(TMP_Text text, TMP_Text number)
        {
            _sequence = DOTween.Sequence();

            _sequence.Append(text.transform.DOScale(MaxScale, AnimateTime));
            _sequence.Append(text.DOFade(0f, AnimateTime));
            _sequence.Join(number.DOFade(0f, AnimateTime));

            _sequence.OnComplete(() => { gameObject.SetActive(false); });
        }
    }
}
