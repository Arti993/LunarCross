using System.Collections;
using System.Collections.Generic;
using Ami.BroAudio;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour
{
    private const float Speed = 5.3f;
    private const float Delay = 0.5f;
    
    [SerializeField] private GameObject _ladder;
    [SerializeField] private GameObject _smokeBeforeTakeOff;
    [SerializeField] private Transform _bottomLadderPoint;
    [SerializeField] private Transform _middleLadderPoint;
    [SerializeField] private Transform _topLadderPoint;
    [SerializeField] private List<RunnerToRocket> _runnersToRocket;

    private Rigidbody _rigidbody;
    
    public Transform BottomLadderPoint => _bottomLadderPoint;
    public Transform TopLadderPoint => _topLadderPoint;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        SoundID rocketEngine = DIServicesContainer.Instance.GetService<IAudioPlayback>().SoundsContainer.RocketEngine;
        
        DIServicesContainer.Instance.GetService<IAudioPlayback>().PlaySound(rocketEngine);
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
        yield return new WaitForSeconds(Delay);
        
        DIServicesContainer.Instance.GetService<IParticleSystemFactory>()
             .GetGreenCollectEffect(_middleLadderPoint.position);
        
        Destroy(_ladder);
        
        yield return new WaitForSeconds(Delay);
        
        Destroy(_smokeBeforeTakeOff);
        
        _rigidbody.AddForce(0f,Speed,0f, ForceMode.VelocityChange);
        
        SoundID rocketTurbine = DIServicesContainer.Instance.GetService<IAudioPlayback>().SoundsContainer.RocketTurbine;
        
        DIServicesContainer.Instance.GetService<IAudioPlayback>().PlaySound(rocketTurbine);
    }
}
