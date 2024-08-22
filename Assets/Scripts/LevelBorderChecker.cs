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

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        int tutorialSceneIndex = DIServicesContainer.Instance.GetService<IScenesLoader>().GetTutorialSceneIndex();

        if (sceneIndex == tutorialSceneIndex)
        {
            TimePauserWithDelay timePauserWithDelay = new TimePauserWithDelay();
            
            DIServicesContainer.Instance.GetService<IScenesLoader>().LoadTutorialScene();

            StartCoroutine(timePauserWithDelay.Pause());
        }
        else
        {
            GameObject uiRoot = DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();
                
            DIServicesContainer.Instance.GetService<IUiWindowFactory>().GetLevelFailedWindow(uiRoot);
        }
    }
}
