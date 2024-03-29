using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GravityRay : MonoBehaviour
{
    [SerializeField] private Transform _evacuationPoint;

    private LevelCompleteWindow _levelCompleteWindow;
    private float _attractionTime = 2;
    private VehicleCatchBehaviour _vehicle;
    private IReadOnlyList<BindPoint> _vehicleBindPoints;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<VehicleCatchBehaviour>(out VehicleCatchBehaviour vehicle))   //можно переделать под интерфейс
        {
            _vehicle = vehicle;
            _vehicle.GetComponent<Rigidbody>().velocity = Vector3.zero;
            _vehicle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            _vehicle.GetComponent<Rigidbody>().useGravity = false;

            _vehicleBindPoints = _vehicle.GetComponent<VehicleCatchBehaviour>().GetEntitiesBindPoints();
            
            StartCoroutine(MoveVehicleToCenter(_evacuationPoint.position, _attractionTime));
        }
    }

    private IEnumerator MoveVehicleToCenter(Vector3 target, float duration)
    {
        float time = 0;
        Vector3 startPosition = _vehicle.transform.position;

        while (time < duration)
        {
            _vehicle.transform.position = Vector3.Lerp(startPosition, target, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        ShowLevelCompleteWindow();

        StartCoroutine(EjectEntitiesByGravityRay());
    }

    private void ShowLevelCompleteWindow()
    {
        GameObject uiRoot = AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();
        
        GameObject window = AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetLevelCompleteWindow(uiRoot);

        if (window.TryGetComponent(out LevelCompleteWindow levelCompleteWindow))
            _levelCompleteWindow = levelCompleteWindow;
    }
    
    private IEnumerator EjectEntitiesByGravityRay()
    {
        foreach (var bindPoint in _vehicleBindPoints)
        {
            if (bindPoint.IsFree == false)
                yield return StartCoroutine(MoveEntityToGravityRay(bindPoint));
        }
        
        _levelCompleteWindow.EvaluatePassage();
    }
    
    private IEnumerator MoveEntityToGravityRay(BindPoint bindPoint)
    {
        Vector3 entityPosition = bindPoint.BindedEntity.transform.position;
        
        AllServicesContainer.Instance.GetService<IParticleSystemFactory>()
            .GetRayPullingEffect(entityPosition);
        
        _levelCompleteWindow.CollectPoint();

        bindPoint.BindedEntity.SwitchState<RisingByGravityRayState>();
        bindPoint.Exempt();
        
        yield return new WaitForSeconds(0.5f);
    }
}
