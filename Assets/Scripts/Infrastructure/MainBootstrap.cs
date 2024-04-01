using UnityEngine;

public class MainBootstrap : MonoBehaviour
{
    private AllServicesContainer _allServices;
    private static bool _isFirstAwakened;

    private void Awake()
    {
        if (_isFirstAwakened) 
            return;
        
        _allServices = new AllServicesContainer();
            
        RegisterServices();

        _isFirstAwakened = true;
    }

    private void RegisterServices()
    {
        _allServices.RegisterService<IAssets>(new AssetProvider());
        
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
}
