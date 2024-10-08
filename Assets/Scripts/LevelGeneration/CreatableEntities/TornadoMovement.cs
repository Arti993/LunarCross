using UnityEngine;

public class TornadoMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 0.8f;
    [SerializeField] private float _distance = 2.5f;
    
    private Vector3 startPoint;
    private Vector3 endPoint;
    private float startTime;
    private float journeyLength;

    private void Start()
    {
        startPoint = transform.position;
        endPoint = startPoint + Vector3.right * _distance; 
        startTime = Time.time;
        journeyLength = Vector3.Distance(startPoint, endPoint);
    }

    private void Update()
    {
        var distanceCovered = (Time.time - startTime) * _speed;
        var fracJourney = distanceCovered / journeyLength;
        transform.position = Vector3.Lerp(startPoint, endPoint, Mathf.PingPong(fracJourney, 1));
    }
}
