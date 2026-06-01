using Unity.VisualScripting;
using UnityEngine;

public class BasePanel : MonoBehaviour

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
    protected virtual void OnShow() { } // 显示时调用
    protected virtual void OnHide() { } // 隐藏时调用


    //if use hide init, override this method
    protected virtual void Awake()
    {
        Init();
    }
    
    protected virtual void OnEnable()
    {
        OnShow();
    }

    protected virtual void OnDisable()
    {
        OnHide();
    }
    
    protected virtual void Update()
    {
    }
}
