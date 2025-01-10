using Vehicle;

namespace UI
{
    public class TutorialCollectingWindow : TutorialWindow
    {
        private CatchZoneViewer _catchZoneViewer;

        protected override void Awake()
        {
            base.Awake();
            
            _catchZoneViewer = GetCatchZoneViewer();
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
}
