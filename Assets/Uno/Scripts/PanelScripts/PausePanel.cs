using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : BasePanel
{
    public override void InitContent()
    {
        Button resumeBtn = transform.Find("ResumeBtn").GetComponent<Button>();
        Button restartBtn = transform.Find("RestartBtn").GetComponent<Button>();
        Button settingBtn = transform.Find("SettingBtn").GetComponent<Button>();
        Button exitBtn = transform.Find("ExitBtn").GetComponent<Button>();
        resumeBtn.onClick.AddListener(onResumeBtnClick);
        restartBtn.onClick.AddListener(onRestartBtnClick);
        settingBtn.onClick.AddListener(onSettingBtnClick);
        exitBtn.onClick.AddListener(onExitBtnClick);
    }

    protected override void Awake()
    {
        HideInit();
    }
    
    void onResumeBtnClick()
    {
        EventManager.Instance.TriggerEvent<ResumeGameEvent>();
        UIManager.Instance.HidePanel<PausePanel>();
    }
    
    void onRestartBtnClick()
    {
        // EventManager.Instance.TriggerEvent<RestartGameEvent>();
        Hide();
    }
    
    void onSettingBtnClick()
    {
        Hide();
        UIManager.Instance.ShowPanel<SettingsPanel>();
    }
    
    void onExitBtnClick()
    {
        // EventManager.Instance.TriggerEvent<ExitGameEvent>();
        Hide();
    }
}
