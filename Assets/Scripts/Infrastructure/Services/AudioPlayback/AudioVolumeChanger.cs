using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Services.AudioPlayback
{
    public class AudioVolumeChanger : MonoBehaviour
    {
        private const string MusicVolumeTag = "LastMusicVolume";
        private const string SoundsVolumeTag = "LastSoundsVolume";
        private const string MusicMutedTag = "MusicMuted";
        private const string SoundsMutedTag = "SoundsMuted";
        private const int MaxVolume = 1;

        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _turnedOffMusicSlider;
        [SerializeField] private Slider _soundsVolumeSlider;
        [SerializeField] private Slider _turnedOffSoundsSlider;
        [SerializeField] private Toggle _musicVolumeToggle;
        [SerializeField] private Toggle _soundsVolumeToggle;

        private IAudioPlayback _audioPlayback;
        
        [Inject]
        private void Construct(IAudioPlayback audioPlayback)
        {
            _audioPlayback = audioPlayback;
        }
        
        private void Awake()
        {
            if (PlayerPrefs.HasKey(MusicVolumeTag))
            {
                float lastSavedMusicVolume = _audioPlayback.GetLastSavedVolume(MusicVolumeTag);

                _musicVolumeSlider.value = lastSavedMusicVolume;
            }

            if (PlayerPrefs.HasKey(SoundsVolumeTag))
            {
                float lastSavedSoundsVolume = _audioPlayback.GetLastSavedVolume(SoundsVolumeTag);

                _soundsVolumeSlider.value = lastSavedSoundsVolume;
            }

            if (PlayerPrefs.HasKey(MusicMutedTag))
                _musicVolumeToggle.isOn = false;

            if (PlayerPrefs.HasKey(SoundsMutedTag))
                _soundsVolumeToggle.isOn = false;
        }

        public void ChangeMusicVolume()
        {
            _audioPlayback.SaveVolume(_musicVolumeSlider.value, MusicVolumeTag);

            _audioPlayback.ChangeMusicVolume(_musicVolumeSlider.value);
        }

        public void ChangeSoundsVolume()
        {
            _audioPlayback.SaveVolume(_soundsVolumeSlider.value, SoundsVolumeTag);
        }

        public void MuteOrUnmuteMusic()
        {
            if (_musicVolumeToggle.isOn)
            {
                _turnedOffMusicSlider.gameObject.SetActive(false);
                _musicVolumeSlider.gameObject.SetActive(true);

                _musicVolumeSlider.value = MaxVolume;

                _audioPlayback.UnMuteMusic();
            }
            else
            {
                _turnedOffMusicSlider.gameObject.SetActive(true);
                _musicVolumeSlider.gameObject.SetActive(false);

                _audioPlayback.MuteMusic();
            }
        }

        public void MuteOrUnmuteSounds()
        {
            if (_soundsVolumeToggle.isOn)
            {
                _turnedOffSoundsSlider.gameObject.SetActive(false);
                _soundsVolumeSlider.gameObject.SetActive(true);

                _soundsVolumeSlider.value = MaxVolume;

                _audioPlayback.UnMuteSounds();
            }
            else
            {
                _turnedOffSoundsSlider.gameObject.SetActive(true);
                _soundsVolumeSlider.gameObject.SetActive(false);

                PlayerPrefs.DeleteKey(SoundsMutedTag);

                _audioPlayback.MuteSounds();
            }
        }
    }
}
