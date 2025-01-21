using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

namespace LevelGeneration.Entities
{
    public abstract class Movement : MonoBehaviour
    {
        [SerializeField] protected float MinDelayBeforeMove = 0f;
        [SerializeField] protected float MaxDelayBeforeMove = 0.7f;
        
        protected Transform Transform;
        protected bool IsLoopMoving;
        protected float StartTime;
        protected float DelayBeforeMove;
        private Coroutine MovingCoroutine;

        private void Awake()
        {
            Transform = transform;
        }

        private void OnEnable()
        {
            Move();
        }

        private void OnDisable()
        {
            IsLoopMoving = false;

            if (MovingCoroutine != null)
                StopCoroutine(MovingCoroutine);
        }

        private void Move()
        {
            DelayBeforeMove = Random.Range(MinDelayBeforeMove, MaxDelayBeforeMove);
            
            MovingCoroutine = StartCoroutine(StartLoopMovementAfterDelay());
        }

        protected void DetectMovementStart()
        {
            StartTime = Time.time;
            IsLoopMoving = true;
        }

        protected abstract IEnumerator StartLoopMovementAfterDelay();
    }
}