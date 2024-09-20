using UnityEngine.EventSystems;
using UnityEngine;

public class ControlButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private UIControlInput _input;
    [SerializeField] private ControlButton _otherButton;
    [SerializeField] private Vector2 _direction;
    
    private bool _isPressed;

    public bool IsPressed => _isPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
        _input.ApplyControl(_direction);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;

        if (_otherButton.IsPressed == false)
            _input.ApplyControl(Vector2.zero);
    }
}