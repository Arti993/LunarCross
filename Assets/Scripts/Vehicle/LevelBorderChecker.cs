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
            
        DIServicesContainer.Instance.GetService<IParticleSystemFactory>().ShowExplosionEffect(intersectionPoint);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == (int)SceneIndex.Tutorial)
        {
            TimePauserWithDelay timePauserWithDelay = new TimePauserWithDelay();

            DIServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene((int)SceneIndex.Tutorial);

            _ = StartCoroutine(timePauserWithDelay.Pause());
        }
        else
        {
            DIServicesContainer.Instance.GetService<IUiStateMachine>().SetState<UiStateLevelFailed>();
        }
    }
}
