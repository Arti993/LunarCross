using Infrastructure.Services.Factories.UiFactory;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    [RequireComponent(typeof(Canvas))]
    public class UIRoot : MonoBehaviour
    {
        private const int CameraPlaneDistance = 2;

        private Canvas _canvas;
        private IUiWindowFactory _uiWindowFactory;
        
        private void Construct()
        {
            _uiWindowFactory = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IUiWindowFactory>();
        }
        
        private void Awake()
        {
            Construct();
            
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

