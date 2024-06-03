using UnityEngine;

public class CatchZoneViewer : MonoBehaviour
{
    [SerializeField] private Collider[] _catchZones;

    private Color _startColor;
    private bool _isShowing;

    public void Show()
    {
        _isShowing = true;

        foreach (var Collider in _catchZones)
        {
            Material colliderMaterial = Collider.GetComponent<Renderer>().material;

            _startColor = colliderMaterial.color;
            
            colliderMaterial.color = Color.green;
        }
    }

    public void StopShow()
    {
        if(_isShowing == false)
            return;
        
        foreach (var Collider in _catchZones)
        {
            Material colliderMaterial = Collider.GetComponent<Renderer>().material;

            colliderMaterial.color = _startColor;
        }

        _isShowing = false;
    }
}
