using System;
using UnityEngine;
using YG;


public class FocusSoundController : MonoBehaviour
{
    private float _volume;
    void OnApplicationFocus(bool hasFocus)
    {
        Silence(!hasFocus);
    }

    void OnApplicationPause(bool isPaused)
    {
        Silence(isPaused);
    }

    private void Silence(bool silence)
    {
        AudioListener.pause = silence;
    }

    private void Awake()
    {
        _volume = AudioListener.volume;
        CheckADS();
    }

    private void Update()
    {
        CheckADS();
    }

    private void CheckADS()
    {
        if (YG2.nowAdsShow || YG2.nowRewardAdv || YG2.nowInterAdv)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = _volume;
        }
    }
}