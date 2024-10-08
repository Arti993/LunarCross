using YG;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YandexSDKInitializer : MonoBehaviour
{
    private void Update()
    {
        if (YandexGame.SDKEnabled)
            StartGame();
    }

    private void StartGame()
    {
        SceneManager.LoadScene((int)SceneIndex.MainMenu);
    }
}
