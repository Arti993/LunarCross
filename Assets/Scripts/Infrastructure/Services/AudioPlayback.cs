using System;
using UnityEngine;

public class AudioPlayback : IAudioPlayback
{
    private const string MusicVolumeTag = "LastMusicVolume";
    private const string SoundsVolumeTag = "LastSoundsVolume";
    private const string MusicMutedTag = "MusicMuted";
    private const string SoundsMutedTag = "SoundsMuted";
    private const string MenuTheme = "MenuTheme";
    private const string AlienGrabsAstronaut = "AlienGrabsAstronaut";
    private const string PickUpAstronaut = "PickUpAstronaut";
    private const string AstronautGetIn = "AstronautGetIn";
    private const string ButtonPress = "ButtonPress";
    private const string StarCollecting = "StarCollecting";
    private const string Explosion = "Explosion";
    private const string Knock = "Knock";
    private const string GravityRay = "GravityRay";
    private const string Tornado = "Tornado";
    private const string AstronautInRay = "AstronautInRay";
    private const int MaxConvertedVolume = 100;

    private bool _isMenuThemePlaying;
    private readonly SoundsCollection _soundsCollection;

    public AudioPlayback(IAssets provider)
    {
        GameObject soundsCollectionObject = provider.Instantiate("Prefabs/SoundsCollection");

        if (soundsCollectionObject.TryGetComponent(out SoundsCollection soundsCollection) == false)
            throw new InvalidOperationException();

        _soundsCollection = soundsCollection;

        ApplySavedVolume();
    }

    public void MuteAudio()
    {
        _soundsCollection.MuteMusic();
        _soundsCollection.MuteSounds();
    }

    public void UnMuteAudio()
    {
        ApplySavedVolume();
    }

    public void MuteMusic()
    {
        _soundsCollection.MuteMusic();

        PlayerPrefs.SetInt(MusicMutedTag, 0);
        PlayerPrefs.Save();
    }

    public void UnMuteMusic()
    {
        _soundsCollection.UnMuteMusic();

        PlayerPrefs.DeleteKey(MusicMutedTag);
        PlayerPrefs.Save();
    }

    public void MuteSounds()
    {
        _soundsCollection.MuteSounds();

        PlayerPrefs.SetInt(SoundsMutedTag, 0);
        PlayerPrefs.Save();
    }

    public void UnMuteSounds()
    {
        _soundsCollection.UnMuteSounds();

        PlayerPrefs.DeleteKey(SoundsMutedTag);
        PlayerPrefs.Save();
    }

    public void ChangeMusicVolume(float volume)
    {
        _soundsCollection.ChangeMusicVolume(volume);
    }

    public void ChangeSoundsVolume(float volume)
    {
        _soundsCollection.ChangeSoundsVolume(volume);
    }

    public void PlayLevelTheme()
    {
        _isMenuThemePlaying = false;

        int levelNumber = DIServicesContainer.Instance.GetService<IGameProgress>().GetCurrentLevelNumber();

        _soundsCollection.PlayMusic($"Level{levelNumber}Theme");
    }

    public void PlayMenuTheme()
    {
        if (_isMenuThemePlaying)
            return;

        _soundsCollection.PlayMusic(MenuTheme);

        _isMenuThemePlaying = true;
    }

    public void PlayAlienGrabsAstronautSound()
    {
        _soundsCollection.PlaySound(AlienGrabsAstronaut);
    }

    public void PlayPickUpAstronautSound()
    {
        _soundsCollection.PlaySound(PickUpAstronaut);
    }

    public void PlayAstronautGetInSound()
    {
        _soundsCollection.PlaySound(AstronautGetIn);
    }

    public void PlayButtonPressSound()
    {
        _soundsCollection.PlaySound(ButtonPress);
    }

    public void PlayStarCollectingSound()
    {
        _soundsCollection.PlaySound(StarCollecting);
    }

    public void PlayExplosionSound()
    {
        _soundsCollection.StopAudio();

        _soundsCollection.PlaySound(Explosion);
    }

    public void PlayKnockSound()
    {
        _soundsCollection.PlaySound(Knock);
    }

    public void PlayGravityRaySound()
    {
        _soundsCollection.PlaySound(GravityRay);
    }

    public void PlayTornadoSound()
    {
        _soundsCollection.PlaySound(Tornado);
    }

    public void PlayAstronautInRaySound()
    {
        _soundsCollection.PlaySound(AstronautInRay);
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
            ChangeSoundsVolume(GetLastSavedVolume(SoundsVolumeTag));
    }
}