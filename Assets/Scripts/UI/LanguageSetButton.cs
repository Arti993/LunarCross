using Infrastructure.Services.Localization;
using Reflex.Attributes;
using UnityEngine;

namespace UI
{
    public class LanguageSetButton : CustomButton
    {
        [SerializeField] private string _language;

        private ILocalization _localization;

        [Inject]
        private void Construct(ILocalization localization)
        {
            _localization = localization;
        }
        
        public void ChooseLanguage()
        {
            _localization.SetLanguage(_language);
        }
    }
}