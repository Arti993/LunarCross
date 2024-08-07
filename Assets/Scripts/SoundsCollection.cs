using System;
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

    public void StopAllSounds()
    {
        foreach (Sound sound in _sounds)
            sound.StopPlay();
    }

    public void MuteAllSounds()
    {
        foreach (Sound sound in _sounds)
            sound.Mute();
    }
    
    public void UnMuteAllSounds()
    {
        foreach (Sound sound in _sounds)
            sound.UnMute();
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