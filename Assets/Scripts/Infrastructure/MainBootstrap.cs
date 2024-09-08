using Agava.WebUtility;
using Agava.YandexGames;
using UnityEngine;

public class MainBootstrap : MonoBehaviour
{
    private DIServicesContainer _diContainer;
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

            _isFirstAwake = false;
        }

        OpenMainMenu();
    }

    private void RegisterServices()
    {
        _diContainer.RegisterService<IAssetsProvider>(new AssetsProvider());

        IAssetsProvider provider = _diContainer.GetService<IAssetsProvider>();

        _diContainer.RegisterService<ILocalization>(new Localization(provider));

        _diContainer.RegisterService<IAudioPlayback>(new AudioPlayback(provider));

        _diContainer.RegisterService<IGameProgress>(new GameProgress());

        _diContainer.RegisterService<ILevelsSettingsNomenclature>(new LevelsSettingsNomenclature());

        _diContainer.RegisterService<IScreenFader>(new ScreenFader(provider));

        _diContainer.RegisterService<IScenesLoader>(new ScenesLoader());

        _diContainer.RegisterService<IUiWindowFactory>(new UiWindowFactory(provider));

        _diContainer.RegisterService<IParticleSystemFactory>(new ParticleSystemFactory(provider));

        _diContainer.RegisterService<IGameplayFactory>(new GameplayFactory(provider));
        
        _diContainer.RegisterService<IFocusTestStateChanger>(new FocusTestStateChanger(provider));

        _diContainer.RegisterService<IVideoAdService>(new VideoAdService(provider));
    }

    private void OpenMainMenu()
    {
        GameObject uiRootObject = DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();

        DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetMainMenuButtonsWindow(uiRootObject);

        DIServicesContainer.Instance.GetService<IAudioPlayback>().PlayMenuTheme();
    }
}