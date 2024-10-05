using System.Collections;
using YG;
using Ami.BroAudio;
using UnityEngine;

public class MainBootstrap : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private DIServicesContainer _diContainer;
    private IAssetsProvider _provider;
    private float _enableFocusTestDelay = 0.6f;
    private static bool _isFirstAwake = true;

    private void Awake()
    {
        if (_isFirstAwake)
        {
            
#if UNITY_WEBGL && !UNITY_EDITOR
    YandexGame.GameReadyAPI();
    YandexGame.SetFullscreen(true);
#endif
            
            _diContainer = new DIServicesContainer();

            RegisterServices();

            PrepareUiStateMachine();

            _isFirstAwake = false;

            StartCoroutine(EnableFocusTest());
        }

        OpenMainMenu();
    }

    private void RegisterServices()
    {
        _diContainer.RegisterService<IAssetsProvider>(new AssetsProvider());

        _provider = _diContainer.GetService<IAssetsProvider>();

        _diContainer.RegisterService<ILocalization>(new Localization(_provider));

        _diContainer.RegisterService<IAudioPlayback>(new AudioPlayback());

        _diContainer.RegisterService<IGameProgress>(new GameProgress());

        _diContainer.RegisterService<ILevelsSettingsNomenclature>(new LevelsSettingsNomenclature());

        _diContainer.RegisterService<IScreenFader>(new ScreenFader(_provider));

        _diContainer.RegisterService<IUiWindowFactory>(new UiWindowFactory(_provider));

        _diContainer.RegisterService<IParticleSystemFactory>(new ParticleSystemFactory(_provider));

        _diContainer.RegisterService<IGameplayFactory>(new GameplayFactory(_provider));

        _diContainer.RegisterService<IVideoAdService>(new VideoAdService(_provider));

        _diContainer.RegisterService<IUiStateMachine>(new UiStateMachine());
    }

    private void PrepareUiStateMachine()
    {
        AddState(new UiStateMainMenu());
        AddState(new UiStateLeaderboard());
        AddState(new UiStateSettings());
        AddState(new UiStateGameComplete());
        AddState(new UiStatePauseMenu());
        AddState(new UiStatePauseButton());
        AddState(new UIStateLevelComplete());
        AddState(new UiStateLevelFailed());
        AddState(new UiStateRestartGameQuestion());
        AddState(new UiStateAuthorizationQuestion());
        AddState(new UiStateTutorialAliens());
        AddState(new UiStateTutorialAstronauts());
        AddState(new UiStateTutorialTornado());
        AddState(new UiStateTutorialObstacles());
        AddState(new UiStateTutorialFinish());
        AddState(new UiStateTutorialTouchscreenControl());
        AddState(new UiStateTutorialKeyboardControl());
        AddState(new UiStateNoWindow());
    }

    private void AddState(UiStateMachineState state)
    {
        _diContainer.GetService<IUiStateMachine>().AddState(state);
    }

    private void OpenMainMenu()
    {
        GameObject uiRootObject = DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();

        uiRootObject.GetComponent<UIRoot>().SetCamera(_camera);

        DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetWindow(PrefabsPaths.GameMainTitle, uiRootObject);

        DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateMainMenu>();

        StartMenuMusicTheme();
    }

    private IEnumerator EnableFocusTest()
    {
        yield return new WaitForSeconds(_enableFocusTestDelay);

        _diContainer.RegisterService<IFocusTestStateChanger>(new FocusTestStateChanger(_provider));
    }

    private void StartMenuMusicTheme()
    {
        SoundID menuMusicTheme = DIServicesContainer.Instance.GetService<IAudioPlayback>().MusicContainer.MenuTheme;

        DIServicesContainer.Instance.GetService<IAudioPlayback>().PlayMusic(menuMusicTheme);
    }
}