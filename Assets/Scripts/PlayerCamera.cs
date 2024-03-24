using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float _angle = 30;
    [SerializeField] private float _yPosition = 6;
    [SerializeField] private float _zOffset = -9;
    [SerializeField] private GameplaySceneBootstrap _bootstrap;

    private Transform _transform;
    private Transform _roverTransform;

    private void Start()
    {
        _transform = transform;
        _transform.rotation = Quaternion.AngleAxis(_angle, Vector3.right) * _transform.rotation;
        _roverTransform = _bootstrap.Player.transform;
    }

    private void Update()
    {
        _transform.position = new Vector3(_roverTransform.position.x, _yPosition, _roverTransform.position.z + _zOffset);
    }
}
