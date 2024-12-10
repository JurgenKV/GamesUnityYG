using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YG;

[CreateAssetMenu(fileName = "LevelDataStorage", menuName = "ScriptableObjects/LevelDataStorage", order = 1)]
public class LevelDataStorage : ScriptableObject
{
    public List<LevelData> Levels = new List<LevelData>();
    public List<Sprite> LevelLogo = new List<Sprite>();

    public void ResetLevelYg(int levelId)
    {
        LevelData levelToReset = YG2.saves.LevelDataYG.First(i=> i.Id == levelId);
        levelToReset = Levels.Find(i=> i.Id == levelId);
        YG2.SaveProgress();
    }
}

[Serializable]
public class LevelData
{
    public int Id;
    public int CatsAmount = 0;
    public int CatsCought = 0;
    public bool IsNeedRewardToUnlock = false;
    public bool IsUnlocked = false;
    public int[] IdOfCoughtCats = Array.Empty<int>();
    
    public LevelData(int id, int catsAmount, int catsCought, bool isNeedRewardToUnlock, bool isUnlocked, int[] idOfCoughtCats)
    {
        Id = id;
        CatsAmount = catsAmount;
        CatsCought = catsCought;
        IsNeedRewardToUnlock = isNeedRewardToUnlock;
        IsUnlocked = isUnlocked;
        IdOfCoughtCats = idOfCoughtCats ?? Array.Empty<int>(); 
    }
    
    public LevelData(LevelData other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));

        Id = other.Id;
        CatsAmount = other.CatsAmount;
        CatsCought = other.CatsCought;
        IsNeedRewardToUnlock = other.IsNeedRewardToUnlock;
        IsUnlocked = other.IsUnlocked;
        
        IdOfCoughtCats = other.IdOfCoughtCats != null ? (int[])other.IdOfCoughtCats.Clone() : Array.Empty<int>();
    }
}
