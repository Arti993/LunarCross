using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(NPCMovement))]
public abstract class NpcBehaviour : EntityBehaviour
{
    protected Rigidbody Rigidbody;
    protected Collider Collider;
    protected Animator Animator;
    protected NPCMovement NpcMovement;
    protected Ragdoll RagdollFly;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
        Animator = GetComponent<Animator>();
        NpcMovement = GetComponent<NPCMovement>();
        RagdollFly = GetComponentInChildren<Ragdoll>();
    }
}

