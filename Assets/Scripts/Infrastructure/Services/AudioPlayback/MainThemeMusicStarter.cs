using Ami.BroAudio;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.Services.AudioPlayback
{
    public class MainThemeMusicStarter : MonoBehaviour
    {
        private IAudioPlayback _audioPlayback;
        
        private void Construct()
        {
            _audioPlayback = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IAudioPlayback>();
        }

        private void Awake()
        {
            Construct();
            
            SoundID menuMusicTheme = _audioPlayback.MusicContainer.MenuTheme;

            _audioPlayback.PlayMusic(menuMusicTheme);
        }
    }
}