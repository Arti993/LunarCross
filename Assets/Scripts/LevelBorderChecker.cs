using UnityEngine;

public class LevelBorderChecker : MonoBehaviour
{
    private bool _isFirstContactSucceed = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<LevelBorder>(out LevelBorder levelBorder))
        {
            if (_isFirstContactSucceed == false)
            {
                _isFirstContactSucceed = true;
                
                Vector3 intersectionPoint = transform.GetComponent<Collider>().ClosestPointOnBounds(other.transform.position);
            
                AllServicesContainer.Instance.GetService<IParticleSystemFactory>().GetExplosionEffect(intersectionPoint);
                
                AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetLevelFailedWindow();
            }
        }
    }
}
