using UnityEngine;

public class LanguageSetButton : MonoBehaviour
{
    [SerializeField] private string _language;

    public void ChooseLanguage()
    {
        DIServicesContainer.Instance.GetService<ILocalization>().SetLanguage(_language);
    }
}
