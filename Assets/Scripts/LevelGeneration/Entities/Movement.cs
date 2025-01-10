using UnityEngine;
using System.Collections;

namespace LevelGeneration.Entities
{
    public abstract class Movement : MonoBehaviour
    {
        protected bool IsMoving;
        private Coroutine _movingCoroutine;

        private void OnDisable()
        {
            IsMoving = false;

            if (_movingCoroutine != null)
                StopCoroutine(_movingCoroutine);
        }

        protected void Move()
        {
            _movingCoroutine = StartCoroutine(StartMovementAfterDelay());
        }

        protected abstract IEnumerator StartMovementAfterDelay();
    }
}