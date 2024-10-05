using System;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class UIRoot : MonoBehaviour
{
    private Canvas _canvas;
    private int _cameraPlaneDistance = 2;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    private void OnDisable()
    {
        DIServicesContainer.Instance.GetService<IUiWindowFactory>().DeleteUIRoot();
    }

    public void SetCamera(Camera camera)
    {
        _canvas.worldCamera = camera;
        _canvas.planeDistance = _cameraPlaneDistance;
    }
}
