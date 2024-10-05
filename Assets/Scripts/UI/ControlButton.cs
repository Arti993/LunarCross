using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class ControlButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private UIControlInput _input;
    [SerializeField] private Image _image;
    [SerializeField] private ControlButton _otherButton;
    [SerializeField] private Vector2 _direction;

    private Transform _transform;
    private Color _startColor;
    private Vector3 _startScale;
    private bool _isPressed;

    public bool IsPressed => _isPressed;

    private void Start()
    {
        _transform = transform;
        _startColor = _image.color;
        _startScale = _transform.localScale;
    }

    private void OnDisable()
    {
        PointerUp();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
        _input.ApplyControl(_direction);
        _image.color = Color.gray;
        _transform.localScale *= 0.9f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PointerUp();
    }

    private void PointerUp()
    {
        _isPressed = false;

        if (_otherButton.IsPressed == false)
            _input.ApplyControl(Vector2.zero);

        _image.color = _startColor;
        _transform.localScale = _startScale;
    }
}