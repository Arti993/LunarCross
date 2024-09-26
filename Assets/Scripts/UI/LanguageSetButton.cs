using UnityEngine;

public class LanguageSetButton : CustomButton
{
    [SerializeField] private string _language;

    public void ChooseLanguage()
    {
        DIServicesContainer.Instance.GetService<ILocalization>().SetLanguage(_language);
    }
}
