using UnityEngine;

public class TornadoBehaviour : EntityBehaviour, IEjectorFromVehicle, IEntityStateSwitcher
{
    private IPlaceableToVehicle _entityToEject;

    private void Awake()
    {
        CurrentState = new NoActionState(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out BindPoint bindPoint))
        {
            if (bindPoint != null && bindPoint.IsFree == false)
            {
                _entityToEject = bindPoint.GetComponentInChildren<IPlaceableToVehicle>();

                EjectEntity();
            }
        }
    }
    public void EjectEntity()
    {
        if (_entityToEject != null)
            _entityToEject.UnplaceFromVehicle();
    }
}
