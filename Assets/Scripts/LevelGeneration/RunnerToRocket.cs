using UnityEngine;

public class RunnerToRocket : MonoBehaviour
{
    private const float _speed = 0.03f;
    private const float _minDistance = 0.01f;
    
    [SerializeField] private Rocket _rocket;

    private Transform _transform;
    private Transform _firstPoint;
    private Transform _secondPoint;
    private Transform _target;
    private int _currentPointIndex;

    private void Start()
    {
        _firstPoint = _rocket.BottomLadderPoint;
        _secondPoint = _rocket.TopLadderPoint;
        _transform = transform;
        _target = _firstPoint;
        _currentPointIndex = 0;
    }

    private void Update()
    {
        _transform.position = Vector3.MoveTowards(_transform.position, _target.position, _speed);

        if (Vector3.Distance(_transform.position, _target.position) < _minDistance)
        {
            _currentPointIndex++;

            if (_currentPointIndex == 1)
            {
                _transform.rotation = Quaternion.LookRotation(_target.forward);
                _target = _secondPoint;
            }
            else if (_currentPointIndex > 1)
            {
                DIServicesContainer.Instance.GetService<IParticleSystemFactory>().ShowGreenCollectEffect(_transform.position);
                
                _rocket.PlaceRunner(this);
            }
        }
    }
}
