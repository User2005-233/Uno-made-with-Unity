using UnityEngine;
using UnityEngine.UI;

public class SinglePlayerSettingPanel : BasePanel
{
    InputField inputField;
    Button tryStartGameBtn;
    string title = "提示";
    string content = "请输入3-6之间的数字";
    public override void InitContent()
    {
        base.InitContent();
        inputField = transform.Find("Number").GetComponent<InputField>();
        tryStartGameBtn = transform.Find("TryStartGameBtn").GetComponent<Button>();
        tryStartGameBtn.onClick.AddListener(OnTryStartGame);
        UIManager.Instance.RegisterPanel<SinglePlayerSettingPanel>(this);
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