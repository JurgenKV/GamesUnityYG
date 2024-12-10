using System;
using UnityEngine;

public class DeleteLevelProgressUI : MonoBehaviour
{
    [SerializeField] private LevelDataStorage levelDataStorageDefault;
    [SerializeField] private LevelUIManager levelUIManager;
    public int levelToDelete = -1;
    [SerializeField] private GameObject _panel;

    private void Start()
    {
        levelUIManager = FindAnyObjectByType<LevelUIManager>();
    }

    public void OpenDeleteLevelProgressUI(int levelNum)
    {
        levelToDelete = levelNum;
        _panel.SetActive(true);
    }

    public void DeleteProgressButton()
    {
        if(levelToDelete >= 0)
            levelDataStorageDefault.ResetLevelYg(levelToDelete);
        
        ClosePanelButton();
        levelUIManager.UpdateLevelUI();
    }

    public void ClosePanelButton()
    {
        levelToDelete = -1;
        _panel.SetActive(false);
    }
}
