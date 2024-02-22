using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class VehicleCatchBehaviour : MonoBehaviour
{
    [SerializeField] private List<LeftSideBindPoint> _leftSideBindPoints;
    [SerializeField] private List<RightSideBindPoint> _rightSideBindPoints;
    [SerializeField] private List<InsideBindPoint> _insideBindPoints;

    private Player _player;
    private List<BindPoint> _allBindPoints = new List<BindPoint>();

    private void Start()
    {
        PutAllPointsInOneList();
        
        _player = GetComponent<Player>();

    }

    public bool TryFillInsideBindPoint(EntityBehaviour entity)
    {
        bool isFillingSuccess = false;

        BindPoint previousBindPoint = entity.GetComponentInParent<BindPoint>();

        if (previousBindPoint is null)
            throw new InvalidOperationException();

        if(TryFillBindPoint(entity, _insideBindPoints))
        {
            isFillingSuccess = true;
            previousBindPoint.Exempt();
        }

        return isFillingSuccess;
    }

    public bool TryFillLeftSideBindPoint(EntityBehaviour entity)
    {
        return TryFillBindPoint(entity, _leftSideBindPoints);
    }

    public bool TryFillRightSideBindPoint(EntityBehaviour entity)
    {
        return TryFillBindPoint(entity, _rightSideBindPoints);
    }

    public bool TrySendEntitiesToGravityRay()
    {
        StartCoroutine(EjectEntitiesByGravityRay());

        return true; //потом сделать нормальное условие
    }

    private bool TryFillBindPoint(EntityBehaviour entity, IReadOnlyList<BindPoint> bindPoints)
    {
        bool isFillingSuccess = false;

        for (int i = 0; i < bindPoints.Count; i++)
        {
            if (bindPoints[i].IsFree)
            {
                bindPoints[i].Fill(entity);
                isFillingSuccess = true;
                break;
            }
        }

        return isFillingSuccess;
    }

    private void PutAllPointsInOneList()
    {
        foreach (var bindPoint in _leftSideBindPoints)
        {
           _allBindPoints.Add(bindPoint);
        }
        
        foreach (var bindPoint in _rightSideBindPoints)
        {
            _allBindPoints.Add(bindPoint);
        }
        
        foreach (var bindPoint in _insideBindPoints)
        {
            _allBindPoints.Add(bindPoint);
        }
    }

    private void OnLevelFailed()
    {
        foreach (var bindPoint in _allBindPoints)
        {
            bindPoint.Exempt();
        }
    }

    private IEnumerator EjectEntitiesByGravityRay()
    {
        foreach (var bindPoint in _allBindPoints)
        {
            if (bindPoint.IsFree == false)
                yield return StartCoroutine(MoveEntityToGravityRay(bindPoint));
        }
    }

    private IEnumerator MoveEntityToGravityRay(BindPoint bindPoint)
    {
        bindPoint.BindedEntity.SwitchState<RisingByGravityRayState>();
        bindPoint.Exempt();
                
        yield return new WaitForSeconds(0.5f);
    }
}
