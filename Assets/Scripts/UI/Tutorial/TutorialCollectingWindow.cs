public class TutorialCollectingWindow : TutorialWindow
{
    private CatchZoneViewer _catchZoneViewer;
    
    protected override void Awake()
    {
        _catchZoneViewer = GetCatchZoneViewer();
        
        base.Awake();
    }

    public override void PanelIntro()
    {
        _catchZoneViewer.ShowCatchZones();
        
        base.PanelIntro();
    }

    public override void PanelOutro()
    {
        _catchZoneViewer.StopShowCatchZones();
        
        base.PanelOutro();
    }
}
