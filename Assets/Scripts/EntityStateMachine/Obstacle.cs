using System;
using System.Collections;
using Ami.BroAudio;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Collider))]
public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _hitDeviationСoefficient = 0.25f;
    [SerializeField] private float _afterHitMovementSpeed = 10;

    private Rigidbody _rigidbody;
    private Collider _collider;
    private Movement _movement;
    private float _disableColliderTime = 0.7f;
    private bool _isCanCollise;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _movement = GetComponent<Movement>();
    }
    
    private void OnEnable()
    {
        _rigidbody.isKinematic = true;
        _movement.enabled = true;
        _isCanCollise = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isCanCollise && (other.GetComponent<Bumper>() || other.GetComponent<VehicleCatchZone>()))
        {
            _isCanCollise = false;
            
            StartCoroutine(PauseCollisions());

            HitByVehicle();
        }
    }

    private void HitByVehicle()
    {
        _movement.enabled = false;
        _rigidbody.isKinematic = false;

        Vector3 MovementDirection =
            new Vector3(Random.Range(-_hitDeviationСoefficient, _hitDeviationСoefficient), 1, 0f);

        _rigidbody.velocity = MovementDirection * _afterHitMovementSpeed;

        _rigidbody.angularVelocity =
            new Vector3(Random.Range(-1f, 1f), Random.Range(-1, 1f), Random.Range(-1f, 1f)).normalized;

        SoundID knock = DIServicesContainer.Instance.GetService<IAudioPlayback>().SoundsContainer.Knock;
        
        DIServicesContainer.Instance.GetService<IAudioPlayback>().PlaySound(knock);
    }

    private IEnumerator PauseCollisions()
    {
        _collider.enabled = false;
        
        yield return new WaitForSeconds(_disableColliderTime);
        
        _collider.enabled = true;
        _isCanCollise = true;
    }
}