public class TutorialAliensWindow : TutorialWindow
{
    private CatchZoneViewer _catchZoneViewer;
    
    protected override void Awake()
    {
        _catchZoneViewer = GetCatchZoneViewer();
        
        base.Awake();
    }

    public override void Open()
    {
        _catchZoneViewer.ShowDangerZones();
        
        base.Open();
    }

    public override void Close()
    {
        base.Close();
        
        _catchZoneViewer.StopShowDangerZones();
    }
}
