using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ragdoll : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _spineRigidbody;
    private Rigidbody[] _ragdollRigidbodies;
    private Collider[] _ragdollColliders;
    

    private void Awake()
    {
        _animator = GetComponentInParent<Animator>();
        _spineRigidbody = GetComponent<Rigidbody>();
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        _ragdollColliders = GetComponentsInChildren<Collider>();

        foreach (Rigidbody rigidbody in _ragdollRigidbodies)
            rigidbody.isKinematic = true;

        foreach (Collider collider in _ragdollColliders)
            collider.enabled = false;
    }

    public void TurnOn()
    {
        _animator.enabled = false;

        _spineRigidbody.isKinematic = false;

        foreach (Rigidbody rigidbody in _ragdollRigidbodies)
            rigidbody.isKinematic = false;

        foreach (Collider collider in _ragdollColliders)
            collider.enabled = true;
    }

    public void TurnOff()
    {
        _spineRigidbody.velocity = Vector3.zero;
        _spineRigidbody.angularVelocity = Vector3.zero;
        
        _spineRigidbody.isKinematic = true;
        
        foreach (Rigidbody rigidbody in _ragdollRigidbodies)
            rigidbody.isKinematic = true;

        foreach (Collider collider in _ragdollColliders)
            collider.enabled = false;
        
        _animator.enabled = true;
    }

    public Rigidbody GetSpineRigidbody()
    {
        return _spineRigidbody;
    }
}
