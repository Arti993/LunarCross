using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplaySceneBootstrap : MonoBehaviour
{
    //найти всех кто обращается к бутстрапу и переделать.
    
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Camera _camera;
    
    private AllServicesContainer _allServices;

    private GameObject _player;

    public GameObject Player => _player;

    private void Awake()
    {
        _allServices = new AllServicesContainer();
        
        RegisterServices();
        
        _player = _allServices.GetService<IGameplayFactory>().CreatePlayer(_startPoint.position);
    }

    private void Start()
    {
        _allServices.GetService<IUiWindowFactory>().GetPauseButton();
    }

    private void RegisterServices()
    {
        _allServices.RegisterService<IAssets>(new AssetProvider());
        
        _allServices.RegisterService<IGameProgress>(new GameProgress());
        
        _allServices.RegisterService<ILevelsSettingsNomenclature>(new LevelsSettingsNomenclature());
        
        _allServices.RegisterService<IUiWindowFactory>(new UiWindowFactory(
            _allServices.GetService<IAssets>(), _camera));
        
        _allServices.RegisterService<IParticleSystemFactory>(new ParticleSystemFactory(
            _allServices.GetService<IAssets>()));
        
        _allServices.RegisterService<IGameplayFactory>(new GameplayFactory(
            _allServices.GetService<IAssets>()));
    }
}
