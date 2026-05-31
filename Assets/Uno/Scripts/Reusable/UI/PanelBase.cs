using UnityEngine;

public class PanelBase : MonoBehaviour

{
    /// <summary>
    /// initialize and show panel
    /// </summary>
    public void Init()
    {
        InitContent();

    }
    /// <summary>
    /// initialization content, override it yourself
    /// </summary>
    public virtual void InitContent()
    {
    }
    /// <summary>
    /// initialize and hide panel
    /// </summary>
    public void HideInit()
    {
        InitContent();

        Hide();
    }

    public virtual void Show() { gameObject.SetActive(true); }
    public virtual void Hide() { gameObject.SetActive(false); }
    public virtual void OnShow() { } // 显示时调用
    public virtual void OnHide() { } // 隐藏时调用
    
    
    //if use hide init, override this method
    protected virtual void Awake()
    {
        Init();
    }
}
