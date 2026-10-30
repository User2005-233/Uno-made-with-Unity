using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDPanel : BasePanel
{
    public override void InitContent()
    {
        TMP_Text gameCountDown = transform.Find("GameCountDown").GetComponent<TMP_Text>();
        Button pauseBtn = transform.Find("PauseBtn").GetComponent<Button>();
        pauseBtn.onClick.AddListener(onPauseBtnClick);
    }

    protected override void Awake()
    {
        HideInit();
    }
    
    void onPauseBtnClick()
    {
        EventManager.Instance.TriggerEvent<PauseGameEvent>();
        UIManager.Instance.ShowPanel<PausePanel>();
        Hide();
    }
}