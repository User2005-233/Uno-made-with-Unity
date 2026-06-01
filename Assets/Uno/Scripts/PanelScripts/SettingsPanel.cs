using UnityEngine.UIElements;

public class SettingsPanel : BasePanel
{
    Button backButton;
    
    
    public override void InitContent()
    {
        UIManager.Instance.RegisterSingletonPanel<SettingsPanel>(this);
        backButton = transform.Find("BackBtn").GetComponent<Button>();
        backButton.clicked += OnBackButtonClicked;
    }
    
    private void OnBackButtonClicked()
    {
        Hide();
    }
    
    
    
}