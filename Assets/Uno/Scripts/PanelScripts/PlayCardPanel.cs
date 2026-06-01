using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayCardPane : BasePanel
{

    Image playCardRegion;
    TextMeshProUGUI playCardText;

    string playWaitText = "PLAY";
    string notPlayableText = "NOT PLAYABLE";
    bool isPlayable = true;
    TickTimer hideTimer;
    Color onRegionColor = new Color(0f, 1f, 0f, 0.5f);
    Color offRegionColor = new Color(0f, 1f, 0f, 0.2f);
    Color failRegionColor = new Color(1f, 0f, 0f, 0.2f);

    public override void InitContent()
    {
        UIManager.Instance.RegisterSingletonPanel<PlayCardPane>(this);
        playCardRegion = transform.Find("PlayCardRegion").GetComponent<Image>();
        playCardRegion.color = offRegionColor;
        playCardText = playCardRegion.transform.Find("PlayHint").GetComponent<TextMeshProUGUI>();
        EventManager.Instance.AddActionListener<PlayCardFailAction>(OnPlayCardFail);
        hideTimer = new TickTimer(0.5f);
        hideTimer.OnTimerComplete += OnTimeUp;
    }

    protected override void OnShow()
    {
        base.OnShow();
        playCardText.text = playWaitText;
        playCardRegion.color = offRegionColor;
    }

    private void OnPlayCardFail()
    {
        playCardText.text = notPlayableText;
        playCardRegion.color = failRegionColor;
        isPlayable = false;
    }

    protected override void Update()
    {
        base.Update();
        if (!isPlayable)
        {
            hideTimer.Tick(Time.deltaTime);
        }
    }

    void OnTimeUp()
    {
        isPlayable = true;
        playCardText.text = playWaitText;
        hideTimer.Reset();
        Hide();
    }
    
    
}
