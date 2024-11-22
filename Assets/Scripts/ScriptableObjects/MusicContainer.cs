using System.Collections.Generic;
using Ami.BroAudio;
using Infrastructure;
using Infrastructure.Services.AudioPlayback;
using UnityEngine;

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

        public IReadOnlyList<SoundID> GetIdList()
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

        public void Play(SoundID soundID)
        {
            BroAudio.Stop(_currentPlayingMusicID);

            _currentPlayingMusicID = soundID;

            float volume;

            if (PlayerPrefs.HasKey(MusicMutedTag))
                volume = 0f;
            else
                volume = DIServicesContainer.Instance.GetService<IAudioPlayback>().GetLastSavedVolume(MusicVolumeTag);

            BroAudio.Play(soundID);

            BroAudio.SetVolume(soundID, volume);
        }

        public void Stop(SoundID soundID)
        {
            BroAudio.Stop(soundID);
        }

        public void Mute()
        {
            BroAudio.SetVolume(_currentPlayingMusicID, 0f);
        }

        public void SetVolume(float volume)
        {
            BroAudio.SetVolume(_currentPlayingMusicID, volume);
        }
    }
}
