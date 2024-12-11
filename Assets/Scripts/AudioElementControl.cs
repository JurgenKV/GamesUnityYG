using System;
using System.Collections.Generic;
using UnityEngine;
using YG;

enum AudioType
{
    Music,
    Sound
}
public class AudioElementControl : MonoBehaviour
{
    [SerializeField] AudioType audioType = AudioType.Music;
    [SerializeField] private List <AudioSource> audioSourceByType;

    private bool _prevIsSoundActive = false;
    private bool _prevIsMusicActive = false;
    private void Start()
    {
        CheckStartAudio();
        //CheckAudio();
    }

    private void Update()
    {
        CheckAudio();
    }

    private void CheckAudio()
    {
        switch (audioType)
        {
            case AudioType.Music:
                CheckMusicSettings();
                break;
            case AudioType.Sound:
                CheckSoundSettings();
                break;
        }
    }

    private void CheckSoundSettings()
    {
        if(_prevIsSoundActive == YG2.saves.IsSoundActive)
            return;
        
        if (YG2.saves.IsSoundActive)
        {
            foreach (AudioSource source in audioSourceByType)
            {
                source.mute = false;
            }
        }
        else
        {
            foreach (AudioSource source in audioSourceByType)
            {
                source.mute = true;
            }
        }

        _prevIsSoundActive = YG2.saves.IsSoundActive;
    }

    private void CheckMusicSettings()
    {
        if(_prevIsMusicActive == YG2.saves.IsMusicActive)
            return;
        
        if (YG2.saves.IsMusicActive)
        {
            foreach (AudioSource source in audioSourceByType)
            {
                source.mute = false;
            }
        }
        else
        {
            foreach (AudioSource source in audioSourceByType)
            {
                source.mute = true;
            }
        }
        
        _prevIsMusicActive = YG2.saves.IsMusicActive;
    }

    private void CheckStartAudio()
    {
        _prevIsSoundActive = YG2.saves.IsSoundActive;
        _prevIsMusicActive = YG2.saves.IsMusicActive;
        
        if (YG2.saves.IsSoundActive)
        {
            foreach (AudioSource source in audioSourceByType)
            {
                source.mute = false;
            }
        }
        else
        {
            foreach (AudioSource source in audioSourceByType)
            {
                source.mute = true;
            }
        }
        
        if (YG2.saves.IsMusicActive)
        {
            foreach (AudioSource source in audioSourceByType)
            {
                source.mute = false;
            }
        }
        else
        {
            foreach (AudioSource source in audioSourceByType)
            {
                source.mute = true;
            }
        }
    }
    
    
    
    
    
}
