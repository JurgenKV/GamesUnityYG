using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

public class LevelUIManager : MonoBehaviour
{
    [SerializeField] private List<LevelUI> levelUis;
    [SerializeField] private LevelDataStorage levelDataStorageDefault;
    void Start()
    {
        UpdateLevelUI();
    }

    public void UpdateLevelUI()
    {
        var levelData = YG2.saves.LevelDataYG.OrderBy(i => i.Id);
        
        for (int i = 0; i < levelData.Count(); i++)
        {
            levelUis[i].SetLevelUI(levelData.ElementAt(i));
            levelUis[i].SetLevelImage(levelDataStorageDefault.LevelLogo.ElementAt(i));
        }
    }
}
