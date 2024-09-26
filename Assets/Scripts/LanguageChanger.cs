using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

[RequireComponent(typeof(LeanLocalization))]
public class LanguageChanger : MonoBehaviour
{
    private const string NotFirstGameLaunch = "NotFirstGameLaunch";
    private const string EnglishCode = "English";
    private const string TurkishCode = "Turkish";
    private const string RussianCode = "Russian";
    private const string English = "en";
    private const string Turkish = "tr";
    private const string Russian = "ru";

    private LeanLocalization _leanLocalization;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _leanLocalization = GetComponent<LeanLocalization>();

        if (PlayerPrefs.HasKey(NotFirstGameLaunch) == false)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            ChangeLanguageByLocation();
#endif
        }
    }

    public void SetLanguage(string language)
    {
        _leanLocalization.SetCurrentLanguage(language);
    }

    public string GetCurrentLanguage()
    {
        return _leanLocalization.CurrentLanguage;
    }

    private void ChangeLanguageByLocation()
    {
        string languageCode = YandexGamesSdk.Environment.i18n.lang;

        switch (languageCode)
        {
            case English:
                _leanLocalization.SetCurrentLanguage(EnglishCode);
                break;
            case Turkish:
                _leanLocalization.SetCurrentLanguage(TurkishCode);
                break;
            case Russian:
                _leanLocalization.SetCurrentLanguage(RussianCode);
                break;
        }
    }
}