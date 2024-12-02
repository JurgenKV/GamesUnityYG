using UnityEngine;
using UnityEngine.UI;
using YG;

public class AudioButtonsControl : MonoBehaviour
{
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Image musicImage;
    [SerializeField] private Sprite musicOnSprite;
    [SerializeField] private Sprite musicOffSprite;
    
    [Space(10)]
    
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private Image soundImage;
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;
    
    void Start()
    {
        RefreshButtonsUI();
    }

    private void RefreshButtonsUI()
    {
        if (musicToggle != null)
            RefreshMusicUI();
        
        if(soundToggle != null)
            RefreshSoundUI();
    }

    private void RefreshMusicUI()
    {
        if (YG2.saves.IsMusicActive)
        {
            musicToggle.isOn = true;
            musicImage.sprite = musicOnSprite;
        }
        else
        {
            musicToggle.isOn = false;
            musicImage.sprite = musicOffSprite;
        }
    }

    private void RefreshSoundUI()
    {
        if (YG2.saves.IsSoundActive)
        {
            soundToggle.isOn = true;
            soundImage.sprite = soundOnSprite;
        }
        else
        {
            soundToggle.isOn = false;
            soundImage.sprite = soundOffSprite;
        }
        
    }

    public void OnClickMusicToggle(bool isOn)
    {
        YG2.saves.IsMusicActive = isOn;
        YG2.SaveProgress();
        RefreshMusicUI();
    }
    
    public void OnClickSoundToggle(bool isOn)
    {
        YG2.saves.IsSoundActive = isOn;
        YG2.SaveProgress();
        RefreshSoundUI();
    }
    
    
    
}
