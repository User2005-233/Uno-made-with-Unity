public interface IModule

{
    /// <summary>
    /// 模块创建时调用
    /// </summary>
    void OnCreate(System.Object createParam);
    /// <summary>
    /// 模块更新时调用
    /// </summary>
    void OnUpdate();
    /// <summary>
    /// 模块销毁时调用
    /// </summary>
    void OnDestroy();
    /// <summary>
    /// GUI绘制时调用
    /// </summary>
    void OnGUI();
}
