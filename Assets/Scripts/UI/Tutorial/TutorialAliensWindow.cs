public class TutorialAliensWindow : TutorialWindow
{
    private CatchZoneViewer _catchZoneViewer;
    
    protected override void Awake()
    {
        _catchZoneViewer = GetCatchZoneViewer();
        
        base.Awake();
    }

    public override void PanelIntro()
    {
        _catchZoneViewer.ShowDangerZones();
        
        base.PanelIntro();
    }

    public override void PanelOutro()
    {
        _catchZoneViewer.StopShowDangerZones();
        
        base.PanelOutro();
    }
}
