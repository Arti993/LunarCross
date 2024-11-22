using Infrastructure;
using Infrastructure.Services.Factories.ParticleSystemFactory;
using LevelGeneration.Entities.EntityStateMachine.Alien;
using UnityEngine;
using Vehicle.BindPoints;

namespace LevelGeneration.Entities.EntityStateMachine
{
    public class EntityToEjectDetector : MonoBehaviour, IEjectorFromVehicle
    {
        private bool _isActive;

        private void OnEnable()
        {
            _isActive = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_isActive == false)
                return;

            if (other.TryGetComponent(out BindPoint bindPoint) == false || bindPoint.IsFree)
                return;

            IPlaceableToVehicle entityToEject = bindPoint.GetComponentInChildren<IPlaceableToVehicle>();

            PlayEjectionEffect(bindPoint);

            EjectEntity(entityToEject);
        }

        public void EjectEntity(IPlaceableToVehicle entityToEject)
        {
            entityToEject?.UnplaceFromVehicle();
        }

        public void Disable()
        {
            _isActive = false;
        }

        private void PlayEjectionEffect(BindPoint bindPoint)
        {
            if (TryGetComponent(out AlienBehaviour alienBehaviour))
                DIServicesContainer.Instance.GetService<IParticleSystemFactory>()
                    .ShowAlienEjectEffect(bindPoint.transform.position);

            if (TryGetComponent(out TornadoMovement tornadoMovement))
                DIServicesContainer.Instance.GetService<IParticleSystemFactory>()
                    .ShowTornadoEjectEffect(bindPoint.transform.position);
        }
    }
}
