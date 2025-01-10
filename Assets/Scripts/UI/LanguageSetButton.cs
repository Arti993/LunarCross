using Infrastructure.Services.Localization;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LanguageSetButton : CustomButton
    {
        [SerializeField] private string _language;

        private ILocalization _localization;
        
        protected override void Construct()
        {
            base.Construct();
            
            _localization = SceneManager.GetActiveScene().GetSceneContainer().Resolve<ILocalization>();
        }
        
        public void ChooseLanguage()
        {
            _localization.SetLanguage(_language);
        }
    }
}