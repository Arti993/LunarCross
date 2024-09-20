using Agava.WebUtility;
using Agava.YandexGames;
using Ami.BroAudio;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainBootstrap : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    
    private DIServicesContainer _diContainer;
    private UiStateMachine _uiStateMachine;
    private static bool _isFirstAwake = true;

    private void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    YandexGamesSdk.GameReady();
#endif

        if (_isFirstAwake)
        {
            _diContainer = new DIServicesContainer();

            RegisterServices();
            
            MouseClickOnScreenForStartAudio();
            
            PrepareUiStateMachine();

            _isFirstAwake = false;
        }
        
        OpenMainMenu();
    }

    private void RegisterServices()
    {
        _diContainer.RegisterService<IAssetsProvider>(new AssetsProvider());

        IAssetsProvider provider = _diContainer.GetService<IAssetsProvider>();

        _diContainer.RegisterService<ILocalization>(new Localization(provider));

        _diContainer.RegisterService<IAudioPlayback>(new AudioPlayback());

        _diContainer.RegisterService<IGameProgress>(new GameProgress());

        _diContainer.RegisterService<ILevelsSettingsNomenclature>(new LevelsSettingsNomenclature());

        _diContainer.RegisterService<IScreenFader>(new ScreenFader(provider));

        _diContainer.RegisterService<IUiWindowFactory>(new UiWindowFactory(provider));
        
        _diContainer.RegisterService<IParticleSystemFactory>(new ParticleSystemFactory(provider));

        _diContainer.RegisterService<IGameplayFactory>(new GameplayFactory(provider));
        
        _diContainer.RegisterService<IFocusTestStateChanger>(new FocusTestStateChanger(provider));

        _diContainer.RegisterService<IVideoAdService>(new VideoAdService(provider));
        
        _uiStateMachine = new UiStateMachine();

        _diContainer.RegisterService<IUiStateMachine>(_uiStateMachine);
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

        SoundID menuMusicTheme = DIServicesContainer.Instance.GetService<IAudioPlayback>().MusicContainer.MenuTheme;
        
        DIServicesContainer.Instance.GetService<IAudioPlayback>().PlayMusic(menuMusicTheme);
    }

    private void MouseClickOnScreenForStartAudio()
    {
        Vector3 mousePosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = mousePosition
        };
    }
}