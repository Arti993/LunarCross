using Data;
using Infrastructure.Services.ScreenFader;
using Infrastructure.UIStateMachine;
using Infrastructure.UIStateMachine.States;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        private const string NotFirstGameLaunch = "NotFirstGameLaunch";
        private IScreenFader _screenFader;
        private IUiStateMachine _uiStateMachine;

        private void Construct()
        {
            _screenFader = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IScreenFader>();
            _uiStateMachine = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IUiStateMachine>();
        }

        private void Awake()
        {
            Construct();
        }

        public void OnPlayButtonClick()
        {
            _screenFader.FadeOutAndLoadScene((int) SceneIndex.Gameplay);
        }

        public void OnLevelsChooseButtonCLick()
        {
            _screenFader.FadeOutAndLoadScene((int) SceneIndex.LevelChoose);
        }

        public void OnNewGameButtonClick()
        {
            if (PlayerPrefs.HasKey(NotFirstGameLaunch))
            {
                _uiStateMachine.SetState<UiStateRestartGameQuestion>();
            }
            else
            {
                OnTutorialButtonClick();

                PlayerPrefs.SetInt(NotFirstGameLaunch, 1);
                PlayerPrefs.Save();
            }
        }

        public void OnTutorialButtonClick()
        {
            _screenFader.FadeOutAndLoadScene((int) SceneIndex.Tutorial);
        }

        public void OnLeaderBoardButtonClick()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        if(YandexGame.auth)
            {
                _uiStateMachine.SetState<UiStateLeaderboard>();
            }
        else
            {
                _uiStateMachine.SetState<UiStateAuthorizationQuestion>();
            }
#endif
        }

        public void OnSettingsButtonClick()
        {
            _uiStateMachine.SetState<UiStateSettings>();
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}