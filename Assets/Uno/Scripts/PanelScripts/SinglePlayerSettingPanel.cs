using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SinglePlayerSettingPanel : BasePanel
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] Button tryStartGameBtn;
    string title = "提示";
    string content = "请输入3-6之间的数字";

    protected override void Awake()
    {
        HideInit();
    }

    public override void InitContent()
    {
        inputField = transform.Find("Number").GetComponent<TMP_InputField>();
        // Set the input field to only accept numbers and limit the input to 1 character
        inputField.contentType = TMP_InputField.ContentType.IntegerNumber;
        inputField.characterLimit = 1;
        tryStartGameBtn = transform.Find("TryStartGameBtn").GetComponent<Button>();
        tryStartGameBtn.onClick.AddListener(OnTryStartGame);
    }

    void OnTryStartGame()
    {
        // Check if the input is a valid number between 3 and 6
        if (int.TryParse(inputField.text, out int number) && number >= 3 && number <= 6)
        {
            GameManager.Instance.gameMode.StartGame(number);
            GameManager.Instance.HideCamera();
            UIManager.Instance.ShowPanel<HUDPanel>();
            Hide();
        }
        else
        {
            UIManager.Instance.ShowPanel<HintPanel>();
            EventManager.Instance.SendMessage(new ShowHintEvent(title, content));
        }
    }
}