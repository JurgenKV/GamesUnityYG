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

        public int TopScore = 0;
        //public int AmountMoney = 0;
        //public int TotalCats = 0;
        //public List<LevelData> LevelDataYG = new List<LevelData>();
        public void SetDefaultData()
        {
            IsMusicActive = true;
            IsSoundActive = true;
            TopScore = 0;
        }
        public void SetAnyLeaderboard(string name, int value)
        {
            YG2.SetLeaderboard(name, value);
        }
    }
    
}