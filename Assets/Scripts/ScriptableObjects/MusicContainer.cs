using System;
using System.Collections.Generic;
using System.Linq;
using Ami.BroAudio;
using Infrastructure.Services.AudioPlayback;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "New MusicContainer", menuName = "MusicContainer/Create New MusicContainer",
        order = 51)]
    public class MusicContainer : ScriptableObject
    {
        private const string MusicMutedTag = "MusicMuted";
        private const string MusicVolumeTag = "LastMusicVolume";

        [SerializeField] private SoundID _menuTheme;
        [SerializeField] private SoundID _finalTheme;
        [SerializeField] private SoundID _marsLevel;
        [SerializeField] private SoundID _moonLevel;
        [SerializeField] private SoundID _sandLevel;
        [SerializeField] private SoundID _jungleLevel;
        [SerializeField] private SoundID _snowLevel;
        [SerializeField] private SoundID _blackLevel;

        private SoundID _currentPlayingMusicID;

        public SoundID MenuTheme => _menuTheme;
        public SoundID FinalTheme => _finalTheme;
        
        public void Play(SoundID soundID)
        {
            if(GetIdList().Contains(soundID) == false)
                throw new InvalidOperationException();

            BroAudio.Stop(_currentPlayingMusicID);

            _currentPlayingMusicID = soundID;

            float volume;

            if (PlayerPrefs.HasKey(MusicMutedTag))
                volume = 0f;
            else
            {
                IAudioPlayback audioPlayback = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IAudioPlayback>();
                
                volume = audioPlayback.GetLastSavedVolume(MusicVolumeTag);
            }
            
            BroAudio.Play(soundID);

            BroAudio.SetVolume(soundID, volume);
        }

        public void Mute()
        {
            BroAudio.SetVolume(_currentPlayingMusicID, 0f);
        }

        public void SetVolume(float volume)
        {
            BroAudio.SetVolume(_currentPlayingMusicID, volume);
        }
        
        private IReadOnlyList<SoundID> GetIdList()
        {
            List<SoundID> music = new List<SoundID>
            {
                _menuTheme,
                _finalTheme,
                _marsLevel,
                _sandLevel,
                _jungleLevel,
                _snowLevel,
                _blackLevel
            };

            return music;
        }
    }
}
