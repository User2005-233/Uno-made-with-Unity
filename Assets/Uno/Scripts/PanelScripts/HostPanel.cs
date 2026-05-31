
// Host panel for network game hosting
public class HostPanel : PanelBase
{
    public override void InitContent()
    {
        UIManager.Instance.RegisterSingletonPanel<HostPanel>(this);
        
    }

    protected override void Awake()
    {
        HideInit();
    }
    
}
