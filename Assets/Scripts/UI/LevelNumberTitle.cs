using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TMP_Text))]
public class LevelNumberTitle : MonoBehaviour
{
    private const float AnimateTime = 1.3f;
    private const float MaxScale = 2;
    private const int TutorialSceneIndex = 4;

    private TMP_Text _title;

    private void Start()
    {
        _title = GetComponent<TMP_Text>();

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex == TutorialSceneIndex)
        {
            _title.text = "Tutorial";
        }
        else
        {
            int levelNumber = AllServicesContainer.Instance.GetService<IGameProgress>().GetCurrentLevelNumber();

            _title.text = $"Level {levelNumber}";
        }
            
        
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(MaxScale, AnimateTime));
        sequence.Append(_title.DOFade(0f, AnimateTime));
        
        sequence.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
