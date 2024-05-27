using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] private Transform _begin;
    [SerializeField] private Transform _end;
    [SerializeField] private Renderer _planeRenderer;

    private Material _surfaceMaterial;
    private Material _stonesMaterial;
    private Material _mountainsMaterial;
    
    public Transform Begin => _begin;

    public Transform End => _end;

    public Renderer SurfaceRenderer => _planeRenderer;

    public void SetMaterials(Material surfaceMaterial, Material stonesMaterial, Material mountainsMaterial)
    {
        _surfaceMaterial = surfaceMaterial;
        _stonesMaterial = stonesMaterial;
        _mountainsMaterial = mountainsMaterial;

        AssignMaterials();
    }

    private void AssignMaterials()
    {
        _planeRenderer.material = _surfaceMaterial;
        
        RockElement[] rockElements = this.GetComponentsInChildren<RockElement>();

        foreach (var rockElement in rockElements)
        {
            rockElement.GetComponent<Renderer>().material = _stonesMaterial;
        }
        
        Mountain[] mountains = this.GetComponentsInChildren<Mountain>();
     
        foreach (var mountain in mountains)
        {
            mountain.GetComponent<Renderer>().material = _mountainsMaterial;
        }    
            
    }
}
