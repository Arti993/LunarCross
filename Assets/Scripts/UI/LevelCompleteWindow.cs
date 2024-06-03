using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Linq;
using IJunior.TypedScenes;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class LevelCompleteWindow : UIWindow
{
    private const int TutorialSceneIndex = 4;
    
    [SerializeField] private GameObject[] _ratingStars;
    [SerializeField] private TMP_Text _pointsLabel;
    [SerializeField] private Button _continueButton;
    [SerializeField] private float _resultsPanelTopPosY = 0f;
    [SerializeField] private float _panelAnimationDuration = 0.5f;
    [SerializeField] private float _pointsTextSizeMultiplier = 2;
    [SerializeField] private float _sizeChangeAnimationDuration = 0.3f;
    
    private int _pointsForFirstStar;
    private int _pointsForSecondStar;
    private int _pointsForThirdStar;
   
    private RectTransform _resultsPanelRect;
    private int _points;
    private int _starsCount;
    
    private void Awake()
    {
        _resultsPanelRect = GetComponent<RectTransform>();

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
        
        AllServicesContainer.Instance.GetService<IParticleSystemFactory>()
            .GetYellowBurstEffect(_pointsLabel.transform.position);

       if(new[] { _pointsForFirstStar, _pointsForSecondStar, _pointsForThirdStar }.Any(p => _points == p))
            GetStar();
    }

    public void GoToLevelChooseScene()
    {
        PlayerPrefs.DeleteKey("SelectedLevelNumber");
        
        LevelsChoose.Load();
    }
    
    public void RestartLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        AllServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(sceneIndex);
    }

    public void EvaluatePassage()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        if(sceneIndex != TutorialSceneIndex)
            AllServicesContainer.Instance.GetService<IGameProgress>().SaveLevelProgress(_points);
        
        _continueButton.interactable = true;
    }

    private Level GetLevelSettings()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        Level currentLevel;
        
        if (sceneIndex != TutorialSceneIndex)
        {
            int levelNumber = AllServicesContainer.Instance.GetService<IGameProgress>().GetCurrentLevelNumber();

            currentLevel = AllServicesContainer.Instance.GetService<ILevelsSettingsNomenclature>()
                .GetLevelSettings(levelNumber);
        }
        else
        {
            currentLevel = AllServicesContainer.Instance.GetService<ILevelsSettingsNomenclature>()
                .GetTutorialLevelSettings();
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
        
        AllServicesContainer.Instance.GetService<IParticleSystemFactory>()
            .GetYellowBurstEffect(currentStar.transform.position);
    }

    private void PanelIntro()
    {
        _resultsPanelRect.DOAnchorPosY(_resultsPanelTopPosY, _panelAnimationDuration).SetUpdate(true);
    }
}
