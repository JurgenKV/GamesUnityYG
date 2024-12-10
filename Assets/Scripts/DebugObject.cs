using System.Collections.Generic;
using UnityEngine;
using YG;

public class DebugObject : MonoBehaviour
{
    [SerializeField] private bool isDebugActive = false;
    
    [SerializeField] private List<LevelData> _levelDatas = new List<LevelData>();
    void Start()
    {
        if(!isDebugActive)
            Destroy(gameObject);
    }

    void Update()
    {
        _levelDatas = YG2.saves.LevelDataYG;
    }
}
