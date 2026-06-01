
// Host panel for network game hosting
public class HostPanel : BasePanel
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
