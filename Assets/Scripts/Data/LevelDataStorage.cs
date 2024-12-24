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
    
}

[Serializable]
public class LevelData
{
    public int Id;
    public int CatsAmount = 0;
    //public int CatsCought = 0;
    public int CatsCoughtTotal = 0;

    public bool IsNeedRewardToUnlock = false;
    public bool IsNeedTotalCatsToUnlock = false;

    public int CatsToUnlock = 0;

    public bool IsUnlocked = false;
    public List<int> IdOfCoughtCats = new List<int>();

    public LevelData(
        int id,
        int catsAmount,
        int catsCought,
        int catsCoughtTotal,
        bool isNeedRewardToUnlock,
        bool isNeedTotalCatsToUnlock,
        int catsToUnlock,
        bool isUnlocked,
        List<int> idOfCoughtCats)
    {
        Id = id;
        CatsAmount = catsAmount;
        //CatsCought = catsCought;
        CatsCoughtTotal = catsCoughtTotal;
        IsNeedRewardToUnlock = isNeedRewardToUnlock;
        IsNeedTotalCatsToUnlock = isNeedTotalCatsToUnlock;
        CatsToUnlock = catsToUnlock;
        IsUnlocked = isUnlocked;
        IdOfCoughtCats = idOfCoughtCats ?? new List<int>();
    }

    public LevelData(LevelData other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));

        Id = other.Id;
        CatsAmount = other.CatsAmount;
        //CatsCought = other.CatsCought;
        CatsCoughtTotal = other.CatsCoughtTotal;
        IsNeedRewardToUnlock = other.IsNeedRewardToUnlock;
        IsNeedTotalCatsToUnlock = other.IsNeedTotalCatsToUnlock;
        CatsToUnlock = other.CatsToUnlock;
        IsUnlocked = other.IsUnlocked;

        IdOfCoughtCats = new List<int>(other.IdOfCoughtCats);
    }

    public void SetTotalCats(int newTotal)
    {
        if(newTotal > CatsCoughtTotal)
            CatsCoughtTotal = newTotal;
    }
}