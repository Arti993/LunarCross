using Data;
using Infrastructure.Services.GameProgress;
using Infrastructure.Services.LevelSettings;
using Infrastructure.Services.ScreenFader;
using Reflex.Extensions;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class LevelPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _unblockedView;
        [SerializeField] private GameObject _blockedView;
        [SerializeField] private GameObject[] _ratingStars;
        [SerializeField] private TMP_Text _levelNumberTitle;
        [SerializeField] private TMP_Text _bestRecordTitle;
        [SerializeField] private int _levelNumber;

        private Button _button;
        private IGameProgress _gameProgress;
        private IScreenFader _screenFader;
        private ILevelsSettingsNomenclature _levelsSettingsNomenclature;
        
        private void Construct()
        {
            _gameProgress = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IGameProgress>();

            _screenFader = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IScreenFader>();

            _levelsSettingsNomenclature = SceneManager.GetActiveScene().GetSceneContainer().Resolve<ILevelsSettingsNomenclature>();
        }

        private void Awake()
        {
            Construct();
            
            _button = GetComponent<Button>();

            if (PlayerPrefs.GetInt("ReachedLevelNumber", 1) < _levelNumber)
            {
                _unblockedView.SetActive(false);
                _blockedView.SetActive(true);
                _button.interactable = false;
            }
            else
            {
                _unblockedView.SetActive(true);
                _blockedView.SetActive(false);

                if (_screenFader.IsActive() == false)
                    _button.interactable = true;

                _levelNumberTitle.text = _levelNumber.ToString();

                DisplayProgress();
            }
        }

        public void LoadLevel()
        {
            _gameProgress.SelectLevel(_levelNumber);

            _screenFader.FadeOutAndLoadScene((int) SceneIndex.Gameplay);
        }

        private void DisplayProgress()
        {
            int levelResult = _gameProgress.GetLevelResult(_levelNumber);

            _bestRecordTitle.text = levelResult.ToString();

            DisplayStars(levelResult);
        }

        private void DisplayStars(int levelResult)
        {
            Level levelSettings = _levelsSettingsNomenclature.GetLevelSettings(_levelNumber);
            
            foreach (var star in _ratingStars)
            {
                star.SetActive(false);
            }

            int starsToDisplay = _ratingStars.Length;

            if (levelResult < levelSettings.PointsForThirdStar)
                starsToDisplay--;

            if (levelResult < levelSettings.PointsForSecondStar)
                starsToDisplay--;

            if (levelResult < levelSettings.PointsForFirstStar)
                starsToDisplay--;

            for (int i = 0; i < starsToDisplay; i++)
            {
                _ratingStars[i].SetActive(true);
            }
        }
    }
}
