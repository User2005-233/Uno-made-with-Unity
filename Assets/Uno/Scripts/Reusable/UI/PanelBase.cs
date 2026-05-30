using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class PanelBase : MonoBehaviour

{
    public virtual void Init()
    {
        InitUtil();

    }   // 初始化并显示面板
    public virtual void InitUtil()
    {
    }

    public virtual void HideInit()
    {
        InitUtil();

        Hide();
    }

    public virtual void Show() { gameObject.SetActive(true); }
    public virtual void Hide() { gameObject.SetActive(false); }
    public virtual void OnShow() { } // 显示时调用
    public virtual void OnHide() { } // 隐藏时调用
}
