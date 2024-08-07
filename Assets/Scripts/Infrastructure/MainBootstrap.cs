using Agava.YandexGames;
using UnityEngine;

public class MainBootstrap : MonoBehaviour
{
    //ВКЛЮЧИТЬ В AWAKE ЯНДЕКС!!!
    
    private AllServicesContainer _allServices;
    private static bool _isFirstAwakened;

    private void Awake()
    {
        //YandexGamesSdk.GameReady();

        if (_isFirstAwakened == false)
        {
            _allServices = new AllServicesContainer();
            
            RegisterServices();

            _isFirstAwakened = true;
        }
        
        OpenMainMenu();
    }

    private void RegisterServices()
    {
        _allServices.RegisterService<IAssets>(new AssetProvider());

        _allServices.RegisterService<ILocalization>(new Localization(
            _allServices.GetService<IAssets>()));
        
        _allServices.RegisterService<IAudioPlayback>(new AudioPlayback(
            _allServices.GetService<IAssets>()));
        
        _allServices.RegisterService<IGameProgress>(new GameProgress());
        
        _allServices.RegisterService<ILevelsSettingsNomenclature>(new LevelsSettingsNomenclature());
        
        _allServices.RegisterService<IScreenFader>(new ScreenFader(
            _allServices.GetService<IAssets>()));
        
        _allServices.RegisterService<IUiWindowFactory>(new UiWindowFactory(
            _allServices.GetService<IAssets>()));
        
        _allServices.RegisterService<IParticleSystemFactory>(new ParticleSystemFactory(
            _allServices.GetService<IAssets>()));
        
        _allServices.RegisterService<IGameplayFactory>(new GameplayFactory(
            _allServices.GetService<IAssets>()));
    }

    private void OpenMainMenu()
    {
        GameObject uiRootObject = AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();
        
        AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetMainMenuButtonsWindow(uiRootObject);
        
        AllServicesContainer.Instance.GetService<IAudioPlayback>().PlayMenuTheme();
    }
}
