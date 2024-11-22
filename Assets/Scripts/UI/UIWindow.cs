using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class UIWindow : MonoBehaviour
    {
        [SerializeField] protected RectTransform PanelRect;
        [SerializeField] protected float PanelTopPosY;
        [SerializeField] protected float PanelBottomPosY = -1500f;
        [SerializeField] protected float PanelAnimationDuration = 0.5f;

        public virtual void PanelIntro()
        {
            gameObject.SetActive(true);

            PanelRect.anchoredPosition = new Vector2(PanelRect.anchoredPosition.x, PanelBottomPosY);

            PanelRect.DOAnchorPosY(PanelTopPosY, PanelAnimationDuration).SetUpdate(true);
        }

        public virtual void PanelOutro()
        {
            PanelRect.DOAnchorPosY(PanelBottomPosY, PanelAnimationDuration).SetUpdate(true).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }
}
