using System;
using System.Linq;
using UnityEngine;

public class SoundsCollection : MonoBehaviour
{
    [SerializeField] private Sound[] _sounds;

    private Sound _currentPlayingMusic;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        PrepareAudioSources();
    }

    public void PlaySound(string soundName)
    {
        Sound soundToPlay = FindSoundToPlay(soundName);
        
        soundToPlay.Play();
    }

    public void PlayMusic(string musicName)
    {
        Sound musicToPlay = FindSoundToPlay(musicName);

        _currentPlayingMusic?.StopPlay();

        _currentPlayingMusic = musicToPlay;
        
        _currentPlayingMusic.Play();
    }

    public void StopAudio()
    {
        foreach (Sound sound in _sounds)
            sound.StopPlay();
    }
    
    public void MuteSounds()
    {
        foreach (Sound sound in _sounds.Where( s => !s.Loop))
            sound.Mute();
    }
    
    public void UnMuteSounds()
    {
        foreach (Sound sound in _sounds.Where( s => !s.Loop))
            sound.UnMute();
    }
    
    public void MuteMusic()
    {
        foreach (Sound sound in _sounds.Where( s => s.Loop))
            sound.Mute();
    }
    
    public void UnMuteMusic()
    {
        foreach (Sound sound in _sounds.Where( s => s.Loop))
            sound.UnMute();
    }

    public void MuteAudio()
    {
        foreach (Sound sound in _sounds)
            sound.Mute();
    }
    
    public void UnMuteAudio()
    {
        foreach (Sound sound in _sounds)
            sound.UnMute();
    }

    public void ChangeMusicVolume(float volume)
    {
        foreach (Sound sound in _sounds.Where( s => s.Loop))
            sound.ChangeVolume(volume);
    }
    
    public void ChangeSoundsVolume(float volume)
    {
        foreach (Sound sound in _sounds.Where( s => !s.Loop))
            sound.ChangeVolume(volume);
    }

    private void PrepareAudioSources()
    {
        foreach (Sound sound in _sounds)
            sound.PrepareAudioSource(gameObject.AddComponent<AudioSource>());
    }

    private Sound FindSoundToPlay(string soundName)
    {
        Sound soundToPlay = Array.Find(_sounds, sound => sound.Name == soundName);
        
        if(soundToPlay == null)
            throw new InvalidOperationException("Sound with this name not found");

        return soundToPlay;
    }
}