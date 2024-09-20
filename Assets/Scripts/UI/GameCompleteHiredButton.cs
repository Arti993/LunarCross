using UnityEngine;

public class GameCompleteHiredButton : MonoBehaviour
{
    private const string GameIsComplete = nameof(GameIsComplete);
    
    private void Start()
    {
        if (PlayerPrefs.HasKey(GameIsComplete))
            Destroy(gameObject);
    }
}
