using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chunk : MonoBehaviour
{
    [SerializeField] private Transform _begin;
    [SerializeField] private Transform _end;
    [SerializeField] private Renderer _planeRenderer;
    public Transform Begin => _begin;

    public Transform End => _end;

    public Renderer SurfaceRenderer => _planeRenderer;
}
