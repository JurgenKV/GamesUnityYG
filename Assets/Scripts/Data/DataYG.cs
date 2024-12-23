using System;
using System.Collections.Generic;
using System.Linq;

namespace YG
{
    public partial class SavesYG
    {
        ////Settings
        
        public bool IsMusicActive = false;
        public bool IsSoundActive = false;
        
        ////PlayerData
        
        //public int TopScore = 0;
        //public int AmountMoney = 0;
        public int TotalCats = 0;
        public List<LevelData> LevelDataYG = new List<LevelData>();
        
        public void SetData(List<LevelData> defaultLevelData)
        {
            IsMusicActive = true;
            IsSoundActive = true;
            TotalCats = 0;
            LevelDataYG.Clear();
            LevelDataYG = defaultLevelData
                .Select(level => new LevelData(level))
                .ToList();
            YG2.SaveProgress();
        }

        public void SetAnyLeaderboard(string name, int value)
        {
            YG2.SetLeaderboard(name, value);
        }
    }
    
}