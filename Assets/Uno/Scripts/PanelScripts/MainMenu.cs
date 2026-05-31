using System.Collections;

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class MainMenu : PanelBase

{
    [SerializeField] private Button netGameBtn;
    [SerializeField] private Button quitBtn;
    [SerializeField] private Button aiGameBtn;
    [SerializeField] private Button settingsBtn;
    
    public override void InitContent()
    {
        UIManager.Instance.RegisterSingletonPanel<MainMenu>(this);
        netGameBtn.onClick.AddListener(OnNetGameButtonClicked);
        quitBtn.onClick.AddListener(OnQuitButtonClicked);
        aiGameBtn.onClick.AddListener(OnAiGameButtonClicked);
        settingsBtn.onClick.AddListener(OnSettingsButtonClicked);
        
    }


    private void OnNetGameButtonClicked()
    {
        // TODO: Start net game
    }

    private void OnQuitButtonClicked()
    {
        // TODO: Quit game
    }

    private void OnAiGameButtonClicked()
    {
        // TODO: Start AI game
    }

    private void OnSettingsButtonClicked()
    {
        // TODO: Open settings
    }
}
