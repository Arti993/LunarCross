using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _collectedBonusesCount;
    public event Action LevelFailed;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<LevelBorder>(out LevelBorder levelBorder))
        {
            LevelFailed?.Invoke();
        }
    }
}
