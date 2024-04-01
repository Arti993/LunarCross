using System;
using UnityEngine;

public class PlacementToVehiclePattern : IPlaceableToVehicle
{
    private EntityBehaviour _entity;
    private float _searchRadius = 1;
    private VehicleCatchBehaviour _vehicleCatchBehaviour;
    private CatchZoneChecker _catchZoneChecker;

    public PlacementToVehiclePattern(EntityBehaviour entity)
    {
        _entity = entity;
        _catchZoneChecker = new CatchZoneChecker(entity, _searchRadius);
    }

    public bool TryPlaceToVehicle()
    {
        CheckEntityPlacement(out BindPoint bindPoint);

        if(bindPoint == null)
        {
            if (TryPlaceOutsideVehicle())
            {
                _entity.SwitchState<OutsideVehicleAttachState>();
                return true;
            }
            else
            {
                _entity.SwitchState<KnockedByVehicleState>();
                return false;
            }
        }
        else if (bindPoint is OutsideBindPoint)
        {
            if (TryPlaceInsideVehicle())
            {
                _entity.SwitchState<InsideVehicleAttachState>();
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }

    public void UnplaceFromVehicle()
    {
        if (_entity == null)
            throw new InvalidOperationException();

        CheckEntityPlacement(out BindPoint bindPoint);

        if (bindPoint == null)
            throw new InvalidOperationException();

        bindPoint.Exempt();

        _entity.transform.rotation = Quaternion.LookRotation(-Vector3.up);

        _entity.SwitchState<EjectedFromVehicleState>();
    }

    private bool CheckEntityPlacement(out BindPoint bindPoint)
    {
        bindPoint = _entity.GetComponentInParent<BindPoint>();
        
        return bindPoint != null;
    }

    private bool TryPlaceOutsideVehicle()
    {
        bool isPlacedOutsideVehicle = false;

        if (_catchZoneChecker.CheckEnteringCatchZone(out VehicleCatchZone catchZone) == false)
            return false;

        _vehicleCatchBehaviour = catchZone.GetComponentInParent<VehicleCatchBehaviour>();

        if (_vehicleCatchBehaviour == null)
            throw new InvalidOperationException();

        if (catchZone is VehicleLeftCatchZone)
        {
            if (_vehicleCatchBehaviour.TryFillLeftSideBindPoint(_entity))
                isPlacedOutsideVehicle = true;

            if (isPlacedOutsideVehicle == false && _vehicleCatchBehaviour.TryFillRightSideBindPoint(_entity))
                isPlacedOutsideVehicle = true;
        }

        if (catchZone is VehicleRightCatchZone)
        {
            if (_vehicleCatchBehaviour.TryFillRightSideBindPoint(_entity))
                isPlacedOutsideVehicle = true;

            if (isPlacedOutsideVehicle == false && _vehicleCatchBehaviour.TryFillLeftSideBindPoint(_entity))
                isPlacedOutsideVehicle = true;
        }

        return isPlacedOutsideVehicle;
    }

    private bool TryPlaceInsideVehicle()
    {
        if (_vehicleCatchBehaviour == null)
            throw new InvalidOperationException();

        return _vehicleCatchBehaviour.TryFillInsideBindPoint(_entity);
    }
}
