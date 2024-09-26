using UnityEngine;
using UnityEngine.UI;

public class GameTitle : MonoBehaviour
{
    private const string English = "English";
    private const string Turkish = "Turkish";
    private const string Russian = "Russian";
    
    [SerializeField] private Image _titleImage;
    [SerializeField] private Sprite _titleENG;
    [SerializeField] private Sprite _titleRUS;
    [SerializeField] private Sprite _titleTUR;

    private string _currentLanguage;

    private void OnEnable()
    {
        _currentLanguage = DIServicesContainer.Instance.GetService<ILocalization>().GetCurrentLanguage();
        
        ShowTitle(_currentLanguage);
        
        DIServicesContainer.Instance.GetService<ILocalization>().LanguageChanged += OnLanguageChanged;
    }

    private void OnDisable()
    {
        DIServicesContainer.Instance.GetService<ILocalization>().LanguageChanged -= OnLanguageChanged;
    }

    private void OnLanguageChanged(string language)
    {
        ShowTitle(language);
    }

    private void ShowTitle(string language)
    {
        if(language == English)
            _titleImage.sprite = _titleENG;
        
        if(language == Russian)
            _titleImage.sprite = _titleRUS;
        
        if(language == Turkish)
            _titleImage.sprite = _titleTUR;
    }
}
