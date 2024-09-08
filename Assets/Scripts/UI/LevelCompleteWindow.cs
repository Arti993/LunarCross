using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Linq;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class LevelCompleteWindow : GameplayUIWindow
{
    [SerializeField] private GameObject[] _ratingStars;
    [SerializeField] private TMP_Text _pointsLabel;
    [SerializeField] private Button _continueButton;
    [SerializeField] private float _pointsTextSizeMultiplier = 2;
    [SerializeField] private float _sizeChangeAnimationDuration = 0.3f;
    
    private int _pointsForFirstStar;
    private int _pointsForSecondStar;
    private int _pointsForThirdStar;
    private int _points;
    private int _starsCount;
    private int _currentSceneIndex;
    private int _tutorialSceneIndex;
    
    private void Awake()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        _tutorialSceneIndex = DIServicesContainer.Instance.GetService<IScenesLoader>().GetTutorialSceneIndex();
        
        Level currentLevel = GetLevelSettings();

        _pointsForFirstStar = currentLevel.PointsForFirstStar;
        _pointsForSecondStar = currentLevel.PointsForSecondStar;
        _pointsForThirdStar = currentLevel.PointsForThirdStar;
        
        foreach (var ratingStar in _ratingStars)
        {
            ratingStar.SetActive(false);
        }
        
        DestroyPauseButton();

        _starsCount = 0;
        _points = 0;
        _pointsLabel.text = _points.ToString();

        _continueButton.interactable = false;
        
        PanelIntro();
    }
    
    public void CollectPoint()
    {
        _points++;
        _pointsLabel.text = _points.ToString();

        Vector3 baseScale = _pointsLabel.transform.localScale;
        Vector3 increasedScale = baseScale * _pointsTextSizeMultiplier;

        _pointsLabel.transform.DOScale(increasedScale, _sizeChangeAnimationDuration / 2).SetLoops(2, LoopType.Yoyo);
        
        DIServicesContainer.Instance.GetService<IParticleSystemFactory>()
            .GetYellowBurstEffect(_pointsLabel.transform.position);

       if(new[] { _pointsForFirstStar, _pointsForSecondStar, _pointsForThirdStar }.Any(p => _points == p))
            GetStar();
    }

    public void GoToLevelChooseScene()
    {
        if (_currentSceneIndex == _tutorialSceneIndex)
        {
            DIServicesContainer.Instance.GetService<IScenesLoader>().LoadMainMenuScene();
            
            return;
        }

        PlayerPrefs.DeleteKey("SelectedLevelNumber");
        
#if UNITY_WEBGL && !UNITY_EDITOR
    DIServicesContainer.Instance.GetService<IVideoAdService>().ShowInterstitialAd();
#endif

        DIServicesContainer.Instance.GetService<IScenesLoader>().LoadLevelChooseScene();
    }
    
    public void RestartLevel()
    {
        DIServicesContainer.Instance.GetService<IScenesLoader>().LoadScene(_currentSceneIndex);
    }

    public void EvaluatePassage()
    {
        if (_currentSceneIndex == _tutorialSceneIndex)
        {
            _continueButton.interactable = true;
            return;
        }

        if (_points >= _pointsForFirstStar)
        {
            DIServicesContainer.Instance.GetService<IGameProgress>().SaveLevelProgress(_points);

            _continueButton.interactable = true;
        }
    }

    private Level GetLevelSettings()
    {
        Level currentLevel;
        
        if (_currentSceneIndex == _tutorialSceneIndex)
        {
            currentLevel = DIServicesContainer.Instance.GetService<ILevelsSettingsNomenclature>()
                .GetTutorialLevelSettings();
        }
        else
        {
            int levelNumber = DIServicesContainer.Instance.GetService<IGameProgress>().GetCurrentLevelNumber();
            
            Debug.Log(levelNumber);

            currentLevel = DIServicesContainer.Instance.GetService<ILevelsSettingsNomenclature>()
                .GetLevelSettings(levelNumber);
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

        currentStar.transform.DOScale(baseScale, _sizeChangeAnimationDuration);
        
        DIServicesContainer.Instance.GetService<IParticleSystemFactory>()
            .GetYellowBurstEffect(currentStar.transform.position);
        
        DIServicesContainer.Instance.GetService<IAudioPlayback>().PlayStarCollectingSound();
    }
}
