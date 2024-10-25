using Ami.BroAudio;
using Ami.Extension;
using UnityEngine;

public class HumanoidKnockedState : KnockedState
{
    private readonly Collider _collider;
    private readonly Ragdoll _ragdollBody;
    private Rigidbody _spineRigidbody;
    
    public HumanoidKnockedState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody, Ragdoll ragdollBody, Collider collider) : base(stateSwitcher, rigidbody)
    {
        _ragdollBody = ragdollBody;
        _collider = collider;
    }

    public override void Start()
    {
        if (Rigidbody.gameObject.TryGetComponent(out EntityToEjectDetector ejector))
            ejector.Disable();
        
        base.Start();
    }

    public override void Move()
    {
        _ragdollBody.TurnOn();

        _spineRigidbody = _ragdollBody.GetSpineRigidbody();
 
        _spineRigidbody.velocity = MovementDirection * MovementSpeed;
        _spineRigidbody.angularVelocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1, 1f), Random.Range(-1f, 1f)).normalized;
        
        SoundID knock = DIServicesContainer.Instance.GetService<IAudioPlayback>().SoundsContainer.Knock;
        
        DIServicesContainer.Instance.GetService<IAudioPlayback>().PlaySound(knock);
    }

    public override void Stop()
    {
        _ragdollBody.TurnOff();

        base.Stop();
    }
}
