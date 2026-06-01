using System.Collections.Generic;

using System.Security.Cryptography;

using UnityEngine;

public class UIManager : ModuleSingleton<UIManager>

{
    private static Dictionary<int, BasePanel> SingletonPanels = new Dictionary<int, BasePanel>();
    private static Dictionary<int, List<BasePanel>> PoolPanels = new Dictionary<int, List<BasePanel>>();

    public void ShowPanel<TPanel>() where TPanel : BasePanel
    {
        int key = typeof(TPanel).GetHashCode();
        if (SingletonPanels.ContainsKey(key) == false)
        {
            return;
        }

        SingletonPanels[key].Show();
    }
    
    public void HidePanel<TPanel>() where TPanel : BasePanel
    {
        int key = typeof(TPanel).GetHashCode();
        if (SingletonPanels.ContainsKey(key) == false)
        {
            return;
        }

        SingletonPanels[key].Hide();
    }

    /// <summary>
    /// panel on/off switch
    /// </summary>
    public void SwitchPanel<TPanel>() where TPanel : BasePanel
    {
        int key = typeof(TPanel).GetHashCode();
        if (SingletonPanels.ContainsKey(key) == false)
        {
            Debug.Log("panel is not existed");
            return;
        }

        if (SingletonPanels[key].isActiveAndEnabled == false)
        {
            SingletonPanels[key].Show();

            Debug.Log($"hashcode:{key},has showed up");
        }
        else
        {
            SingletonPanels[key].Hide();

            Debug.Log($"hashcode:{key},has hided");
        }
    }

    //register panel
    public void RegisterSingletonPanel<TPanel>(BasePanel panel) where TPanel : BasePanel
    {
        System.Type type = typeof(TPanel);

        RegisterSingletonPanel(type, panel);
    }


    public void RegisterSingletonPanel(System.Type type, BasePanel panel)
    {
        int key = type.GetHashCode();
        if (SingletonPanels.ContainsKey(key) == false)
        {
            SingletonPanels.Add(key, panel);

            Debug.Log($"hashcode: {key},panel: {panel.name},has been added to register");
        }
    }
}
