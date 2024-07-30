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

    public void PlaySingleSound(string soundName)
    {
        Sound soundToPlay = FindSoundToPlay(soundName);
        
        soundToPlay.Play();
    }

    public void PlayMusic(string musicName)
    {
        Sound musicToPlay = FindSoundToPlay(musicName);

        _currentPlayingMusic?.Stop();

        _currentPlayingMusic = musicToPlay;
        
        _currentPlayingMusic.Play();
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