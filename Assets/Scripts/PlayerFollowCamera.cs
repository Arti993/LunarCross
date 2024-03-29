using System;
using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    [SerializeField] private float _angle = 30;
    [SerializeField] private float _yPosition = 6;
    [SerializeField] private float _zOffset = -9;
    
    private Transform _transform;
    private Transform _playerTransform;

    private void Start()
    {
        _transform = transform;
        _transform.rotation = Quaternion.AngleAxis(_angle, Vector3.right) * _transform.rotation;
        
        if(_playerTransform == null)
            throw new InvalidOperationException();
    }

    private void Update()
    {
        _transform.position = new Vector3(_playerTransform .position.x, _yPosition, _playerTransform .position.z + _zOffset);
    }
    
    public void SetPlayerTransform(Transform playerTransform)
    {
        _playerTransform = playerTransform;
    }

}
