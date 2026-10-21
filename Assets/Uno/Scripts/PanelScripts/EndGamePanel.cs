using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndGamePanel : BasePanel
{

    TextMeshProUGUI title;
    TextMeshProUGUI content;
    Button closeBtn;
    
    public override void InitContent()
    {
        title = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        content = transform.Find("Content").GetComponent<TextMeshProUGUI>();
        closeBtn = transform.Find("CloseBtn").GetComponent<Button>();
        closeBtn.onClick.AddListener(OnCloseBtnClick);
    }

    protected override void Awake()
    {
        HideInit();
    }
    
    public void ShowWinner(int winnerCode, bool isTimeUp, bool isSelf)
    {
        // TODO: show winner panel
        title.text = isTimeUp ? "Time's Up!" : "Game Over";
        content.text = isTimeUp ? "Time's Up!" : (isSelf ? "You win!" : "Winner: " + winnerCode);
        Show();
        Debug.Log($"ShowWinner: {winnerCode}, {isTimeUp}, {isSelf}");
        Debug.Log($"Title: {title.text}");
        Debug.Log($"Content: {content.text}");
    }

    private void OnCloseBtnClick()
    {
        Hide();
    }
}
