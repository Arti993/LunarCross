using Agava.WebUtility;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFrustrumChanger : MonoBehaviour
{
    private float horizontalFOVScale = 0.85f; 
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        
        if (Device.IsMobile)
            ChanfeFrustumForMobileDevices();
    }

    private void ChanfeFrustumForMobileDevices()
    {
        Matrix4x4 projectionMatrix = _camera.projectionMatrix;

        float aspect = _camera.aspect;
        float fovY = _camera.fieldOfView * Mathf.Deg2Rad * 0.5f;
        
        float fovX = 2f * Mathf.Atan(Mathf.Tan(fovY) * aspect * horizontalFOVScale);
        
        projectionMatrix.m00 = 1f / Mathf.Tan(fovX * 0.5f);
        projectionMatrix.m11 = 1f / Mathf.Tan(fovY);
        
        _camera.projectionMatrix = projectionMatrix;
    }
}
