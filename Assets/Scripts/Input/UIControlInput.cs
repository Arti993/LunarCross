using UnityEngine;

public class UIControlInput : MonoBehaviour, IControlInput
{
    private Vector2 _currentInput;

    public void ApplyControl(Vector2 input)
    {
        _currentInput = input;
    }
    
    public Vector2 GetMoveInput()
    {
        return _currentInput;
    }
}
