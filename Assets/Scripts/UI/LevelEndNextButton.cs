using Infrastructure.Services.ScreenFader;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class LevelEndNextButton : CustomButton
    {
        private Button _button;
        
        private IScreenFader _screenFader;
        
        [Inject]
        private void Construct(IScreenFader screenFader)
        {
            _screenFader = screenFader;
        }

        protected override void OnEnable()
        {
            _button = gameObject.GetComponent<Button>();
            _screenFader.FadingComplete += OnScreenFaderDisable;
            _screenFader.FadingStart += OnScreenFaderEnable;
        }

        public void SetInterractable()
        {
            IsClickable = true;
            _button.interactable = true;
        }

        public void SetNotInterractable()
        {
            IsClickable = false;
            _button.interactable = false;
        }
    }
}
