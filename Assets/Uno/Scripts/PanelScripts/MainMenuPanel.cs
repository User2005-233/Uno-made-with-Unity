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
        UIManager.Instance.ShowPanel<NetworkPanel>();
        Hide();
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    private void OnAiGameButtonClicked()
    {
        UIManager.Instance.ShowPanel<SinglePlayerSettingPanel>();
        Hide();
    }

    private void OnSettingsButtonClicked()
    {
        UIManager.Instance.ShowPanel<SettingsPanel>();
        Hide();
    }
}
