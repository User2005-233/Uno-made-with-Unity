using UnityEngine.UI;


public class SettingsPanel : BasePanel
{
    Button backButton;
    
    
    public override void InitContent()
    {
        backButton = transform.Find("BackBtn").GetComponent<Button>();
        backButton.onClick.AddListener(OnBackButtonClicked);
    }
    protected override void Awake()
    {
        HideInit();
    }
    
    private void OnBackButtonClicked()
    {
        UIManager.Instance.ShowPanel<MainMenuPanel>();
        Hide();
    }
    
    
    
}