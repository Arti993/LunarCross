using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VehicleSpeedLimit : MonoBehaviour
{
    private const float StartSpeed = 2;
    private const float MaxSpeed = 3.5f;
    private const float TrackingPeriod = 0.08f;
    private const float MinRequiredDistancePerTrackingPeriod = 0.01f;
    
    [SerializeField] private Transform _blowUpPoint;
    
    private Rigidbody _rigidbody;
    private Transform _transform;
    private float _timer;
    private float _distanceTraveled;
    private float _periodStartPositionZ;
    private bool _isMinSpeedViolation;

    private void Start()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.velocity = new Vector3(0, 0, StartSpeed);
        _periodStartPositionZ = _transform.position.z;
    }

    private void FixedUpdate()
    {
        LimitSpeed();
        
        TrackMinimalMovement();
    }

    private void LimitSpeed()
    {
        if (_rigidbody.velocity.z > MaxSpeed)
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, MaxSpeed);
    }

    private void TrackMinimalMovement()
    {
        _timer += Time.deltaTime;
        _distanceTraveled = _transform.position.z - _periodStartPositionZ;
        Debug.Log(_distanceTraveled);

        if (_timer > TrackingPeriod)
        {
            if (_distanceTraveled < MinRequiredDistancePerTrackingPeriod)
            {
                HandleSpeedViolation();
            }
            
            _timer = 0;
            _periodStartPositionZ = _transform.position.z;
        }
    }

    private void HandleSpeedViolation()
    {
        if (_isMinSpeedViolation == false)
        {
            AllServicesContainer.Instance.GetService<IParticleSystemFactory>().GetExplosionEffect(_blowUpPoint.position);

            GameObject uiRoot = AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();
                
            AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetLevelFailedWindow(uiRoot);

            _isMinSpeedViolation = true;
        }
    }
}
