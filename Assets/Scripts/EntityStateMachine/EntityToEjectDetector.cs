using UnityEngine;

public class EntityToEjectDetector : MonoBehaviour, IEjectorFromVehicle
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BindPoint bindPoint) == false)
            return;

        if (bindPoint.IsFree)
            return;

        IPlaceableToVehicle entityToEject = bindPoint.GetComponentInChildren<IPlaceableToVehicle>();

        PlayEjectionEffect(bindPoint);
        
        EjectEntity(entityToEject);
    }

    public void EjectEntity(IPlaceableToVehicle entityToEject)
    {
        entityToEject?.UnplaceFromVehicle();
    }

    private void PlayEjectionEffect(BindPoint bindPoint)
    {
        if (TryGetComponent(out AlienBehaviour alienBehaviour))
            AllServicesContainer.Instance.GetService<IParticleSystemFactory>().GetAlienEjectEffect(bindPoint.transform.position);
        
        if (TryGetComponent(out TornadoMovement tornadoMovement))
            AllServicesContainer.Instance.GetService<IParticleSystemFactory>().GetTornadoEjectEffect(bindPoint.transform.position);
    }
}
