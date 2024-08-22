using System;
using UnityEngine;

[System.Serializable]
public class Sound
{
   [SerializeField] private string _name;

   [SerializeField] private AudioClip _audioClip;
   
   [Range(0f,1f)]
   [SerializeField] private float _volume;
   
   [Range(1f,3f)]
   [SerializeField] private float _pitch;
   
   [SerializeField] private bool _loop;

   private AudioSource _audioSource;

   public string Name => _name;
   public bool Loop => _loop;

   public void PrepareAudioSource(AudioSource audioSource)
   {
      if (_audioSource != null)
         return;

      _audioSource = audioSource;
      _audioSource.clip = _audioClip;
      _audioSource.volume = _volume;
      _audioSource.pitch = _pitch;
      _audioSource.loop = _loop;
   }

   public void Play()
   {
      if (_audioSource == null)
         throw new InvalidOperationException();
      
      _audioSource.Play();
   }

   public void StopPlay()
   {
      if (_audioSource == null)
         throw new InvalidOperationException();
      
      _audioSource.Stop();
   }

   public void Mute()
   {
      if (_audioSource == null)
         throw new InvalidOperationException();

      _audioSource.volume = 0f;
   }
   
   public void UnMute()
   {
      if (_audioSource == null)
         throw new InvalidOperationException();

      _audioSource.volume = _volume;
   }

   public void ChangeVolume(float volume)
   {
      _volume = volume;
      _audioSource.volume = _volume;
   }
}
