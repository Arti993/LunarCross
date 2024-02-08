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

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        GetRandomDirection();
        
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
        ReflectDirection();
    }

    private void Move()
    {
        _rigidbody.velocity = Vector3.zero;
        
        transform.rotation = Quaternion.LookRotation(_moveDirection);
        
        _rigidbody.AddForce(_moveDirection.normalized * _speed, ForceMode.VelocityChange);
    }

    private void ReflectDirection()
    {
        StopCoroutine(_changeDirectionCoroutine);

        Vector3 position = transform.position;
        
        Vector3 reflectionNormal = new Vector3(Mathf.Sign(position.x), 0, Mathf.Sign(position.z)).normalized;
        
        float reflectAngleRad = Vector3.SignedAngle(-reflectionNormal, Vector3.forward, Vector3.up) * Mathf.Deg2Rad;
        
        _moveDirection = new Vector3(Mathf.Cos(reflectAngleRad), 0f, Mathf.Sin(reflectAngleRad));
        
        Move();

        _changeDirectionCoroutine = StartCoroutine(ChangeDirectionWithInterval());
    }
    
    private Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
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
