public class TutorialAliensWindow : TutorialWindow
{
    private CatchZoneViewer _catchZoneViewer;
    
    protected override void Awake()
    {
        _catchZoneViewer = GetCatchZoneViewer();
        
        base.Awake();
    }

    protected override void PauseGame()
    {
        _catchZoneViewer.ShowDangerZones();
        
        base.PauseGame();
    }

    protected override void ResumeGame()
    {
        base.ResumeGame();
        
        _catchZoneViewer.StopShowDangerZones();
    }
}
