using UnityEngine;
using System.Collections;

namespace LevelGeneration.Entities
{
    public abstract class Movement : MonoBehaviour
    {
        protected bool IsMoving;
        private Coroutine MovingCoroutine;

        private void OnDisable()
        {
            IsMoving = false;

            if (MovingCoroutine != null)
                StopCoroutine(MovingCoroutine);
        }

        protected void Move()
        {
            MovingCoroutine = StartCoroutine(StartMovementAfterDelay());
        }

        protected abstract IEnumerator StartMovementAfterDelay();
    }
}