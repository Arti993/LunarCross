using UnityEngine;

namespace LevelGeneration.Entities
{
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

            DisableFunctioning();
        }

        public void TurnOn()
        {
            _animator.enabled = false;

            _spineRigidbody.isKinematic = false;

            EnableFunctioning();
        }

        public void TurnOff()
        {
            _spineRigidbody.velocity = Vector3.zero;
            _spineRigidbody.angularVelocity = Vector3.zero;

            _spineRigidbody.isKinematic = true;

            DisableFunctioning();

            _animator.enabled = true;
        }

        public Rigidbody GetSpineRigidbody()
        {
            return _spineRigidbody;
        }

        private void EnableFunctioning()
        {
            foreach (Rigidbody rigidbody in _ragdollRigidbodies)
                rigidbody.isKinematic = false;

            foreach (Collider collider in _ragdollColliders)
                collider.enabled = true;
        }

        private void DisableFunctioning()
        {
            foreach (Rigidbody rigidbody in _ragdollRigidbodies)
                rigidbody.isKinematic = true;

            foreach (Collider collider in _ragdollColliders)
                collider.enabled = false;
        }
    }
}
