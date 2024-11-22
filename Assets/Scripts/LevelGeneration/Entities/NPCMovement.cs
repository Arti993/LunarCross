using System.Collections;
using UnityEngine;
using Vehicle.ReactZones;
using Random = UnityEngine.Random;

namespace LevelGeneration.Entities
{
    [RequireComponent(typeof(Rigidbody))]
    public class NPCMovement : MonoBehaviour
    {
        private const float ChangeDirectionInterval = 5f;
        private const float Speed = 0.8f;
        private const float MaxDeflectAngle = 60;

        private Coroutine _changeDirectionCoroutine;
        private Vector3 _moveDirection;
        private Rigidbody _rigidbody;
        private Transform _transform;
        private bool _canTrigger;

        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _transform = transform;
            _transform.rotation = Quaternion.identity;
            _moveDirection = Vector3.zero;
            _canTrigger = true;

            Move();

            _changeDirectionCoroutine = StartCoroutine(ChangeDirectionWithInterval());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Bumper bumper) || other.TryGetComponent(out VehicleCatchZone zone))
                _canTrigger = false;

            if (_canTrigger && (other.isTrigger || other.TryGetComponent(out Obstacle obstacle)))
                ReflectDirection(other);
        }

        public void Disable()
        {
            _rigidbody.velocity = Vector3.zero;

            StopCoroutine(_changeDirectionCoroutine);

            enabled = false;
        }

        private void Move()
        {
            _rigidbody.velocity = Vector3.zero;

            if (_moveDirection == Vector3.zero)
                _moveDirection = GetRandomDirection();

            _transform.rotation = Quaternion.LookRotation(_moveDirection);

            _rigidbody.AddForce(transform.forward * Speed, ForceMode.Impulse);
        }

        private void ReflectDirection(Collider otherCollider)
        {
            if (_changeDirectionCoroutine != null)
                StopCoroutine(_changeDirectionCoroutine);

            Vector3 position = _transform.position;

            Vector3 normal = otherCollider.ClosestPointOnBounds(position) - position;

            Vector3 newDirection = new Vector3(-normal.x, 0f, -normal.z).normalized;

            Quaternion deflection = Quaternion.Euler(0f, Random.Range(0f, MaxDeflectAngle), 0f);

            _moveDirection = deflection * newDirection;

            Move();

            if (gameObject.activeSelf)
                _changeDirectionCoroutine = StartCoroutine(ChangeDirectionWithInterval());
        }

        private Vector3 GetRandomDirection()
        {
            return new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        }

        private IEnumerator ChangeDirectionWithInterval()
        {
            yield return new WaitForSeconds(ChangeDirectionInterval);

            _moveDirection = GetRandomDirection();

            Move();
        }
    }
}