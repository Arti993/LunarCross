using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VehicleCatchBehaviour : MonoBehaviour
{
    [SerializeField] private List<LeftSideBindPoint> _leftSideBindPoints;
    [SerializeField] private List<RightSideBindPoint> _rightSideBindPoints;
    [SerializeField] private List<InsideBindPoint> _insideBindPoints;

    public bool TryFillInsideBindPoint(EntityBehaviour entity)
    {
        bool isFillingSuccess = false;

        BindPoint previousBindPoint = entity.GetComponentInParent<BindPoint>();

        if (previousBindPoint == null)
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
        StartCoroutine(EjectEntities());

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

    private IEnumerator EjectEntities()
    {
        foreach (var bindpoint in _leftSideBindPoints)
        {
            if (bindpoint.IsFree == false)
                yield return StartCoroutine(ExemptBindPoint(bindpoint));
        }
        
        foreach (var bindpoint in _rightSideBindPoints)
        {
            if (bindpoint.IsFree == false)
                yield return StartCoroutine(ExemptBindPoint(bindpoint));
        }
        
        foreach (var bindpoint in _insideBindPoints)
        {
            if (bindpoint.IsFree == false)
                yield return StartCoroutine(ExemptBindPoint(bindpoint));
        }
    }

    private IEnumerator ExemptBindPoint(BindPoint bindpoint)
    {
        bindpoint.BindedEntity.SwitchState<RisingByRayState>();
                
        yield return new WaitForSeconds(0.5f);
    }
}
