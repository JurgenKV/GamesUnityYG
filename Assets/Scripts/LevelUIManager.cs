using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using YG;

public class LevelUIManager : MonoBehaviour
{
    [SerializeField] private List<LevelUI> levelUis;
    [SerializeField] private LevelDataStorage levelDataStorageDefault;
    void Start()
    {
        CheckTotalCats();
        UpdateLevelUI();
        ADManagerYG.ShowFullAds();
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

    public void EndRewardUnlockLevel(int tempLevelID)
    {
        LevelData tempLevel = YG2.saves.LevelDataYG.First(i=> i.Id == tempLevelID);
        tempLevel.IsUnlocked = true;
        //tempLevel.IsNeedRewardToUnlock = false;
        YG2.SaveProgress();

        levelUis.First(id => id.LevelID == tempLevelID).CheckLockState(tempLevel);
    }

    private void CheckTotalCats()
    {
        YG2.saves.TotalCats = YG2.saves.LevelDataYG.Sum(level => level.CatsCoughtTotal);
        
        YG2.saves.SetAnyLeaderboard("CatFinder", YG2.saves.TotalCats );
    }
}
