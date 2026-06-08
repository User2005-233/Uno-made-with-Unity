using UnityEngine.UI;

public class SoundSubPanel : BasePanel
{

    Slider soundVolumeSlider;
    Slider musicVolumeSlider;
    Slider sfxVolumeSlider;


    public override void InitContent()
    {
        UIManager.Instance.RegisterPanel<SoundSubPanel>(this);
        soundVolumeSlider = transform.Find("Panel/OverallVolume/Slider").GetComponent<Slider>();
        musicVolumeSlider = transform.Find("Panel/MusicVolume/Slider").GetComponent<Slider>();
        sfxVolumeSlider = transform.Find("Panel/SFXVolume/Slider").GetComponent<Slider>();
    }

    protected override void OnShow()
    {
        base.OnShow();

        // TODO: Load saved volume settings and update sliders
        soundVolumeSlider.value = SoundManager.Instance.GetSoundVolume();
        musicVolumeSlider.value = SoundManager.Instance.GetMusicVolume();
        sfxVolumeSlider.value = SoundManager.Instance.GetSfxVolume();
    }
    
    protected override void OnHide()
    {
        base.OnHide();
        
        // TODO: Save current volume settings
    }
}
