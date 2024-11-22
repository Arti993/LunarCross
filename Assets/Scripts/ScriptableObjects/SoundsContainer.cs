using System.Collections.Generic;
using Ami.BroAudio;
using Infrastructure;
using Infrastructure.Services.AudioPlayback;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New SoundsContainer", menuName = "SoundsContainers/Create New SoundContainer",
        order = 51)]
    public class SoundsContainer : ScriptableObject
    {
        private const string SoundsMutedTag = "SoundsMuted";
        private const string SoundsVolumeTag = "LastSoundsVolume";

        [SerializeField] private SoundID _alienGrab;
        [SerializeField] private SoundID _astronautPickUp;
        [SerializeField] private SoundID _explosion;
        [SerializeField] private SoundID _knock;
        [SerializeField] private SoundID _ray;
        [SerializeField] private SoundID _rocketEngine;
        [SerializeField] private SoundID _rocketTurbine;
        [SerializeField] private SoundID _savingAstronaut;
        [SerializeField] private SoundID _starCollect;
        [SerializeField] private SoundID _tornado;
        [SerializeField] private SoundID _buttonClick;

        public SoundID AlienGrab => _alienGrab;
        public SoundID AstronautPickUp => _astronautPickUp;
        public SoundID Explosion => _explosion;
        public SoundID Knock => _knock;
        public SoundID Ray => _ray;
        public SoundID RocketEngine => _rocketEngine;
        public SoundID RocketTurbine => _rocketTurbine;
        public SoundID SavingAstronaut => _savingAstronaut;
        public SoundID StarCollect => _starCollect;
        public SoundID Tornado => _tornado;
        public SoundID ButtonClick => _buttonClick;

        public IReadOnlyList<SoundID> GetIdList()
        {
            List<SoundID> sounds = new List<SoundID>
            {
                _alienGrab,
                _astronautPickUp,
                _explosion,
                _knock,
                _ray,
                _rocketEngine,
                _rocketTurbine,
                _savingAstronaut,
                _starCollect,
                _tornado,
                _buttonClick
            };

            return sounds;
        }

        public void Play(SoundID soundID)
        {
            float volume;

            if (PlayerPrefs.HasKey(SoundsMutedTag))
                volume = 0f;
            else
                volume = DIServicesContainer.Instance.GetService<IAudioPlayback>().GetLastSavedVolume(SoundsVolumeTag);

            BroAudio.Play(soundID);

            BroAudio.SetVolume(soundID, volume);
        }

        public void Stop(SoundID soundID)
        {
            BroAudio.Stop(soundID);
        }
    }
}