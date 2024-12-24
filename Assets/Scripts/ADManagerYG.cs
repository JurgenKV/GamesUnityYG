using System;
using System.Linq;
using UnityEngine;
using YG;

public class ADManagerYG : MonoBehaviour
{
       public string rewardID;

       private int tempLevelID;
       
       private void OnEnable()
       {
           YG2.onRewardAdv += OnReward;
       }
       private void OnDisable()
       {
           YG2.onRewardAdv -= OnReward;
       }
       
       private void OnReward(string id)
       {
           switch(id)
           {
               case "0":
                   EndRewardHelpFindCatLevel();
                   Debug.Log("EndRewardHelpFindCatLevel();");
                   break;
               case "1":
                   EndRewardUnlockLevel();
                   Debug.Log("EndRewardUnlockLevel();");
                   break;
               case "2":
                   
                   break;
               case "3":
                   
                   break;
               case "4":
                   
                   break;
               default:
                   Debug.Log("OnReward " + id + " is unknown");
                   break;
           }
       }

       public void StartRewardUnlockLevel(string id, int level)
       {
           rewardID = id;
           tempLevelID = level;
           YG2.RewardedAdvShow(rewardID);
       }

       private void EndRewardUnlockLevel()
       {
           FindAnyObjectByType<LevelUIManager>().EndRewardUnlockLevel(tempLevelID);
       }

       public void StartRewardHelpFindCatLevel(string id)
       {
           rewardID = id;
           YG2.RewardedAdvShow(rewardID);
       }
       
       private void EndRewardHelpFindCatLevel()
       {
           
       }
       
        
        public static void ShowFullAds()
        {
            try
            {
                YG2.InterstitialAdvShow();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
}
