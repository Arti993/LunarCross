using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAudioPlayback : IService
{
    public void MuteAudio();
    public void UnMuteAudio();
    public void MuteMusic();
    public void UnMuteMusic();
    public void MuteSounds();
    public void UnMuteSounds();
    public void ChangeMusicVolume(float volume);
    public void ChangeSoundsVolume(float volume);
    public void PlayLevelTheme();
    public void PlayMenuTheme();
    public void PlayAlienGrabsAstronautSound();
    public void PlayPickUpAstronautSound();
    public void PlayAstronautGetInSound();
    public void PlayButtonPressSound();
    public void PlayStarCollectingSound();
    public void PlayExplosionSound();
    public void PlayKnockSound();
    public void PlayGravityRaySound();
    public void PlayTornadoSound();
    public void PlayAstronautInRaySound();
    public void SaveVolume(float volume, string volumeTag);
    public float GetLastSavedVolume(string volumeTag);
}
