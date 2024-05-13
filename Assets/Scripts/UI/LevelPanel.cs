using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelPanel : MonoBehaviour
{
    [SerializeField] private GameObject _unblockedView;
    [SerializeField] private GameObject _blockedView;
    [SerializeField] private GameObject[] _ratingStars;
    [SerializeField] private TMP_Text _levelNumberTitle;
    [SerializeField] private TMP_Text _bestRecordTitle;
    [SerializeField] private int _levelNumber;
    
    private const int GameplaySceneIndex = 2;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        
        if (PlayerPrefs.GetInt("ReachedLevelNumber", 1) < _levelNumber)
        {
            _unblockedView.SetActive(false);
            _blockedView.SetActive(true);
            _button.interactable = false;
        }
        else
        {
            _unblockedView.SetActive(true);
            _button.interactable = true;
            _blockedView.SetActive(false);
            
            _levelNumberTitle.text = _levelNumber.ToString();
            
            DisplayProgress();
        }
    }

    public void LoadLevel()
    {
        AllServicesContainer.Instance.GetService<IGameProgress>().SelectLevel(_levelNumber);
        
        AllServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(GameplaySceneIndex);
    }

    private void DisplayProgress()
    {
        int levelResult = AllServicesContainer.Instance.GetService<IGameProgress>().GetLevelResult(_levelNumber);
            
        _bestRecordTitle.text = levelResult.ToString();
            
        DisplayStars(levelResult);
    }
    
    private void DisplayStars(int levelResult)
    {
        Level levelSettings = AllServicesContainer.Instance.GetService<ILevelsSettingsNomenclature>()
            .GetLevelSettings(_levelNumber);

            
        foreach (var star in _ratingStars)
        {
            star.SetActive(false);
        }
            
        int starsToDisplay = _ratingStars.Length;

        if (levelResult < levelSettings.PointsForThirdStar)
            starsToDisplay--;
            
        if (levelResult < levelSettings.PointsForSecondStar)
            starsToDisplay--;
            
        if (levelResult < levelSettings.PointsForFirstStar)
            starsToDisplay--;

        for (int i = 0; i < starsToDisplay; i++)
        {
            _ratingStars[i].SetActive(true);
        }
    }
}
