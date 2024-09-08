using UnityEngine;

public class FirstLaunchHiredButton : SimpleButton
{
    private const string NotFirstGameLaunch = "NotFirstGameLaunch";
    
    protected override void Start()
    {
        if (PlayerPrefs.HasKey(NotFirstGameLaunch) == false)
            Destroy(gameObject);
        
        base.Start();
    }
}
