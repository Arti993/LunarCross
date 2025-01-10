using Infrastructure.Services.Factories.UiFactory;
using Reflex.Extensions;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayersInput
{
    public class UIControlInput : MonoBehaviour, IControlInput
    {
        [SerializeField] private GameObject _controlButtons;
        private Vector2 _currentInput;
        private PauseButton _pauseButton;
        private IUiWindowFactory _uiWindowFactory;

        private void Construct()
        {
            _uiWindowFactory = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IUiWindowFactory>();
        }

        private void Awake()
        {
            Construct();
        }

        private void Start()
        {
            GameObject uiRoot = _uiWindowFactory.GetUIRoot();

            _pauseButton = uiRoot.GetComponentInChildren<PauseButton>();
            _pauseButton.Enabled += OnControlEnabled;
            _pauseButton.Disabled += OnControlDisabled;
        }

        private void OnDisable()
        {
            _pauseButton.Enabled -= OnControlEnabled;
            _pauseButton.Disabled -= OnControlDisabled;
        }

        public void ApplyControl(Vector2 input)
        {
            _currentInput = input;
        }

        public Vector2 GetMoveInput()
        {
            return _currentInput;
        }

        private void OnControlEnabled()
        {
            _controlButtons.SetActive(true);
        }

        private void OnControlDisabled()
        {
            _controlButtons.SetActive(false);
        }
    }
}
