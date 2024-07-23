using UnityEngine;

public class Localization : ILocalization
{
    private readonly IAssets _provider;
    private LanguageChanger _languageChanger;

    public Localization(IAssets provider)
    {
        _provider = provider;

        _languageChanger = CreateLanguageChanger();
    }
    
    public void SetLanguage(string language)
    {
        _languageChanger.SetLanguage(language);
    }

    private LanguageChanger CreateLanguageChanger()
    {
           GameObject leanLocalizationObject = _provider.Instantiate("Prefabs/LeanLocalization");

           return leanLocalizationObject.GetComponent<LanguageChanger>();
    }
}
