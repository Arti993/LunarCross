using System;

namespace Infrastructure.Services.Localization
{
    public interface ILocalization : IService
    {
        public event Action<string> LanguageChanged;
        public void SetLanguage(string language);
        public string GetCurrentLanguage();
    }
}
