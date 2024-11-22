using Infrastructure;
using Infrastructure.Services.Factories.UiFactory;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Canvas))]
    public class UIRoot : MonoBehaviour
    {
        private const int CameraPlaneDistance = 2;

        private Canvas _canvas;

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
            _canvas.planeDistance = CameraPlaneDistance;
        }
    }
}

