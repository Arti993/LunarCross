using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBootstrap : MonoBehaviour
{
    private AllServicesContainer _allServices;

    private void Awake()
    {
        _allServices = new AllServicesContainer();
        
        RegisterServices();
    }

    private void RegisterServices()
    {
        _allServices.RegisterService<IAssets>(new AssetProvider());
        
        _allServices.RegisterService<IGameProgress>(new GameProgress());
        
        _allServices.RegisterService<ILevelsSettingsNomenclature>(new LevelsSettingsNomenclature());
        
        _allServices.RegisterService<IUiWindowFactory>(new UiWindowFactory(
            _allServices.GetService<IAssets>()));
        
        _allServices.RegisterService<IParticleSystemFactory>(new ParticleSystemFactory(
            _allServices.GetService<IAssets>()));
        
        _allServices.RegisterService<IGameplayFactory>(new GameplayFactory(
            _allServices.GetService<IAssets>()));
    }
}
