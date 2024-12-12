using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject descWindow;
    [SerializeField] private GameObject descWindowWin;
    [SerializeField] private Button menuButton;
    public List<Cat> CatsList;
    public int helpCooldown = 60;
    
    [SerializeField] private TMP_Text catCounterText;
    //[SerializeField] private LevelDataStorage _defaultLevelDataStorage;

    [SerializeField] private Button helpAdButton;
    [SerializeField] private TMP_Text CatsAmountText;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioResource> _resources;
    
    private LevelData _currentlevelData;
    private int _levelID = -1;
    void Start()
    {
        string levelName = SceneManager.GetActiveScene().name.ToString();
        _levelID = int.Parse(Regex.Replace(levelName, @"^.*? ", ""));
        
        _currentlevelData = YG2.saves.LevelDataYG.First(i=> i.Id == _levelID);
        
        SetCatsState();
        UpdateUI();
    }

    private void SetCatsState()
    {
        for (int i = 0; i < CatsList.Count; i++)
        {
            CatsList[i].CatID = i;
            if (_currentlevelData.IdOfCoughtCats.Contains(i))
                CatsList[i].WasFound = true;
            
            CatsList[i].CheckCatState();
        }
        
    }

    public void CatWasFound(int catId)
    {
        PlayRandomAudio();
        YG2.saves.LevelDataYG.Find(i => i.Id == _levelID).IdOfCoughtCats.Add(catId);
        Cat tempCat = CatsList.First(i=>i.CatID == catId);
        tempCat.WasFound = true;
        tempCat.CheckCatState();
        
        _currentlevelData.SetTotalCats(_currentlevelData.IdOfCoughtCats.Count);
        UpdateUI();
        CheckWinDesc();
        YG2.SaveProgress();
    }

    private void CheckWinDesc()
    {
        if (!CatsList.TrueForAll(i => i.WasFound))
            return;
        
        descWindow.SetActive(false);
        descWindowWin.SetActive(true);
        menuButton.interactable = false;
    }

    public void OnClickHelpAdButton()
    {
        StartCoroutine(AdCoolDownCoroutine(helpCooldown));
        StartAdVideoHelpButton();
    }

    private void StartAdVideoHelpButton()
    {
        FindAnyObjectByType<ADManagerYG>().StartRewardHelpFindCatLevel("0");
    }

    public void EndAdVideoHelpButton()
    {
        List<int> missingIds = CatsList
            .Where(obj => !obj.IsHelpActive) 
            .Select(obj => obj.CatID)        
            .Where(id => !_currentlevelData.IdOfCoughtCats.Contains(id)) 
            .ToList();
        
        int randomId = missingIds[Random.Range(0, missingIds.Count)];
        
        CatsList.First(i=> i.CatID == randomId).SetHelpTrigger();
        
        FindAnyObjectByType<CameraSizeChanger>().ScrollHelp();
    }
    private IEnumerator AdCoolDownCoroutine(int cooldown)
    {
        helpAdButton.interactable = false;
        yield return new WaitForSeconds(cooldown);
        helpAdButton.interactable = true;
        UpdateUI();
    }

    private void PlayRandomAudio()
    {
        audioSource.resource = _resources[Random.Range(0, _resources.Count)];
        audioSource.Play();
    }
    private void UpdateUI()
    {
        CatsAmountText.text = (_currentlevelData.CatsAmount - _currentlevelData.IdOfCoughtCats.Count).ToString();

        if (CatsList.TrueForAll(i=>i.WasFound || i.IsHelpActive))
        {
            helpAdButton.interactable = false;
        }
    }
}
