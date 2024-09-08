using UnityEngine;

public class Localization : ILocalization
{
    private readonly IAssetsProvider _provider;
    private LanguageChanger _languageChanger;

    public Localization(IAssetsProvider provider)
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
