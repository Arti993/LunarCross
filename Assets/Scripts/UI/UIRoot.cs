using System;
using UnityEngine;

public class UIRoot : MonoBehaviour
{
    private PauseButton _pauseButton;
    
    public PauseButton PauseButton => _pauseButton;

    public void SetPauseButtonIfItNotExist(PauseButton pauseButton)
    {
        if(_pauseButton != null)
            throw new InvalidOperationException();

        _pauseButton = pauseButton;
    }
}
