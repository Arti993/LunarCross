using Ami.BroAudio;
using UnityEngine;

public class HumanoidKnockedState : KnockedState
{
    private readonly Ragdoll RagdollBody;
    private Rigidbody SpineRigidbody;
    
    public HumanoidKnockedState(IEntityStateSwitcher stateSwitcher, Rigidbody rigidbody, Ragdoll ragdollBody) : base(stateSwitcher, rigidbody)
    {
        RagdollBody = ragdollBody;
    }

    public override void Move()
    {
        RagdollBody.TurnOn();

        SpineRigidbody = RagdollBody.GetSpineRigidbody();

        SpineRigidbody.velocity = MovementDirection * MovementSpeed;
        SpineRigidbody.angularVelocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1, 1f), Random.Range(-1f, 1f)).normalized;
        
        SoundID knock = DIServicesContainer.Instance.GetService<IAudioPlayback>().SoundsContainer.Knock;
        
        DIServicesContainer.Instance.GetService<IAudioPlayback>().PlaySound(knock);
    }
}
