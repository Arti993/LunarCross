using Infrastructure;
using Infrastructure.Services.Localization;
using UnityEngine;

namespace UI
{
    public class LanguageSetButton : CustomButton
    {
        [SerializeField] private string _language;

        public void ChooseLanguage()
        {
            DIServicesContainer.Instance.GetService<ILocalization>().SetLanguage(_language);
        }
    }
}