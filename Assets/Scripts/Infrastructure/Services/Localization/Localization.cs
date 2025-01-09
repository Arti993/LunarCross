using System;
using Data;
using Infrastructure.Services.AssetsProvider;
using UnityEngine;

namespace Infrastructure.Services.Localization
{
    public class Localization : ILocalization
    {
        private readonly IAssetsProvider _provider;
        private LanguageChanger _languageChanger;

        public Localization()
        {
            Debug.Log("локализация создана через пустой конструктор");
        }
        
        public Localization(IAssetsProvider provider)
        {
            _provider = provider;
            
            Debug.Log("локализация создана");

            _languageChanger = CreateLanguageChanger();
        }

        public event Action<string> LanguageChanged;

        public string GetCurrentLanguage()
        {
            return _languageChanger.GetCurrentLanguage();
        }

        public void SetLanguage(string language)
        {
            _languageChanger.SetLanguage(language);
            LanguageChanged?.Invoke(language);
        }

        private LanguageChanger CreateLanguageChanger()
        {
            GameObject leanLocalizationObject = _provider.Instantiate(PrefabsPaths.LeanLocalization);

            return leanLocalizationObject.GetComponent<LanguageChanger>();
        }
    }
}
