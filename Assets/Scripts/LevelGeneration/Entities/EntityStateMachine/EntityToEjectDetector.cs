using Infrastructure.Services.Factories.ParticleSystemFactory;
using LevelGeneration.Entities.EntityStateMachine.Alien;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vehicle.BindPoints;

namespace LevelGeneration.Entities.EntityStateMachine
{
    public class EntityToEjectDetector : MonoBehaviour
    {
        private bool _isActive;
        
        private IParticleSystemFactory _particleSystemFactory;
        
  
        private void Construct()
        {
            _particleSystemFactory = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IParticleSystemFactory>();
        }

        private void Awake()
        {
            Construct();
        }

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
        
        public void Disable()
        {
            _isActive = false;
        }

        private void EjectEntity(IPlaceableToVehicle entityToEject)
        {
            entityToEject?.UnplaceFromVehicle();
        }
        
        private void PlayEjectionEffect(BindPoint bindPoint)
        {
            if (TryGetComponent(out AlienBehaviour alienBehaviour))
                _particleSystemFactory.ShowAlienEjectEffect(bindPoint.transform.position);

            if (TryGetComponent(out TornadoMovement tornadoMovement))
                _particleSystemFactory.ShowTornadoEjectEffect(bindPoint.transform.position);
        }
    }
}
