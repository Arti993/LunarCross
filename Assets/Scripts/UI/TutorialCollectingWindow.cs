using System;
using UnityEngine;

public class TutorialCollectingWindow : TutorialWindow
{
    private CatchZoneViewer _catchZoneViewer;
    
    protected override void Awake()
    {
        GameObject vehicle = AllServicesContainer.Instance.GetService<IGameplayFactory>().GetPlayerInstance();
        
        if(vehicle.TryGetComponent(out CatchZoneViewer catchZoneViewer) == false)
            throw new InvalidOperationException();

        _catchZoneViewer = catchZoneViewer;

        base.Awake();
    }

    protected override void PauseGame()
    {
        _catchZoneViewer.Show();
        
        base.PauseGame();
    }
    
    protected override void Continue()
    {
        _catchZoneViewer.StopShow();
        
        base.Continue();
    }
}
