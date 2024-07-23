using UnityEngine;

public class LanguageSetButton : MonoBehaviour
{
    [SerializeField] private string _language;

    private LanguageChangeWindow _languageChangeWindow;

    private void Awake()
    {
        _languageChangeWindow = GetComponentInParent<LanguageChangeWindow>();
    }

    public void ChooseLanguage()
    {
        AllServicesContainer.Instance.GetService<ILocalization>().SetLanguage(_language);
        
        _languageChangeWindow.Exit();
    }
}
