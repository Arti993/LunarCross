using Agava.YandexGames;
using UnityEngine;

public class MainBootstrap : MonoBehaviour
{
    //ВКЛЮЧИТЬ В AWAKE ЯНДЕКС!!!
    
    private DIServicesContainer _diContainer;
    private static bool _isFirstAwake = true;

    private void Awake()
    {
        //YandexGamesSdk.GameReady();

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
        _diContainer.RegisterService<IAssets>(new AssetProvider());

        _diContainer.RegisterService<ILocalization>(new Localization(
            _diContainer.GetService<IAssets>()));
        
        _diContainer.RegisterService<IAudioPlayback>(new AudioPlayback(
            _diContainer.GetService<IAssets>()));
        
        _diContainer.RegisterService<IGameProgress>(new GameProgress());
        
        _diContainer.RegisterService<ILevelsSettingsNomenclature>(new LevelsSettingsNomenclature());
        
        _diContainer.RegisterService<IScreenFader>(new ScreenFader(
            _diContainer.GetService<IAssets>()));
        
        _diContainer.RegisterService<IScenesLoader>(new ScenesLoader());
        
        _diContainer.RegisterService<IUiWindowFactory>(new UiWindowFactory(
            _diContainer.GetService<IAssets>()));
        
        _diContainer.RegisterService<IParticleSystemFactory>(new ParticleSystemFactory(
            _diContainer.GetService<IAssets>()));
        
        _diContainer.RegisterService<IGameplayFactory>(new GameplayFactory(
            _diContainer.GetService<IAssets>()));
    }

    private void OpenMainMenu()
    {
        GameObject uiRootObject = DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();
        
        DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetMainMenuButtonsWindow(uiRootObject);
        
        DIServicesContainer.Instance.GetService<IAudioPlayback>().PlayMenuTheme();
    }
}
