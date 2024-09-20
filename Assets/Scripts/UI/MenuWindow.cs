public class MenuWindow : UIWindow
{
    protected virtual void Awake()
    {
        PanelIntro();
    }

    public virtual void Exit()
    {
        PanelOutro();
    }
}
