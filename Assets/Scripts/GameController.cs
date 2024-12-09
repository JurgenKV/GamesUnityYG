using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class GameController : MonoBehaviour
{
    private List<Cat> _cats;
    [SerializeField] private LevelDataStorage _levelDataStorage;
    private LevelData _currentlevelData;
    void Start()
    {
        string levelName = SceneManager.GetActiveScene().name.ToString();
        levelName = Regex.Replace(levelName, @"^.*? ", "");
        _currentlevelData = _levelDataStorage.Levels.Find(i=> i.Id == int.Parse(levelName));

        SetCatsState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetCatsState()
    {
        for (int i = 0; i < _cats.Count; i++)
        {
            _cats[i].CatID = i;
            if (_currentlevelData.IdOfCoughtCats.Contains(i))
                _cats[i].WasFound = true;
        }
        
    }

    // public int GetCatID(Cat cat)
    // {
    //     return _cats.FindIndex(item => item.Equals(cat));
    // }
    
}
