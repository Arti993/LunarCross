using Data;
using YG;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tools
{
    public class YandexSDKInitializer : MonoBehaviour
    {
        private void Update()
        {
            if (YandexGame.SDKEnabled)
                StartGame();
        }

        private void StartGame()
        {
            SceneManager.LoadScene((int) SceneIndex.MainMenu);
        }
    }
}
