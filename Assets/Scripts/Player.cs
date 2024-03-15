using System;
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

            }

            RestartLevel();
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
