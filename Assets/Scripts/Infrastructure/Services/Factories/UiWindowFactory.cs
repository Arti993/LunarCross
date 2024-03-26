using System;
using UnityEngine;

public class UiWindowFactory : IUiWindowFactory
{
    private readonly IAssets _provider;
    private GameObject _uiRootObject;
    private Canvas _canvas;

    public UiWindowFactory(IAssets provider, Camera camera)
    {
        _provider = provider;
        _uiRootObject = GetUIRoot();
        _canvas = _uiRootObject.GetComponent<Canvas>();
        _canvas.worldCamera = camera;
        _canvas.planeDistance = 4;
    }

    public GameObject GetUIRoot()
    {
        return _uiRootObject ? _uiRootObject : (_uiRootObject = _provider.Instantiate("Prefabs/UI/UIRoot"));
    }

    public GameObject GetPauseButton()
    {
        CheckUIRootNotNull();

        _uiRootObject.TryGetComponent(out UIRoot uiRoot);

        if (uiRoot.PauseButton == null)
        {
            GameObject pauseButtonObject = _provider.Instantiate("Prefabs/UI/PauseButton", _uiRootObject.transform);

            pauseButtonObject.TryGetComponent(out PauseButton pauseButton);
            
            uiRoot.SetPauseButtonIfItNotExist(pauseButton);

            return pauseButtonObject;
        }
        else
        {
            return uiRoot.PauseButton.gameObject;
        }
    }

    public GameObject GetLevelCompleteWindow()
    {
        CheckUIRootNotNull();
        
        return _provider.Instantiate("Prefabs/UI/LevelCompleteWindow", _uiRootObject.transform);
    }
    
    public GameObject GetPauseMenuWindow()
    {
        CheckUIRootNotNull();
        
        return _provider.Instantiate("Prefabs/UI/PauseMenu", _uiRootObject.transform);
    }
    
    public GameObject GetLevelFailedWindow()
    {
        CheckUIRootNotNull();
        
        return _provider.Instantiate("Prefabs/UI/LevelFailedWindow", _uiRootObject.transform);
    }

    private bool CheckUIRootNotNull()
    {
        if(_uiRootObject == null)
            throw new InvalidOperationException();

        return true;
    }
}
