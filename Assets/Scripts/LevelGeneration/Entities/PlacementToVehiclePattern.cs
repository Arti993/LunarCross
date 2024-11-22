using System;
using LevelGeneration.Entities.EntityStateMachine;
using UnityEngine;
using Vehicle;
using Vehicle.BindPoints;
using Vehicle.ReactZones;

namespace LevelGeneration.Entities
{
    public class PlacementToVehiclePattern : IPlaceableToVehicle
    {
        private const float SearchRadius = 1;

        private EntityBehaviour _entity;
        private VehicleCatchBehaviour _vehicleCatchBehaviour;
        private CatchZoneChecker _catchZoneChecker;

        public PlacementToVehiclePattern(EntityBehaviour entity)
        {
            _entity = entity;
            _catchZoneChecker = new CatchZoneChecker(entity, SearchRadius);
        }

        public bool TryPlaceToVehicle()
        {
            _ = CheckEntityPlacement(out BindPoint bindPoint);

            if (bindPoint == null)
            {
                if (TryPlaceOutsideVehicle())
                {
                    _entity.SwitchState<OutsideVehicleAttachState>();
                    return true;
                }
                else
                {
                    _entity.SwitchState<KnockedState>();
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
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void UnplaceFromVehicle()
        {
            if (_entity == null)
                throw new InvalidOperationException();

            _ = CheckEntityPlacement(out BindPoint bindPoint);

            if (bindPoint == null)
                throw new InvalidOperationException();

            bindPoint.Exempt();

            _entity.transform.rotation = Quaternion.LookRotation(-Vector3.up);

            _entity.SwitchState<EjectedFromVehicleState>();
        }

        private bool CheckEntityPlacement(out BindPoint bindPoint)
        {
            bindPoint = _entity.GetComponentInParent<BindPoint>();

            return bindPoint is not null;
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
}
