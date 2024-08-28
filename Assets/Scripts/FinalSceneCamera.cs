using UnityEngine;

public class FinalSceneCamera : MonoBehaviour
{
    [SerializeField] private Transform _rocket;
    [SerializeField] private GameObject _designObjectsContainer;

    private Transform _transform;
    private float _startRocketPositionY;
    private float _startCameraPositionY;
    private float _maxCameraOffset = 28;
    private float _maxRocketOffset = 55;

    private void Awake()
    {
        _transform = transform;
        _startRocketPositionY = _rocket.position.y;
        _startCameraPositionY = _transform.position.y;
    }
    
    private void FixedUpdate()
    {
        float rocketOffset = _rocket.position.y - _startRocketPositionY;

        if (rocketOffset < _maxCameraOffset)
        {
            Vector3 newCameraPosition = _transform.position;
            newCameraPosition.y = _startCameraPositionY + rocketOffset;
            _transform.position = newCameraPosition;
        }
        else if (rocketOffset > _maxRocketOffset)
        {
            Destroy(_rocket.gameObject);
            Destroy(_designObjectsContainer);
            
            //вызов UI
            
            Destroy(this);
        }
    }
}
