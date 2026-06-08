using TMPro;

using UnityEngine;

//done
public class HintMenu : BasePanel

{
    TextMeshProUGUI hintTitle;
    TextMeshProUGUI hintContent;
    TickTimer hideTimer;
    public override void InitContent()
    {
        UIManager.Instance.RegisterPanel<HintMenu>(this);
        hintTitle = transform.Find("Frame/Title").GetComponent<TextMeshProUGUI>();
        hintContent = transform.Find("Frame/Content").GetComponent<TextMeshProUGUI>();
        EventManager.Instance.AddListener<ShowHintEvent>(OnShowHint);
        hideTimer = new TickTimer(3f);
    }

    //receive show hint event message
    void OnShowHint(IEventMessage e)
    {
        if (e is ShowHintEvent showHintEvent)
        {
            ShowHint(showHintEvent.title, showHintEvent.content);
        }
    }
    public void ShowHint(string title, string content)
    {
        hideTimer.Reset();
        hideTimer.Start();
        hintTitle.text = title;
        hintContent.text = content;
    }

    protected override void Awake()
    {
        HideInit();
    }



    protected override void Update()
    {
        base.Update();

        if (!hideTimer.IsCompleted)
        {
            hideTimer.Tick(Time.deltaTime);
        }
        else
        {
            hideTimer.Reset();
            Hide();
        }
    }
}
