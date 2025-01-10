using Infrastructure.Services.Localization;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        private void Construct()
        {
            _localization = SceneManager.GetActiveScene().GetSceneContainer().Resolve<ILocalization>();
        }
        
        private void Awake()
        {
            Construct();
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
