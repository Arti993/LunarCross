using UnityEngine;

public class FirstLaunchHiredButton : MonoBehaviour
{
    private const string NotFirstGameLaunch = "NotFirstGameLaunch";
    
    private void Start()
    {
        if (PlayerPrefs.HasKey(NotFirstGameLaunch) == false)
            Destroy(gameObject);
    }
}
