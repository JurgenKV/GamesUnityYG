using UnityEngine;
using UnityEngine.UI;
using YG;

public class AudioButtonsControl : MonoBehaviour
{
    private static readonly int IsActive = Animator.StringToHash("IsActive");
    
    [SerializeField] private Toggle musicToggle;
    [SerializeField] private Animator musicSwitchAnimator;
    
    [Space(10)]
    
    [SerializeField] private Toggle soundToggle;
    [SerializeField] private Animator soundSwitchAnimator;

    
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
            musicSwitchAnimator.SetBool(IsActive, true);
        }
        else
        {
            musicToggle.isOn = false;
            musicSwitchAnimator.SetBool(IsActive, false);
        }
    }

    private void RefreshSoundUI()
    {
        if (YG2.saves.IsSoundActive)
        {
            soundToggle.isOn = true;
            soundSwitchAnimator.SetBool(IsActive, true);
        }
        else
        {
            soundToggle.isOn = false;
            soundSwitchAnimator.SetBool(IsActive, false);
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
