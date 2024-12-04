using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class InitPlugin : MonoBehaviour
{
    [SerializeField] private string sceneName = "LanguageScene";
    [SerializeField] private LevelDataStorage levelDataStorage;
    private void OnEnable() => YG2.onGetSDKData += OnGetData;

    private void OnGetData()
    {
        if (YG2.isFirstGameSession)
            LoadDefaultLevelData();
        
        SceneManager.LoadScene(sceneName);
    }

    private void LoadDefaultLevelData()
    {
        YG2.saves.LevelDataYG = new List<LevelData>();
        YG2.saves.LevelDataYG.AddRange(levelDataStorage.Levels);
    }
}
