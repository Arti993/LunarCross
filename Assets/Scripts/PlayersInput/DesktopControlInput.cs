using UnityEngine;

namespace PlayersInput
{
    public class DesktopControlInput : MonoBehaviour, IControlInput
    {
        private PlayerInput _playerInput;

        private void OnEnable()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        public Vector2 GetMoveInput()
        {
            return _playerInput.Player.Turn.ReadValue<Vector2>();
        }
    }
}
