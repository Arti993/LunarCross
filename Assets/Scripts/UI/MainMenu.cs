using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private const string NotFirstGameLaunch = "NotFirstGameLaunch";
    private Canvas _uiRoot;
    private float _delay = 0.5f;

    private void Awake()
    {
        _uiRoot = GetComponentInParent<Canvas>();
    }

    public void OnPlayButtonClick()
    {
        CloseUi();
        
        DIServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene((int)SceneIndex.Gameplay);
    }

    public void OnLevelsChooseButtonCLick()
    {
        DIServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene((int)SceneIndex.LevelChoose);
    }

    public void OnNewGameButtonClick()
    {
        if (PlayerPrefs.HasKey(NotFirstGameLaunch))
            DIServicesContainer.Instance.GetService<IUiWindowFactory>()
                .GetWindow(PrefabsPaths.RestartGameQuestion, _uiRoot.gameObject);
        else
        {
            OnTutorialButtonClick();

            PlayerPrefs.SetInt(NotFirstGameLaunch, 1);
            PlayerPrefs.Save();
        }
    }

    public void OnTutorialButtonClick()
    {
        CloseUi();
        
        DIServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene((int)SceneIndex.Tutorial);
    }

    public void OnLeaderBoardButtonClick()
    {
        CloseUi();
        
        DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateLeaderboard>();
    }

    public void OnSettingsButtonClick()
    {
        DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateSettings>();
    }

    public void Disable()
    {
        StartCoroutine(DisableWithDelay());
    }

    private IEnumerator DisableWithDelay()
    {
       yield return new WaitForSeconds(_delay);
       
       gameObject.SetActive(false);
    }

    private void CloseUi()
    {
        DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateNoWindow>();
    }
}