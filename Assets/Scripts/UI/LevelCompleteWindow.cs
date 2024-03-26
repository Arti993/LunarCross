using TMPro;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using IJunior.TypedScenes;

[RequireComponent(typeof(RectTransform))]
public class LevelCompleteWindow : UIWindow
{
    [SerializeField] private GameObject[] _ratingStars;
    [SerializeField] private TMP_Text _pointsLabel;
    [SerializeField] private float _resultsPanelTopPosY = 0;
    [SerializeField] private float _resultsPanelBottomPosY = -500f;//возможно сделать красивое скрытие перед следующим экраном
    [SerializeField] private float _panelAnimationDuration = 0.5f;
    [SerializeField] private int _pointsForFirstStar = 4;
    [SerializeField] private int _pointsForSecondStar = 8;
    [SerializeField] private int _pointsForThirdStar = 13;
    [SerializeField] private float _pointsTextSizeMultiplier = 2;
    [SerializeField] private float _sizeChangeAnimationDuration = 0.3f;
   
    private RectTransform _resultsPanelRect;
    private int _points;
    private int _starsCount;
    
    private void Awake()
    {
        _resultsPanelRect = GetComponent<RectTransform>();
        
        foreach (var ratingStar in _ratingStars)
        {
            ratingStar.SetActive(false);
        }
        
        DestroyPauseButton();

        _starsCount = 0;
        _points = 0;
        _pointsLabel.text = _points.ToString();
        
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
        LevelsChoose.Load();
    }

    public void EvaluatePassage()
    {
        if (_points >= _pointsForFirstStar)
        {
            AllServicesContainer.Instance.GetService<IGameProgress>().ChangeLevelProgress(_points);
        }
        else
        {
            //сделать анимацию complite и failed и какнибудь предложить рестарт уровня.
        }
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
