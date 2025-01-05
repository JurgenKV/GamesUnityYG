using System.Collections.Generic;
using UnityEngine;
using YG;

public class DebugObject : MonoBehaviour
{
    [SerializeField] private bool isDebugActive = false;
    [SerializeField] private bool Music = false;
    [SerializeField] private bool Sound = false;
    
    [Range(0, 10)]
    [SerializeField] private float gameSpeed = 1f;
    void Start()
    {
        if(!isDebugActive)
            Destroy(gameObject);
    }

    void Update()
    {
        //Music = YG2.saves.IsMusicActive;
        //Sound = YG2.saves.IsSoundActive;
        Time.timeScale = gameSpeed;
    }
}
