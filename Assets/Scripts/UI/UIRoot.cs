using System;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class UIRoot : MonoBehaviour
{
    private Canvas _canvas;

    public PauseButton PauseButton { get; private set; }

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    public void SetCamera(Camera camera)
    {
        _canvas.worldCamera = camera;
        _canvas.planeDistance = 4;
    }


    public void SetPauseButtonIfItNotExist(PauseButton pauseButton)
    {
        if(PauseButton != null)
            throw new InvalidOperationException();

        PauseButton = pauseButton;
    }
}
