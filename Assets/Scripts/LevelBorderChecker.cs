using UnityEngine;
using UnityEngine.SceneManagement;

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
            
        DIServicesContainer.Instance.GetService<IParticleSystemFactory>().GetExplosionEffect(intersectionPoint);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == (int)SceneIndex.Tutorial)
        {
            TimePauserWithDelay timePauserWithDelay = new TimePauserWithDelay();
            
            DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateNoWindow>();
            
            DIServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene((int)SceneIndex.Tutorial);

            StartCoroutine(timePauserWithDelay.Pause());
        }
        else
        {
            DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateLevelFailed>();
        }
    }
}
