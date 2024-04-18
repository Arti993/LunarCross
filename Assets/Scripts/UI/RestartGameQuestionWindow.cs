using UnityEngine;
using DG.Tweening;

public class RestartGameQuestionWindow : UIWindow
{
    [SerializeField] private RectTransform _panelRect;
    [SerializeField] private float _panelTopPosY;
    [SerializeField] private float _panelBottomPosY = -1000f;
    [SerializeField] private float _panelAnimationDuration = 0.5f;

    private void Awake()
    {
        PanelIntro();
    }

    public void OnYesButtonClick()
    {
        AllServicesContainer.Instance.GetService<IGameProgress>().ClearSaves();
        
        PanelOutro();
    }
    
    public void OnNoButtonClick()
    {
        PanelOutro();
    }

    private void PanelIntro()
    {
        _panelRect.DOAnchorPosY(_panelTopPosY, _panelAnimationDuration).SetUpdate(true);
    }

    private void PanelOutro()
    {
        _panelRect.DOAnchorPosY(_panelBottomPosY, _panelAnimationDuration).SetUpdate(true);
        
        StartCoroutine(Destroy(_panelAnimationDuration));
    }
}
