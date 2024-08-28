using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour
{
    [SerializeField] private GameObject _ladder;
    [SerializeField] private GameObject _SmokeBeforeTakeOff;
    [SerializeField] private Transform _bottomLadderPoint;
    [SerializeField] private Transform _middleLadderPoint;
    [SerializeField] private Transform _topLadderPoint;
    [SerializeField] private List<RunnerToRocket> _runnersToRocket;

    private Rigidbody _rigidbody;
    private float _speed = 6;
    private float _delay = 0.5f;
    
    public Transform BottomLadderPoint => _bottomLadderPoint;
    public Transform TopLadderPoint => _topLadderPoint;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void PlaceRunner(RunnerToRocket runner)
    {
        _runnersToRocket.Remove(runner);
        
        Destroy(runner.gameObject);

        if (_runnersToRocket.Count == 0)
            StartCoroutine(FlyAway());
    }

    private IEnumerator FlyAway()
    {
        yield return new WaitForSeconds(_delay);
        
        // DIServicesContainer.Instance.GetService<IParticleSystemFactory>()
        //     .GetGreenCollectEffect(_ladder.transform.position);
        
        Destroy(_ladder);
        
        yield return new WaitForSeconds(_delay);
        
        _rigidbody.AddForce(0f,_speed,0f, ForceMode.VelocityChange);
    }
}
