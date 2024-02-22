using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class NPCMovement : MonoBehaviour
{
    private float _changeDirectionInterval = 5f;
    private float _speed = 0.8f;
    private Coroutine _changeDirectionCoroutine;
    private Vector3 _moveDirection;
    private Rigidbody _rigidbody;
    private bool _canTrigger = true;
    private float _maxDeflectAngle = 60;
    
    //убрать могические числа

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _moveDirection = GetRandomDirection();
        
        Move();

        _changeDirectionCoroutine = StartCoroutine(ChangeDirectionWithInterval());
    }

    public void Disable()
    {
        _rigidbody.velocity = Vector3.zero;
        
        StopCoroutine(_changeDirectionCoroutine);
        
        enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Bumper>(out Bumper bumper) || other.TryGetComponent<VehicleCatchZone>(out VehicleCatchZone zone))
            _canTrigger = false;
        
        if(_canTrigger)
             ReflectDirection(other);
    }

    private void Move()
    {
        _rigidbody.velocity = Vector3.zero;

        if(_moveDirection == Vector3.zero)
            _moveDirection = GetRandomDirection();

        transform.rotation = Quaternion.LookRotation(_moveDirection,Vector3.up);
        
        _rigidbody.AddForce(_moveDirection.normalized * _speed, ForceMode.Impulse);
    }

    private void ReflectDirection(Collider otherCollider)
    {
        StopCoroutine(_changeDirectionCoroutine);

        Vector3 position = transform.position;
        
        Vector3 normal = otherCollider.ClosestPointOnBounds(position) - position;
        
        Vector3 newDirection = new Vector3(-normal.x, 0 , -normal.z).normalized;

        Quaternion deflection = Quaternion.Euler(0,Random.Range(0, _maxDeflectAngle), 0);

        _moveDirection =  deflection * newDirection;
        
        Move();

        _changeDirectionCoroutine = StartCoroutine(ChangeDirectionWithInterval());
    }
    
    private Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)).normalized;
    }

    private IEnumerator ChangeDirectionWithInterval()
    {
        while (true)
        {
            yield return new WaitForSeconds(_changeDirectionInterval);
            
            _moveDirection = GetRandomDirection();

            Move();
        }
    }
}
