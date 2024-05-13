using UnityEngine;
using DG.Tweening;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class LevelNumberTitle : MonoBehaviour
{
    private const int AnimateTime = 2;
    private const int MaxScale = 2;

    private TMP_Text _title;

    private void Start()
    {
        _title = GetComponent<TMP_Text>();

        int levelNumber = AllServicesContainer.Instance.GetService<IGameProgress>().GetCurrentLevelNumber();

        _title.text = $"Level {levelNumber}";
        
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(MaxScale, AnimateTime));
        sequence.Append(_title.DOFade(0f, AnimateTime));
        
        sequence.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
