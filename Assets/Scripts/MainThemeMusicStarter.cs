using UnityEngine;

public class MainThemeMusicStarter : MonoBehaviour
{
    private void Awake()
    {
        DIServicesContainer.Instance.GetService<IAudioPlayback>().PlayMenuTheme();
    }
}
