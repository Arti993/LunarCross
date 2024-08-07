using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _hitDeviationСoefficient = 0.25f;
    [SerializeField] private float _afterHitMovementSpeed = 10;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<VehicleCatchBehaviour>())
            HitByVehicle();
    }

    private void HitByVehicle()
    {
        if (TryGetComponent(out Movement movement))
            movement.enabled = false;

        _rigidbody.isKinematic = false;

        Vector3 MovementDirection =
            new Vector3(Random.Range(-_hitDeviationСoefficient, _hitDeviationСoefficient), 1, 0f);

        _rigidbody.velocity = MovementDirection * _afterHitMovementSpeed;

        _rigidbody.angularVelocity =
            new Vector3(Random.Range(-1f, 1f), Random.Range(-1, 1f), Random.Range(-1f, 1f)).normalized;

        AllServicesContainer.Instance.GetService<IAudioPlayback>().PlayKnockSound();
    }
}