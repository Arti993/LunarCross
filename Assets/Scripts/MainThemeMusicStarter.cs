using Ami.BroAudio;
using UnityEngine;

public class MainThemeMusicStarter : MonoBehaviour
{
    private void Awake()
    {
        SoundID menuMusicTheme = DIServicesContainer.Instance.GetService<IAudioPlayback>().MusicContainer.MenuTheme;
        
        DIServicesContainer.Instance.GetService<IAudioPlayback>().PlayMusic(menuMusicTheme);
    }
}
