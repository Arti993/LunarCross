using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelNumberTitle : MonoBehaviour
{
    private const float AnimateTime = 1.2f;
    private const float MaxScale = 2;
    private const int TutorialSceneIndex = 4;
    [SerializeField] private TMP_Text _levelNumber;
    [SerializeField] private TMP_Text _level;
    [SerializeField] private TMP_Text _tutorial;

    private void Start()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex == TutorialSceneIndex)
        {
            _level.gameObject.SetActive(false);
            _tutorial.gameObject.SetActive(true);
            _levelNumber.text = "";
            
            Animate(_tutorial);
        }
        else
        {
            _level.gameObject.SetActive(true);
            _tutorial.gameObject.SetActive(false);
            
            int levelNumber = DIServicesContainer.Instance.GetService<IGameProgress>().GetCurrentLevelNumber();

            _levelNumber.text = $"{levelNumber}";
            
            Animate(_level,_levelNumber);
        }
    }

    private void Animate(TMP_Text text)
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(text.transform.DOScale(MaxScale, AnimateTime));
        sequence.Append(text.DOFade(0f, AnimateTime));
        
        sequence.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
    
    private void Animate(TMP_Text text, TMP_Text number)
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(text.transform.DOScale(MaxScale, AnimateTime));
        sequence.Append(text.DOFade(0f, AnimateTime));
        sequence.Join(number.DOFade(0f, AnimateTime));
        
        sequence.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
