public class MenuWindow : UIWindow
{
    protected virtual void Awake()
    {
        PanelIntro();
    }

    public void Exit()
    {
        PanelOutro();
    }
}
