using System;
using System.Collections.Generic;
using System.Linq;
using Ami.BroAudio;
using UnityEngine;

public class AudioPlayback : IAudioPlayback
{
    private const string SoundsContainerPath = "Prefabs/AudioConfigs/Sounds";
    private const string MusicContainerPath = "Prefabs/AudioConfigs/Music";
    private const string MusicVolumeTag = "LastMusicVolume";
    private const string SoundVolumeTag = "LastSoundsVolume";
    private const string MusicMutedTag = "MusicMuted";
    private const string SoundsMutedTag = "SoundsMuted";
    private const int MaxConvertedVolume = 100;

    private bool _isMenuThemePlaying;
    private List<SoundID> _music;
    private List<SoundID> _sounds;

    public AudioPlayback()
    {
        SoundsContainer = Resources.Load<SoundsContainer>(SoundsContainerPath);
        MusicContainer = Resources.Load<MusicContainer>(MusicContainerPath);

        ApplySavedVolume();
    }

    public MusicContainer MusicContainer { get; }
    public SoundsContainer SoundsContainer { get; }

    public void MuteAudio()
    {
        AudioListener.volume = 0f;
    }

    public void UnMuteAudio()
    {
        AudioListener.volume = 1f;
    }

    public void MuteMusic()
    {
        MusicContainer.Mute();

        PlayerPrefs.SetInt(MusicMutedTag, 1);
        PlayerPrefs.Save();
    }

    public void UnMuteMusic()
    {
        ChangeMusicVolume(GetLastSavedVolume(MusicVolumeTag));

        PlayerPrefs.DeleteKey(MusicMutedTag);
        PlayerPrefs.Save();
    }

    public void MuteSounds()
    {
        PlayerPrefs.SetInt(SoundsMutedTag, 1);
        PlayerPrefs.Save();
    }

    public void UnMuteSounds()
    {
        PlayerPrefs.DeleteKey(SoundsMutedTag);
        PlayerPrefs.Save();
    }

    public void ChangeMusicVolume(float volume)
    {
        MusicContainer.SetVolume(volume);
    }

    public void PlayMusic(SoundID soundID)
    {
        if (soundID == MusicContainer.MenuTheme)
        {
            if (_isMenuThemePlaying)
                return;

            _isMenuThemePlaying = true;
        }
        
        if (MusicContainer.GetIdList().Contains(soundID))
            MusicContainer.Play(soundID);
        else
            throw new InvalidOperationException();
    }

    public void PlaySound(SoundID soundID)
    {
        if (SoundsContainer.GetIdList().Contains(soundID))
            SoundsContainer.Play(soundID);
        else
            throw new InvalidOperationException();
    }

    public void StopSound(SoundID soundID)
    {
        if (SoundsContainer.GetIdList().Contains(soundID))
            SoundsContainer.Play(soundID);
        else
            throw new InvalidOperationException();
    }

    public void PlayLevelTheme()
    {
        _isMenuThemePlaying = false;

        int levelNumber = DIServicesContainer.Instance.GetService<IGameProgress>().GetCurrentLevelNumber();

        SoundID levelTheme = DIServicesContainer.Instance.GetService<ILevelsSettingsNomenclature>()
            .GetLevelSettings(levelNumber).MusicTheme;

        MusicContainer.Play(levelTheme);
    }

    public void PlayExplosionSound()
    {
        MusicContainer.Mute();

        SoundsContainer.Play(SoundsContainer.Explosion);
    }

    public void SaveVolume(float volume, string VolumeTag)
    {
        var convertedVolume = (int) Mathf.Round(volume * 100);

        PlayerPrefs.SetInt(VolumeTag, convertedVolume);

        PlayerPrefs.Save();
    }

    public float GetLastSavedVolume(string VolumeTag)
    {
        int convertedVolume = PlayerPrefs.GetInt(VolumeTag, MaxConvertedVolume);

        float unConvertedVolume = (float) convertedVolume / 100;

        return unConvertedVolume;
    }

    private void ApplySavedVolume()
    {
        if (PlayerPrefs.HasKey(MusicMutedTag))
            MuteMusic();
        else
            ChangeMusicVolume(GetLastSavedVolume(MusicVolumeTag));

        if (PlayerPrefs.HasKey(SoundsMutedTag))
            MuteSounds();
        else
            ChangeMusicVolume(GetLastSavedVolume(SoundVolumeTag));
    }
}