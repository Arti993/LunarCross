using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBorderChecker : MonoBehaviour
{
    private const int TutorialSceneIndex = 4;
    
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

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex == TutorialSceneIndex)
        {
            TimePauser timePauser = new TimePauser();
            
            AllServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(sceneIndex);

            StartCoroutine(timePauser.Pause());
        }
        else
        {
            GameObject uiRoot = AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetUIRoot();
                
            AllServicesContainer.Instance.GetService<IUiWindowFactory>().GetLevelFailedWindow(uiRoot);
        }
    }
}
