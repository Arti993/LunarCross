using System;
using System.Collections.Generic;
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
}
