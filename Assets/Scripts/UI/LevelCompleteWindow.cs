using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Linq;
using Ami.BroAudio;
using Data;
using Infrastructure;
using Infrastructure.Services.AudioPlayback;
using Infrastructure.Services.Factories.ParticleSystemFactory;
using Infrastructure.Services.FocusTest;
using Infrastructure.Services.GameProgress;
using Infrastructure.Services.LevelSettings;
using Infrastructure.Services.ScreenFader;
using Reflex.Attributes;
using ScriptableObjects;

namespace UI
{
    [RequireComponent(typeof(RectTransform))]
    public class LevelCompleteWindow : MenuEscapeWindow
    {
        private const int LoopsCount = 2;
        private const float HalfFactor = 0.5f;

        [SerializeField] private GameObject[] _ratingStars;
        [SerializeField] private TMP_Text _pointsLabel;
        [SerializeField] private LevelEndNextButton _levelEndNextButton;
        [SerializeField] private float _pointsTextSizeMultiplier = 2;
        [SerializeField] private float _sizeChangeAnimationDuration = 0.3f;

        private List<Tweener> _tweeners = new List<Tweener>();
        private int _pointsForFirstStar;
        private int _pointsForSecondStar;
        private int _pointsForThirdStar;
        private int _points;
        private int _starsCount;
        private int _currentSceneIndex;
        private IFocusTestStateChanger _focusTestStateChanger;
        private IGameProgress _gameProgress;
        private IParticleSystemFactory _particleSystemFactory;
        private IAudioPlayback _audioPlayback;
        private IScreenFader _screenFader;
        private ILevelsSettingsNomenclature _levelsSettingsNomenclature;

        [Inject]
        private void Construct(IFocusTestStateChanger focusTestStateChanger, IGameProgress gameProgress,
            IParticleSystemFactory particleSystemFactory, IAudioPlayback audioPlayback, IScreenFader screenFader,
            ILevelsSettingsNomenclature levelsSettingsNomenclature)
        {
            _focusTestStateChanger = focusTestStateChanger;
            _gameProgress = gameProgress;
            _particleSystemFactory = particleSystemFactory;
            _audioPlayback = audioPlayback;
            _screenFader = screenFader;
            _levelsSettingsNomenclature = levelsSettingsNomenclature;
        }

        private void Awake()
        {
            _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            Level currentLevel = GetLevelSettings();

            _pointsForFirstStar = currentLevel.PointsForFirstStar;
            _pointsForSecondStar = currentLevel.PointsForSecondStar;
            _pointsForThirdStar = currentLevel.PointsForThirdStar;

            foreach (var ratingStar in _ratingStars)
            {
                ratingStar.SetActive(false);
            }

            _starsCount = 0;
            _points = 0;
            _pointsLabel.text = _points.ToString();

            _focusTestStateChanger.DisablePauseMenuOpening();
        }

        private void Start()
        {
            _levelEndNextButton.SetNotInterractable();
        }

        private void OnDisable()
        {
            foreach (Tweener tweener in _tweeners)
            {
                tweener.Kill();
            }
        }

        public void CollectPoint()
        {
            _points++;
            _pointsLabel.text = _points.ToString();

            Vector3 baseScale = _pointsLabel.transform.localScale;
            Vector3 increasedScale = baseScale * _pointsTextSizeMultiplier;

            Tweener tweener = _pointsLabel.transform.DOScale(increasedScale,
                _sizeChangeAnimationDuration * HalfFactor).SetLoops(LoopsCount, LoopType.Yoyo);

            _tweeners.Add(tweener);

            _particleSystemFactory.ShowYellowBurstEffect(_pointsLabel.transform.position);

            if (new[] {_pointsForFirstStar, _pointsForSecondStar, _pointsForThirdStar}.Any(p => _points == p))
                GetStar();
        }

        public void GoToNextScreen()
        {
            if (_currentSceneIndex == (int) SceneIndex.Tutorial)
            {
                FromGamePlayToMainMenu();

                return;
            }

            if (PlayerPrefs.HasKey("GameIsComplete") && _gameProgress.IsCurrentLevelLast())
            {
                _screenFader.FadeOutAndLoadScene((int) SceneIndex.Final);

                return;
            }

            PlayerPrefs.DeleteKey("SelectedLevelNumber");

#if UNITY_WEBGL && !UNITY_EDITOR
    DIServicesContainer.Instance.GetService<IInterstitionalAdService>().ShowAd();
#endif

            _screenFader.FadeOutAndLoadScene((int) SceneIndex.LevelChoose);
        }

        public void RestartLevel()
        {
            _screenFader.FadeOutAndLoadScene(_currentSceneIndex);
        }

        public void EvaluatePassage()
        {
            if (_currentSceneIndex == (int) SceneIndex.Tutorial)
            {
                _levelEndNextButton.SetInterractable();
                return;
            }

            if (_points >= _pointsForFirstStar)
            {
                _gameProgress.SaveLevelProgress(_points);

                _levelEndNextButton.SetInterractable();
            }
        }

        private Level GetLevelSettings()
        {
            Level currentLevel;

            if (_currentSceneIndex == (int) SceneIndex.Tutorial)
            {
                currentLevel = _levelsSettingsNomenclature.GetTutorialLevelSettings();
            }
            else
            {
                int levelNumber = _gameProgress.GetCurrentLevelNumber();

                currentLevel = _levelsSettingsNomenclature.GetLevelSettings(levelNumber);
            }

            return currentLevel;
        }

        private void GetStar()
        {
            _starsCount++;

            GameObject currentStar = _ratingStars[_starsCount - 1];

            Vector3 initialScale = Vector3.zero;
            Vector3 baseScale = currentStar.transform.localScale;

            currentStar.transform.localScale = initialScale;

            currentStar.SetActive(true);

            Tweener tweener = currentStar.transform.DOScale(baseScale, _sizeChangeAnimationDuration);

            _tweeners.Add(tweener);

            _particleSystemFactory.ShowYellowBurstEffect(currentStar.transform.position);

            SoundID starCollect = _audioPlayback.SoundsContainer.StarCollect;

            _audioPlayback.PlaySound(starCollect);
        }
    }
}