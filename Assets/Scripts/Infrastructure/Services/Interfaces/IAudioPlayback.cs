using System.Collections;
using System.Collections.Generic;
using Ami.BroAudio;
using UnityEngine;

public interface IAudioPlayback : IService
{
    MusicContainer MusicContainer { get; }
    SoundsContainer SoundsContainer { get; }
    
    public void MuteAudio();
    public void UnMuteAudio();
    public void MuteMusic();
    public void UnMuteMusic();
    public void MuteSounds();
    public void UnMuteSounds();
    public void ChangeMusicVolume(float volume);
    public void PlayMusic(SoundID soundID);
    public void PlaySound(SoundID soundID);
    public void StopSound(SoundID soundID);
    public void PlayLevelTheme();
    public void PlayExplosionSound();
    public void SaveVolume(float volume, string volumeTag);
    public float GetLastSavedVolume(string volumeTag);
}
