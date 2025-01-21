using System;
using Ami.BroAudio;
using Infrastructure.Services.AudioPlayback;
using Infrastructure.Services.ScreenFader;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace UI
{
    public class CustomButton : MonoBehaviour, IPointerClickHandler
    {
        protected bool IsClickable;
        protected IScreenFader ScreenFader;
        private IAudioPlayback _audioPlayback;

        public event Action Clicked;
        
        protected virtual void Construct()
        {
            _audioPlayback = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IAudioPlayback>();
            ScreenFader = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IScreenFader>();
        }

        protected virtual void Awake()
        {
            Construct();
        }

        protected virtual void OnEnable()
        {
            if (ScreenFader.IsActive())
                IsClickable = false;
            else
                IsClickable = true;

            ScreenFader.FadingCompleted += OnScreenFaderDisable;
            ScreenFader.FadingStarted += OnScreenFaderEnable;
        }

        protected virtual void OnDisable()
        {
            ScreenFader.FadingCompleted -= OnScreenFaderDisable;
            ScreenFader.FadingStarted -= OnScreenFaderEnable;
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