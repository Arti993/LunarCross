using System;
using System.Collections;
using System.Collections.Generic;
using Ami.BroAudio;
using UnityEngine;

public class GravityRay : MonoBehaviour
{
    private const float AttractionTime = 2;
    
    [SerializeField] private Transform _evacuationPoint;

    private Transform _vehicleTransform;
    private IReadOnlyList<BindPoint> _vehicleBindPoints;
    private LevelCompleteWindow _levelCompleteWindow;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out VehicleCatchBehaviour vehicle))
        {
            if (vehicle.TryGetComponent(out Rigidbody vehicleRigidbody) == false)
                throw new InvalidOperationException();

            vehicleRigidbody.velocity = Vector3.zero;
            vehicleRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            vehicleRigidbody.useGravity = false;

            _vehicleBindPoints = vehicle.GetEntitiesBindPoints();
            _vehicleTransform = vehicle.transform;
            
            if (vehicle.TryGetComponent(out VehicleSpeedLimit vehicleSpeedLimit) == false)
                throw new InvalidOperationException();

            vehicleSpeedLimit.enabled = false;
            
            if (vehicle.TryGetComponent(out VehicleRotationLimit vehicleRotationLimit) == false)
                throw new InvalidOperationException();

            vehicleRotationLimit.enabled = false;
            
            SoundID raySound = DIServicesContainer.Instance.GetService<IAudioPlayback>().SoundsContainer.Ray;
        
            DIServicesContainer.Instance.GetService<IAudioPlayback>().PlaySound(raySound);
            
            StartCoroutine(MoveVehicleToCenter(_evacuationPoint.position, AttractionTime));
        }
    }

    private IEnumerator MoveVehicleToCenter(Vector3 target, float duration)
    {
        float time = 0;
        Vector3 startPosition = _vehicleTransform.position;

        while (time < duration)
        {
            _vehicleTransform.position = Vector3.Lerp(startPosition, target, time / duration);
            time += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }

        ShowLevelCompleteWindow();

        StartCoroutine(EjectEntitiesByGravityRay());
    }

    private void ShowLevelCompleteWindow()
    {
        DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UIStateLevelComplete>();
        
        GameObject uiRoot = DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();

        GameObject window = DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetLevelCompleteWindow(uiRoot);

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

        DIServicesContainer.Instance.GetService<IParticleSystemFactory>()
            .GetGreenCollectEffect(entityPosition);

        _levelCompleteWindow.CollectPoint();

        bindPoint.BindedEntity.SwitchState<RisingByGravityRayState>();
        bindPoint.Exempt();

        yield return new WaitForSeconds(0.5f);
    }
}