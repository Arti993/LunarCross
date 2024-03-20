using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private int _collectedBonusesCount;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<LevelBorder>(out LevelBorder levelBorder))
        {
            if (other.attachedRigidbody != null)
            {
                //создание эффекта взрыва
            }
            
            RestartLevel();
        }
    }

    private void RestartLevel()
    {
        UiWindowFactory uiFactory = AllServicesContainer.Instance.GetService<UiWindowFactory>();

        uiFactory.GetLevelFailedWindow();
    }
}
