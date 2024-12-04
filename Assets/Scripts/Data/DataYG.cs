using System;
using System.Collections.Generic;

namespace YG
{
    public partial class SavesYG
    {
        //Settings
        public bool IsMusicActive = false;
        public bool IsSoundActive = false;
        //PlayerData
        public int AmountMoney = 0;
        public int TopScore = 0;
        public List<LevelData> LevelDataYG = new List<LevelData>();
    }
    
}