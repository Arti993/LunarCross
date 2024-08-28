using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        if (PlayerPrefs.HasKey(MusicVolumeTag))
        {
            float lastSavedMusicVolume = DIServicesContainer.Instance.GetService<IAudioPlayback>().GetLastSavedVolume(MusicVolumeTag);

            _musicVolumeSlider.value = lastSavedMusicVolume;
        }

        if (PlayerPrefs.HasKey(SoundsVolumeTag))
        {
            float lastSavedSoundsVolume = DIServicesContainer.Instance.GetService<IAudioPlayback>().GetLastSavedVolume(SoundsVolumeTag);

            _soundsVolumeSlider.value = lastSavedSoundsVolume;
        }
        
        if (PlayerPrefs.HasKey(MusicMutedTag))
            _musicVolumeToggle.isOn = false;
        
        if (PlayerPrefs.HasKey(SoundsMutedTag))
            _soundsVolumeToggle.isOn = false;
    }

    public void ChangeMusicVolume()
    {
        DIServicesContainer.Instance.GetService<IAudioPlayback>().SaveVolume(_musicVolumeSlider.value, MusicVolumeTag);

        DIServicesContainer.Instance.GetService<IAudioPlayback>().ChangeMusicVolume(_musicVolumeSlider.value);
    }
    
    public void ChangeSoundsVolume()
    {
        DIServicesContainer.Instance.GetService<IAudioPlayback>().SaveVolume(_soundsVolumeSlider.value, SoundsVolumeTag);

        DIServicesContainer.Instance.GetService<IAudioPlayback>().ChangeSoundsVolume(_soundsVolumeSlider.value);
    }
    
    public void MuteOrUnmuteMusic()
    {
        if (_musicVolumeToggle.isOn)
        {
            _turnedOffMusicSlider.gameObject.SetActive(false);
            _musicVolumeSlider.gameObject.SetActive(true);

            _musicVolumeSlider.value = MaxVolume;
            
            DIServicesContainer.Instance.GetService<IAudioPlayback>().UnMuteMusic();
        }
        else
        {
            _turnedOffMusicSlider.gameObject.SetActive(true);
            _musicVolumeSlider.gameObject.SetActive(false);
            
            DIServicesContainer.Instance.GetService<IAudioPlayback>().MuteMusic();
        }
    }
    
    public void MuteOrUnmuteSounds()
    {
        if (_soundsVolumeToggle.isOn)
        {
            _turnedOffSoundsSlider.gameObject.SetActive(false);
            _soundsVolumeSlider.gameObject.SetActive(true);

            _soundsVolumeSlider.value = MaxVolume;
            
            DIServicesContainer.Instance.GetService<IAudioPlayback>().UnMuteSounds();
        }
        else
        {
            _turnedOffSoundsSlider.gameObject.SetActive(true);
            _soundsVolumeSlider.gameObject.SetActive(false);
            
            PlayerPrefs.DeleteKey(MusicVolumeTag);
            
            DIServicesContainer.Instance.GetService<IAudioPlayback>().MuteSounds();
        }
    }
}
