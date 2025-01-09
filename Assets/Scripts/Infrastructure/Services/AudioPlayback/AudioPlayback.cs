using System;
using System.Linq;
using Ami.BroAudio;
using Data;
using Infrastructure.Services.GameProgress;
using Infrastructure.Services.LevelSettings;
using Reflex.Attributes;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.AudioPlayback
{
    public class AudioPlayback : IAudioPlayback
    {
        private const string MusicVolumeTag = "LastMusicVolume";
        private const string MusicMutedTag = "MusicMuted";
        private const string SoundsMutedTag = "SoundsMuted";
        private const int MaxConvertedVolume = 100;
        private const float MaxVolume = 1;

        private IGameProgress _gameProgress;
        private ILevelsSettingsNomenclature _levelsSettingsNomenclature;
        private bool _isMenuThemePlaying;

        public AudioPlayback(IGameProgress gameProgress, ILevelsSettingsNomenclature levelsSettingsNomenclature)
        {
            _gameProgress = gameProgress;
            _levelsSettingsNomenclature = levelsSettingsNomenclature;
            
            SoundsContainer = Resources.Load<SoundsContainer>(PrefabsPaths.SoundsContainerPath);
            
            MusicContainer = Resources.Load<MusicContainer>(PrefabsPaths.MusicContainerPath);

            ApplySavedVolume();

            UnMuteAudio();
        }

        public MusicContainer MusicContainer { get; }
        public SoundsContainer SoundsContainer { get; }
        

        public void MuteAudio()
        {
            AudioListener.pause = true;
        }

        public void UnMuteAudio()
        {
            AudioListener.pause = false;
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
            {
                MusicContainer.Play(soundID);
            }
            else
            {
                throw new InvalidOperationException();
            }
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

            SoundID levelTheme;

            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (currentSceneIndex != (int) SceneIndex.Tutorial)
            {
                int levelNumber = _gameProgress.GetCurrentLevelNumber();

                levelTheme = _levelsSettingsNomenclature.GetLevelSettings(levelNumber).MusicTheme;
            }
            else
            {
                levelTheme = _levelsSettingsNomenclature.GetTutorialLevelSettings().MusicTheme;
            }

            MusicContainer.Play(levelTheme);
        }

        public void PlayExplosionSound()
        {
            MusicContainer.Mute();

            SoundsContainer.Play(SoundsContainer.Explosion);
        }

        public void SaveVolume(float volume, string VolumeTag)
        {
            var convertedVolume = (int) Mathf.Round(volume * MaxConvertedVolume);

            PlayerPrefs.SetInt(VolumeTag, convertedVolume);

            PlayerPrefs.Save();
        }

        public float GetLastSavedVolume(string VolumeTag)
        {
            int convertedVolume = PlayerPrefs.GetInt(VolumeTag, MaxConvertedVolume);

            float unConvertedVolume = (float) convertedVolume / MaxConvertedVolume;

            return unConvertedVolume;
        }

        private void ApplySavedVolume()
        {
            if (PlayerPrefs.HasKey(MusicMutedTag))
                MuteMusic();
            else if (PlayerPrefs.HasKey(MusicVolumeTag))
                ChangeMusicVolume(GetLastSavedVolume(MusicVolumeTag));
            else
                ChangeMusicVolume(MaxVolume);

            if (PlayerPrefs.HasKey(SoundsMutedTag))
                MuteSounds();
        }
    }
}