using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class MainMenu : PanelBase

{
    public override void InitUtil()
    {
        UIManager.Instance.RegisterSingletonPanel<MainMenu>(this);
    }

    private void Start()
    {
        Init();
    }
}
