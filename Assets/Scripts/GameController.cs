using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class GameController : MonoBehaviour
{
    public List<Cat> CatsList;
    [SerializeField] private TMP_Text catCounterText;
    //[SerializeField] private LevelDataStorage _defaultLevelDataStorage;

    [SerializeField] private Button helpAdButton;
    [SerializeField] private TMP_Text CatsAmountText;
    
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
        //_currentlevelData.IdOfCoughtCats.Add(catId);
        YG2.saves.LevelDataYG.Find(i => i.Id == _levelID).IdOfCoughtCats.Add(catId);
        
        //YG2.saves.LevelDataYG.First(i => i.Equals(levelData)).IdOfCoughtCats.Add(catId);
        
        Cat tempCat = CatsList.First(i=>i.CatID == catId);
        tempCat.WasFound = true;
        tempCat.CheckCatState();
        
        _currentlevelData.SetTotalCats(_currentlevelData.IdOfCoughtCats.Count);
        UpdateUI();
        
        YG2.SaveProgress();
    }

    public void OnClickHelpAdButton(int helpCooldown = 0)
    {
        StartAdVideoHelpButton();
        StartCoroutine(AdCoolDownCoroutine(helpCooldown));
        EndAdVideoHelpButton();
    }

    private void StartAdVideoHelpButton()
    {
        
    }

    public void EndAdVideoHelpButton()
    {
        List<int> missingIds = CatsList
            .Where(obj => !obj.IsHelpActive) 
            .Select(obj => obj.CatID)        
            .Where(id => !_currentlevelData.IdOfCoughtCats.Contains(id)) 
            .ToList();
        int randomIndex = Random.Range(0, missingIds.Count);
        
        int randomId = missingIds[randomIndex];
        
        CatsList.First(i=> i.CatID == randomId).SetHelpTrigger();
    }
    private IEnumerator AdCoolDownCoroutine(int cooldown)
    {
        helpAdButton.interactable = false;
        yield return new WaitForSeconds(cooldown);
        helpAdButton.interactable = true;
        UpdateUI();
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