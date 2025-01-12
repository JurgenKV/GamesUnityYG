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
    public float SpeedMultiplier = 1;
    private float _tempSpeedMultiplier;
    public Witch witch;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private TMP_Text _currentScoreTextGameUI;
    [SerializeField] private TMP_Text _currentScoreTextUI;
    [SerializeField] private TMP_Text _bestScoreTextUI;
    [SerializeField] private GameObject _settingsPanelUI;
    [SerializeField] private Animator _healthBarAnimator;
    
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
                SetPause(true);
                _gameOverUI.SetActive(true);
                SetAllScoreUI();
            }
        }
    }

    
    
    private void Start()
    {
        CurrentHealth = 3;
        _tempSpeedMultiplier = SpeedMultiplier;
        
        InvokeRepeating(nameof(DifficultUp), 15, 15);
    }

    private void DifficultUp()
    {
        if(IsGamePaused || !IsGameRunning)
            return;
        witch.PlayCommonWitch();
        if(SpeedMultiplier >= 1.7f)
            return;
        
        SpeedMultiplier += 0.1f;
    }

    public void OnClickContinue()
    {
        SetPause(false);
    }

    public void SetPause(bool pause)
    {
        if (IsGamePaused == pause)
        {
            return;
        }

        IsGamePaused = pause;

        if (IsGamePaused)
        {
            _tempSpeedMultiplier = SpeedMultiplier;
            SpeedMultiplier = 0f;
        }
        else
        {
            SpeedMultiplier = _tempSpeedMultiplier;
        }
    }

    private void SetAllScoreUI()
    {
        _currentScoreTextGameUI.text = _currentScore.ToString();
        _currentScoreTextUI.text = _currentScore.ToString();
        
        if (_currentScore > YG2.saves.TopScore)
        {
            YG2.saves.TopScore = _currentScore;
            YG2.saves.SetAnyLeaderboard("TopCatScore", _currentScore);
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
        SpeedMultiplier = _tempSpeedMultiplier;
        
        _currentScoreTextGameUI.text = _currentScore.ToString();
        _currentScoreTextUI.text = _currentScore.ToString();
        
        if(_settingsPanelUI.activeSelf)
            _settingsPanelUI.SetActive(false);
        
    }
}
