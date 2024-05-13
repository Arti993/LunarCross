using System.Collections;
using Agava.YandexGames;
using UnityEngine;

public class YandexSDKInitializer : MonoBehaviour
{
    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize(OnInitialized);
    }

    private void OnInitialized()
    {
        IJunior.TypedScenes.MainMenu.Load();
    }
}
