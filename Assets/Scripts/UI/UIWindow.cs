using DG.Tweening;
using UnityEngine;

public class UIWindow : MonoBehaviour
{
    [SerializeField] protected RectTransform PanelRect;
    [SerializeField] protected float PanelTopPosY;
    [SerializeField] protected float PanelBottomPosY = -1500f;
    [SerializeField] protected float PanelAnimationDuration = 0.5f;

    public void PanelIntro()
    {
        gameObject.SetActive(true);
        
        PanelRect.anchoredPosition = new Vector2(PanelRect.anchoredPosition.x, PanelBottomPosY);
        
        PanelRect.DOAnchorPosY(PanelTopPosY, PanelAnimationDuration).SetUpdate(true);
    }

    public void PanelOutro()
    {
        Sequence sequence = DOTween.Sequence().SetUpdate(true);

        sequence.Append(PanelRect.DOAnchorPosY(PanelBottomPosY, PanelAnimationDuration).SetUpdate(true));
        
        sequence.OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
}
