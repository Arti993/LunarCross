using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

[RequireComponent(typeof(NavMeshSurface))]
public class NavMeshRebaker : MonoBehaviour
{
    private NavMeshSurface _navMeshSurface;

    private void Awake()
    {
        _navMeshSurface = GetComponent<NavMeshSurface>();
    }

    public void Rebake()
    {
        _navMeshSurface.UpdateNavMesh(_navMeshSurface.navMeshData);
    }
}
