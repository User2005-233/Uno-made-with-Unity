using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerSettingPanel : BasePanel
{
    InputField inputField;
    Button tryStartGameBtn;
    readonly string title = "提示";
    readonly string content = "请输入3-6之间的数字";
    public override void InitContent()
    {
        inputField = transform.Find("Number").GetComponent<InputField>();
        tryStartGameBtn = transform.Find("TryStartGameBtn").GetComponent<Button>();
        tryStartGameBtn.onClick.AddListener(OnTryStartGame);
    }

    void OnTryStartGame(){
        int number = int.Parse(inputField.text);
        if(number <= 6 && number >= 3){
            GameManager.Instance.gameMode.StartGame(number);
        }else{
            UIManager.Instance.ShowPanel<HintPanel>();
            EventManager.Instance.SendMessage(new ShowHintEvent(title, content));
        }
    }
}