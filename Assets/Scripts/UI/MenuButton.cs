using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace UI
{
    public class MenuButton : CustomButton, IPointerEnterHandler, IPointerExitHandler
    {
        private const float IncreaseFactor = 1.3f;
        private const float AnimationDuration = 0.3f;
        private Transform _transform;
        private Vector3 _startScale;
        private Vector3 _increasedScale;

        protected override void Awake()
        {
            base.Awake();
            
            _transform = transform;
            _startScale = _transform.localScale;
            _increasedScale = _startScale * IncreaseFactor;
        }

        protected override void OnEnable()
        {
            _transform.localScale = _startScale;

            base.OnEnable();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (IsClickable == false)
                return;

            _ = _transform.DOScale(_increasedScale, AnimationDuration).SetUpdate(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (IsClickable == false)
                return;

            _ = _transform.DOScale(_startScale, AnimationDuration).SetUpdate(true);
        }
    }
}
