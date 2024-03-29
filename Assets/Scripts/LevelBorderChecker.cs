using UnityEngine;

public class LevelBorderChecker : MonoBehaviour
{
    private bool _isFirstContactSucceed;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out LevelBorder levelBorder) == false)
            return;

        if (_isFirstContactSucceed) 
            return;
        
        _isFirstContactSucceed = true;
                
        Vector3 intersectionPoint = transform.GetComponent<Collider>().ClosestPointOnBounds(other.transform.position);
            
        AllServicesContainer.Instance.GetService<IParticleSystemFactory>().GetExplosionEffect(intersectionPoint);

        GameObject uiRoot = AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();
                
        AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetLevelFailedWindow(uiRoot);
    }
}
