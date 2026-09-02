using UnityEngine;

public class GamePanel : BasePanel
{
    public override void InitContent()
    {
        base.InitContent();
        UIManager.Instance.RegisterPanel<GamePanel>(this);
    }

    protected override void Awake()
    {
        HideInit();
    }
    
}