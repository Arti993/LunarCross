using DG.Tweening;
using Infrastructure.UIStateMachine;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UiWindow : MonoBehaviour
    {
        [SerializeField] protected RectTransform PanelRect;
        [SerializeField] protected float PanelTopPosY;
        [SerializeField] protected float PanelBottomPosY = -1500f;
        [SerializeField] protected float PanelAnimationDuration = 0.5f;

        protected IUiStateMachine UiStateMachine;
        
        protected virtual void Construct()
        {
            UiStateMachine = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IUiStateMachine>();
        }

        protected virtual void Awake()
        {
            Construct();
        }
        
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
