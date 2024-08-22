using UnityEngine;

public class ContinueButton : SimpleButton
{
    private const string FirstTimeLaunched = "FirstTimeLaunched";
    
    protected override void Start()
    {
        if (PlayerPrefs.HasKey(FirstTimeLaunched) == false)
            Destroy(gameObject);
        
        base.Start();
    }
}
