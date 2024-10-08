using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Linq;
using Ami.BroAudio;

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
    
    private int _pointsForFirstStar;
    private int _pointsForSecondStar;
    private int _pointsForThirdStar;
    private int _points;
    private int _starsCount;
    private int _currentSceneIndex;

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

        DIServicesContainer.Instance.GetService<IFocusTestStateChanger>().DisablePauseMenuOpening();
    }

    private void Start()
    {
        _levelEndNextButton.SetNotInterractable();
    }

    public void CollectPoint()
    {
        _points++;
        _pointsLabel.text = _points.ToString();

        Vector3 baseScale = _pointsLabel.transform.localScale;
        Vector3 increasedScale = baseScale * _pointsTextSizeMultiplier;

        _pointsLabel.transform.DOScale(increasedScale, _sizeChangeAnimationDuration * HalfFactor).SetLoops(LoopsCount, LoopType.Yoyo);
        
        DIServicesContainer.Instance.GetService<IParticleSystemFactory>()
            .GetYellowBurstEffect(_pointsLabel.transform.position);

       if(new[] { _pointsForFirstStar, _pointsForSecondStar, _pointsForThirdStar }.Any(p => _points == p))
            GetStar();
    }

    public void GoToNextScreen()
    {
        if (_currentSceneIndex == (int)SceneIndex.Tutorial)
        {
            FromGamePlayToMainMenu();
            
            return;
        }

        if (PlayerPrefs.HasKey("GameIsComplete") && DIServicesContainer.Instance.GetService<IGameProgress>().IsCurrentLevelLast())
        {
            DIServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene((int)SceneIndex.Final);
            
            return;
        }
        
        PlayerPrefs.DeleteKey("SelectedLevelNumber");
        
#if UNITY_WEBGL && !UNITY_EDITOR
    DIServicesContainer.Instance.GetService<IVideoAdService>().ShowInterstitialAd();
#endif
    }
    
    public void RestartLevel()
    {
        DIServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(_currentSceneIndex);
    }

    public void EvaluatePassage()
    {
        if (_currentSceneIndex == (int)SceneIndex.Tutorial)
        {
            _levelEndNextButton.SetInterractable();
            return;
        }

        if (_points >= _pointsForFirstStar)
        {
            DIServicesContainer.Instance.GetService<IGameProgress>().SaveLevelProgress(_points);

            _levelEndNextButton.SetInterractable();
        }
    }

    private Level GetLevelSettings()
    {
        Level currentLevel;
        
        if (_currentSceneIndex == (int)SceneIndex.Tutorial)
        {
            currentLevel = DIServicesContainer.Instance.GetService<ILevelsSettingsNomenclature>()
                .GetTutorialLevelSettings();
        }
        else
        {
            int levelNumber = DIServicesContainer.Instance.GetService<IGameProgress>().GetCurrentLevelNumber();

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

        SoundID starCollect = DIServicesContainer.Instance.GetService<IAudioPlayback>().SoundsContainer.StarCollect;
        
        DIServicesContainer.Instance.GetService<IAudioPlayback>().PlaySound(starCollect);
    }
}
