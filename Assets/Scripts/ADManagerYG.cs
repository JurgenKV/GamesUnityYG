using System;
using System.Linq;
using UnityEngine;
using YG;

public class ADManagerYG : MonoBehaviour
{
    public string rewardID;
    private int tempLevelID;
    private GameController _gameController;

    private void Start()
    {
        _gameController = FindAnyObjectByType<GameController>();
    }

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
        switch (id)
        {
            case "0":
                EndRewardGetHearth();
                Debug.Log("EndRewardGetHearth();");
                break;
            case "1":
                EndRewardFreezeTime();
                Debug.Log("EndRewardFreezeTime();");
                break;
            case "2":
                EndRewardOneHealth();
                Debug.Log("EndRewardFreezeTime();");
                break;
            case "3":
                EndRewardScoreMultiplier();
                Debug.Log("EndRewardFreezeTime();");
                break;
            case "4":

                break;
            default:
                Debug.Log("OnReward " + id + " is unknown");
                break;
        }
    }

    public void StartRewardGetHearth(string id)
    {
        rewardID = id;
        YG2.RewardedAdvShow(rewardID);
    }

    private void EndRewardGetHearth()
    {
        _gameController.RestoreGame();
    }

    public void StartRewardFreezeTime(string id)
    {
        rewardID = id;
        YG2.RewardedAdvShow(rewardID);
    }

    private void EndRewardFreezeTime()
    {
        _gameController.PrizeFreeze();
    }

    public void StartRewardOneHealth(string id)
    {
        rewardID = id;
        YG2.RewardedAdvShow(rewardID);
    }

    private void EndRewardOneHealth()
    {
        _gameController.PrizeHealth();
    }

    public void StartRewardScoreMultiplier(string id)
    {
        rewardID = id;
        YG2.RewardedAdvShow(rewardID);
    }

    private void EndRewardScoreMultiplier()
    {
        _gameController.PrizeScore();
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