using System;

public interface ILocalization : IService
{
    public event Action<string> LanguageChanged;
    public void SetLanguage(string language);
    public string GetCurrentLanguage();
}
