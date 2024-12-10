using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LevelUI : MonoBehaviour
{
    public int LevelID;
    [SerializeField] private TMP_Text levelNum;
    [SerializeField] private TMP_Text catCount;
    [SerializeField] private Image levelImage;
    [SerializeField] private Button startLevelButton;
    [SerializeField] private Button againLevelButton;
    
    [SerializeField] private GameObject shadowPanelAD;
    [SerializeField] private GameObject shadowPanelNeedCats;
    
    [SerializeField] private TMP_Text needCatCount;
    
    public void SetLevelUI(LevelData levelData)
    {
        LevelID = levelData.Id;
        levelNum.text = (LevelID + 1).ToString();
        catCount.text = levelData.IdOfCoughtCats.Count.ToString() + '/' + levelData.CatsAmount.ToString();
        CheckLockState(levelData);
    }
    
    public void SetLevelImage(Sprite sprite)
    {
        levelImage.sprite = sprite;
    }

    private void SetAgainLevelButton(LevelData levelData)
    {
        againLevelButton.interactable = levelData.IdOfCoughtCats.Count != 0;

        if (!levelData.IsUnlocked)
            againLevelButton.interactable = false;
    }
    
    private void SetStartButton(LevelData levelData)
    {
        if (!levelData.IsUnlocked)
            startLevelButton.interactable = false;
    }

    public void StartLevel()
    {
        FindAnyObjectByType<SceneLoadController>().LoadSceneWithAnim("Level " + LevelID.ToString());
    }

    public void AgainLevel()
    {
        FindAnyObjectByType<DeleteLevelProgressUI>().OpenDeleteLevelProgressUI(LevelID);
    }

    public void CheckLockState(LevelData levelData)
    {
        if (!levelData.IsUnlocked)
        {   
            CheckReward(levelData);
            CheckTotalCats(levelData);
        }
        else
        {
            shadowPanelAD.SetActive(false);
            shadowPanelNeedCats.SetActive(false);
            startLevelButton.interactable = true;
        }
        
        SetAgainLevelButton(levelData);
        SetStartButton(levelData);
    }

    private void CheckReward(LevelData levelData)
    {
        if (levelData.IsNeedRewardToUnlock)
        {
            shadowPanelAD.SetActive(true);
        }
    }

    private void CheckTotalCats(LevelData levelData)
    {
        if (levelData.IsNeedTotalCatsToUnlock)
        {
            if (YG2.saves.TotalCats < levelData.CatsToUnlock)
            {
                shadowPanelNeedCats.SetActive(true);
                needCatCount.text = (levelData.CatsToUnlock - YG2.saves.TotalCats).ToString();
            }else
            {
                shadowPanelNeedCats.SetActive(false);
                YG2.saves.LevelDataYG.Find(i=> i.Id == LevelID).IsUnlocked = true;
                YG2.SaveProgress();
                FindAnyObjectByType<LevelUIManager>().UpdateLevelUI();
            }
        }
    }

    public void StartRewardAD()
    {
        FindAnyObjectByType<ADManagerYG>().StartRewardUnlockLevel("1", LevelID);
    }
    
}
