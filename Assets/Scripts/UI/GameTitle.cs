using Infrastructure.Services.Localization;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
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
        private ILocalization _localization;

        [Inject]
        private void Construct(ILocalization localization)
        {
            _localization = localization;
            Debug.Log("отработал");
        }

        private void OnEnable()
        {
            _currentLanguage = _localization.GetCurrentLanguage();

            ShowTitle(_currentLanguage);

            _localization.LanguageChanged += OnLanguageChanged;
        }

        private void OnDisable()
        {
            _localization.LanguageChanged -= OnLanguageChanged;
        }

        private void OnLanguageChanged(string language)
        {
            ShowTitle(language);
        }

        private void ShowTitle(string language)
        {
            if (language == English)
                _titleImage.sprite = _titleENG;

            if (language == Russian)
                _titleImage.sprite = _titleRUS;

            if (language == Turkish)
                _titleImage.sprite = _titleTUR;
        }
    }
}
