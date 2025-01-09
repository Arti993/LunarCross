using System;
using Ami.BroAudio;
using Infrastructure;
using Infrastructure.Services.AudioPlayback;
using Infrastructure.Services.ScreenFader;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class CustomButton : MonoBehaviour, IPointerClickHandler
    {
        protected bool IsClickable;
        private IAudioPlayback _audioPlayback;
        private IScreenFader _screenFader;

        public event Action Clicked;

        [Inject]
        private void Construct(IAudioPlayback audioPlayback,
            IScreenFader screenFader)
        {
            _audioPlayback = audioPlayback;
            _screenFader = screenFader;
        }

        protected virtual void OnEnable()
        {
            if (_screenFader.IsActive())
                IsClickable = false;
            else
                IsClickable = true;

            _screenFader.FadingComplete += OnScreenFaderDisable;
            _screenFader.FadingStart += OnScreenFaderEnable;
        }

        protected virtual void OnDisable()
        {
            _screenFader.FadingComplete -= OnScreenFaderDisable;
            _screenFader.FadingStart -= OnScreenFaderEnable;
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (IsClickable == false)
                return;

            SoundID buttonClick = _audioPlayback.SoundsContainer.ButtonClick;

            _audioPlayback.PlaySound(buttonClick);

            Clicked?.Invoke();
        }

        protected void OnScreenFaderEnable()
        {
            IsClickable = false;
        }

        protected void OnScreenFaderDisable()
        {
            IsClickable = true;
        }
    }
}