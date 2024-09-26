using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    protected bool IsMoving;
    protected Coroutine MovingCoroutine;

    public abstract void Move();

    private void OnDisable()
    {
        if (MovingCoroutine != null)
            StopCoroutine(MovingCoroutine);
    }
}