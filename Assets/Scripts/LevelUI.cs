using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LevelUI : MonoBehaviour
{
    private int _levelId;
    [SerializeField] private TMP_Text levelNum;
    [SerializeField] private TMP_Text catCount;
    [SerializeField] private Image levelImage;
    [SerializeField] private Button startLevelButton;
    [SerializeField] private Button againLevelButton;
    
    public void SetLevelUI(LevelData levelData)
    {
        _levelId = levelData.Id;
        levelNum.text = (_levelId + 1).ToString();
        catCount.text = levelData.CatsCought.ToString() + '/' + levelData.CatsAmount.ToString();
        SetAgainLevelButton(levelData);
        SetStartButton(levelData);
    }
    
    public void SetLevelImage(Sprite sprite)
    {
        levelImage.sprite = sprite;
    }

    private void SetAgainLevelButton(LevelData levelData)
    {
        againLevelButton.interactable = levelData.CatsCought != 0;

        if (levelData.IsUnlocked)
            againLevelButton.interactable = false;
    }
    
    private void SetStartButton(LevelData levelData)
    {
        if (levelData.IsUnlocked)
            startLevelButton.interactable = false;
    }

    public void StartLevel()
    {
        FindAnyObjectByType<SceneLoadController>().LoadSceneWithAnim("Level " + _levelId.ToString());
    }

    public void AgainLevel()
    {
        FindAnyObjectByType<DeleteLevelProgressUI>().OpenDeleteLevelProgressUI(_levelId);
    }
    
}
