using Vehicle;

namespace UI
{
    public class TutorialAliensWindow : TutorialWindow
    {
        private CatchZoneViewer _catchZoneViewer;

        protected override void Awake()
        {
            base.Awake();
            
            _catchZoneViewer = GetCatchZoneViewer();
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
}
