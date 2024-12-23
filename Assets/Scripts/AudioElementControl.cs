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
    [SerializeField] private AudioType audioType;
    [SerializeField] private List<AudioSource> audioSourceByType;

    private bool previousState;

    private void Start()
    {
        previousState = GetAudioState();
        CheckAudio();
    }

    private void Update()
    {
        bool currentState = GetAudioState();
        if (currentState != previousState)
        {
            previousState = currentState;
            CheckAudio();
        }
    }

    private bool GetAudioState()
    {
        return audioType switch
        {
            AudioType.Music => YG2.saves.IsMusicActive,
            AudioType.Sound => YG2.saves.IsSoundActive,
            _ => true
        };
    }

    private void CheckAudio()
    {
        bool isActive = GetAudioState();
        SetAudioMute(!isActive);
    }

    private void SetAudioMute(bool mute)
    {
        foreach (AudioSource source in audioSourceByType)
        {
            source.mute = mute;
        }
    }
}