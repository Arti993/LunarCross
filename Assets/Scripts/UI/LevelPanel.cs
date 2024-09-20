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
            _blockedView.SetActive(false);

            if (DIServicesContainer.Instance.GetService<IScreenFader>().IsActive() == false)
                _button.interactable = true;
            
            _levelNumberTitle.text = _levelNumber.ToString();
            
            DisplayProgress();
        }
    }

    public void LoadLevel()
    {
        DIServicesContainer.Instance.GetService<IGameProgress>().SelectLevel(_levelNumber);
        
        DIServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene((int)SceneIndex.Gameplay);
    }

    private void DisplayProgress()
    {
        int levelResult = DIServicesContainer.Instance.GetService<IGameProgress>().GetLevelResult(_levelNumber);
            
        _bestRecordTitle.text = levelResult.ToString();
            
        DisplayStars(levelResult);
    }
    
    private void DisplayStars(int levelResult)
    {
        Level levelSettings = DIServicesContainer.Instance.GetService<ILevelsSettingsNomenclature>()
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
