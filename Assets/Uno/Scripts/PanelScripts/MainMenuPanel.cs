using System.Collections;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : BasePanel

{
    [SerializeField] private Button netGameBtn;
    [SerializeField] private Button quitBtn;
    [SerializeField] private Button aiGameBtn;
    [SerializeField] private Button settingBtn;
    
    public override void InitContent()
    {
        UIManager.Instance.RegisterPanel<MainMenuPanel>(this);
        netGameBtn = transform.Find("GameMode/NetGameBtn").GetComponent<Button>();
        quitBtn = transform.Find("GameMode/QuitBtn").GetComponent<Button>();
        aiGameBtn = transform.Find("GameMode/AiGameBtn").GetComponent<Button>();
        settingBtn = transform.Find("SettingBtn").GetComponent<Button>();
        netGameBtn.onClick.AddListener(OnNetGameButtonClicked);
        quitBtn.onClick.AddListener(OnQuitButtonClicked);
        aiGameBtn.onClick.AddListener(OnAiGameButtonClicked);
        settingBtn.onClick.AddListener(OnSettingsButtonClicked);
    }


    private void OnNetGameButtonClicked()
    {
        // TODO: Start net game
        UIManager.Instance.ShowPanel<NetworkPanel>();
        Hide();
    }

    private void OnQuitButtonClicked()
    {
        // TODO: Quit game
        // Application.Quit();
    }

    private void OnAiGameButtonClicked()
    {
        // TODO: Start AI game
        
    }

    private void OnSettingsButtonClicked()
    {
        // TODO: Open settings
        UIManager.Instance.ShowPanel<SettingsPanel>();
        Hide();
    }
}
