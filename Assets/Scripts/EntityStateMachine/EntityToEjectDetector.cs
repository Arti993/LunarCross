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

        AllServicesContainer.Instance.GetService<IParticleSystemFactory>()
            .GetEjectEffect(bindPoint.transform.position);
        
        EjectEntity(entityToEject);
    }

    public void EjectEntity(IPlaceableToVehicle entityToEject)
    {
        
        
        entityToEject?.UnplaceFromVehicle();
    }
}
