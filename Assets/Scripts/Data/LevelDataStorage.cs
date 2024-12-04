using System;
using System.Collections.Generic;
using UnityEngine;

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
    public int CatsCought = 0;
    public bool IsNeedRewardToUnlock = false;
    public bool IsUnlocked = false;
    public int[] IdOfCoughtCats = Array.Empty<int>();
}
