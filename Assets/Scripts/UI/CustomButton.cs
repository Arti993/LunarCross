using System;
using Ami.BroAudio;
using Infrastructure;
using Infrastructure.Services.AudioPlayback;
using Infrastructure.Services.ScreenFader;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class CustomButton : MonoBehaviour, IPointerClickHandler
    {
        protected bool IsClickable;

        public event Action Clicked;

        protected virtual void OnEnable()
        {
            if (DIServicesContainer.Instance.GetService<IScreenFader>().IsActive())
                IsClickable = false;
            else
                IsClickable = true;

            DIServicesContainer.Instance.GetService<IScreenFader>().FadingComplete += OnScreenFaderDisable;
            DIServicesContainer.Instance.GetService<IScreenFader>().FadingStart += OnScreenFaderEnable;
        }

        protected virtual void OnDisable()
        {
            DIServicesContainer.Instance.GetService<IScreenFader>().FadingComplete -= OnScreenFaderDisable;
            DIServicesContainer.Instance.GetService<IScreenFader>().FadingStart -= OnScreenFaderEnable;
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (IsClickable == false)
                return;

            SoundID buttonClick = DIServicesContainer.Instance.GetService<IAudioPlayback>().SoundsContainer.ButtonClick;

            DIServicesContainer.Instance.GetService<IAudioPlayback>().PlaySound(buttonClick);

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