using Ami.BroAudio;
using Reflex.Attributes;
using UnityEngine;

namespace Infrastructure.Services.AudioPlayback
{
    public class MainThemeMusicStarter : MonoBehaviour
    {
        private IAudioPlayback _audioPlayback;
        
        [Inject]
        private void Construct(IAudioPlayback audioPlayback)
        {
            _audioPlayback = audioPlayback;
        }
        
        private void Awake()
        {
            SoundID menuMusicTheme = _audioPlayback.MusicContainer.MenuTheme;

            _audioPlayback.PlayMusic(menuMusicTheme);
        }
    }
}