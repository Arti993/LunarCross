using Infrastructure.Services.Factories.UiFactory;
using Reflex.Attributes;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Canvas))]
    public class UIRoot : MonoBehaviour
    {
        private const int CameraPlaneDistance = 2;

        private Canvas _canvas;
        private IUiWindowFactory _uiWindowFactory;

        [Inject]
        private void Construct(IUiWindowFactory uiWindowFactory)
        {
            _uiWindowFactory = uiWindowFactory;
        }

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        private void OnDisable()
        {
            _uiWindowFactory.DeleteUIRoot();
        }

        public void SetCamera(Camera camera)
        {
            _canvas.worldCamera = camera;
            _canvas.planeDistance = CameraPlaneDistance;
        }
    }
}

