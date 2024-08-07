using DG.Tweening;
using UnityEngine;

public class UIWindow : MonoBehaviour
{
    [SerializeField] protected RectTransform PanelRect;
    [SerializeField] protected float PanelTopPosY;
    [SerializeField] protected float PanelBottomPosY = -1000f;
    [SerializeField] protected float PanelAnimationDuration = 0.5f;

    protected void PanelIntro()
    {
        PanelRect.anchoredPosition = new Vector2(PanelRect.anchoredPosition.x, PanelBottomPosY);
        
        PanelRect.DOAnchorPosY(PanelTopPosY, PanelAnimationDuration).SetUpdate(true);
    }

    protected void PanelOutro()
    {
        Sequence sequence = DOTween.Sequence().SetUpdate(true);

        sequence.Append(PanelRect.DOAnchorPosY(PanelBottomPosY, PanelAnimationDuration).SetUpdate(true));
        
        sequence.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
