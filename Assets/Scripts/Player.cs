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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
