using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MenuButton : CustomButton, IPointerEnterHandler, IPointerExitHandler
{
    private Transform _transform;
    private Vector3 _startScale;
    private Vector3 _increasedScale;
    private float _increaseFactor = 1.3f;
    private float _animationDuration = 0.3f;

    private void Awake()
    {
        _transform = transform;
        _startScale = _transform.localScale;
        _increasedScale = _startScale * _increaseFactor;
    }

    protected override void OnEnable()
    {
        _transform.localScale = _startScale;
        
        base.OnEnable();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(IsClickable == false)
            return;

        _transform.DOScale(_increasedScale, _animationDuration).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(IsClickable == false)
            return;
        
        _transform.DOScale(_startScale, _animationDuration).SetUpdate(true);
    }
    
    
}
