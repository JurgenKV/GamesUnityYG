using System;
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
    private static readonly int Heart = Animator.StringToHash("Heart");
    
    public bool IsGameRunning = false;
    public bool IsGamePaused = false;

    private int _currentScore = 0;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private TMP_Text _currentScoreTextGameUI;
    [SerializeField] private TMP_Text _currentScoreTextUI;
    [SerializeField] private TMP_Text _bestScoreTextUI;
    [SerializeField] private GameObject _settingsPanelUI;
    public int CurrentScore
    {
        get => _currentScore;
        set
        {
            _currentScore = value;
            SetAllScoreUI();
        }
    }
    private int _currentHealth = 3;
    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value; 
            _healthBarAnimator.SetInteger(Heart , value);
            if (value == 0)
            {
                IsGamePaused = true;
                _gameOverUI.SetActive(true);
                SetAllScoreUI();
            }
        }
    }

    [SerializeField] private Animator _healthBarAnimator;
    
    private void Start()
    {
        CurrentHealth = 3;
    }

    public void OnClickContinue()
    {
        SetPause(false);
    }

    public void SetPause(bool pause)
    {
        IsGamePaused = pause;
    }

    private void SetAllScoreUI()
    {
        _currentScoreTextGameUI.text = _currentScore.ToString();
        _currentScoreTextUI.text = _currentScore.ToString();
        
        if (_currentScore > YG2.saves.TopScore)
        {
            YG2.saves.TopScore = _currentScore;
            YG2.saves.SetAnyLeaderboard("TopScore", _currentScore);
            YG2.SaveProgress();
        }

        TopScoreByLang();

    }
    
    private void TopScoreByLang()
    {
        switch (YG2.lang)
        {
            case "ru":
                _bestScoreTextUI.text = "Рекорд " + YG2.saves.TopScore.ToString();
                break;
            case "en":
                _bestScoreTextUI.text = "Best " + YG2.saves.TopScore.ToString();
                break;
            case "tr":
                _bestScoreTextUI.text = "Kayıt " + YG2.saves.TopScore.ToString();
                break;
            default:
                _bestScoreTextUI.text = "Best " + YG2.saves.TopScore.ToString();
                break;
        }
    }

    public void RestoreGame()
    {
        CurrentHealth += 1;
        _gameOverUI.SetActive(false);
        IsGamePaused = false;
        
        if(_settingsPanelUI.activeSelf)
            _settingsPanelUI.SetActive(false);
        
    }
}
