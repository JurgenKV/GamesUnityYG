using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class InitPlugin : MonoBehaviour
{
    [SerializeField] private string sceneName = "LanguageScene";
    [SerializeField] private LevelDataStorage levelDataStorageDefault;

    private void OnEnable()
    {
        YG2.onGetSDKData += OnGetData;
    }

    private void OnGetData()
    {
        if (YG2.isFirstGameSession)
        {
            LoadDefaultLevelData();
            YG2.SaveProgress();
        }

        FixId();
        SceneManager.LoadScene(sceneName);
    }

    private void LoadDefaultLevelData()
    {
        YG2.saves.SetData(levelDataStorageDefault.Levels);
        //YG2.saves.LevelDataYG.AddRange(levelDataStorageDefault.Levels);
    }

    private void FixId()
    {
        if (YG2.saves.LevelDataYG.FindAll(i => i.Id == 9).Count <= 1)
            return;

        for (var i = 0; i < YG2.saves.LevelDataYG.Count; i++)
        {
            var index = levelDataStorageDefault.Levels[i].Id;
            YG2.saves.LevelDataYG[i].Id = index;
        }
    }
}