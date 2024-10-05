using DG.Tweening;
using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    [SerializeField] private float _animateTime = 1;
    [SerializeField] private float MaxScale = 1.5f;

    private void Start()
    {
        transform.DOScale(MaxScale, _animateTime).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
    }
}
