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
using Random = UnityEngine.Random;


public class GameController : MonoBehaviour
{
    private static readonly int Heart = Animator.StringToHash("Heart");
    private static readonly int FreezeAds = Animator.StringToHash("FreezeADS");
    private static readonly int HealthAds = Animator.StringToHash("HealthADS");
    private static readonly int ScoreAds = Animator.StringToHash("ScoreADS");

    public bool IsGameRunning = false;
    public bool IsGamePaused = false;

    private int _currentScore = 0;
    public float SpeedMultiplier = 1;
    public float FreezeSpeedMultiplier = 1;
    public bool IssImmortality = false;
    private float _tempSpeedMultiplier;
    public Witch witch;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private Button _adsButton;
    [SerializeField] private TMP_Text _currentScoreTextGameUI;
    [SerializeField] private TMP_Text _currentScoreTextUI;
    [SerializeField] private TMP_Text _bestScoreTextUI;
    [SerializeField] private GameObject _settingsPanelUI;
    [SerializeField] private Animator _healthBarAnimator;
    [SerializeField] private Animator _adsAnimator;
    [SerializeField] private List<Button> _adsButtons;
    public int CurrentScore
    {
        get => _currentScore;
        set
        {
            _currentScore = value + _scoreMultiplier;
            SetAllScoreUI();
        }
    }

    private int _scoreMultiplier = 0;
    private int _currentHealth = 3;

    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            
            _currentHealth = value;

            if (_currentHealth < 0)
                _currentHealth = 0;
            _healthBarAnimator.SetInteger(Heart, _currentHealth);

            if (_currentHealth != 0)
                return;
            
            SetPause(true);
            _gameOverUI.SetActive(true);
            _adsButton.interactable = true;
            if (_currentScore > YG2.saves.TopScore)
            {
                YG2.saves.TopScore = _currentScore;
                YG2.saves.SetAnyLeaderboard("TopCatScore", _currentScore);
                YG2.SaveProgress();
            }

            SetAllScoreUI();
        }
    }


    private void Start()
    {
        CurrentHealth = 3;
        _tempSpeedMultiplier = SpeedMultiplier;

        InvokeRepeating(nameof(DifficultUp), 15, 15);
        InvokeRepeating(nameof(RandomAdsSpawner), 25, 25);
    }

    private void DifficultUp()
    {
        if (IsGamePaused || !IsGameRunning)
            return;
        witch.PlayCommonWitch();
        if (SpeedMultiplier >= 1.7f)
            return;

        SpeedMultiplier += 0.1f;
    }

    private void RandomAdsSpawner()
    {
        if (IsGamePaused || !IsGameRunning)
            return;
        _adsButtons.ForEach(i=> i.interactable = true);
        
        switch (Random.Range(0, 3))
        {
            case 0:
                _adsAnimator.SetTrigger(FreezeAds);
                Debug.Log("FreezeAds");
                break;
            case 1:
                if (_currentHealth < 3)
                    _adsAnimator.SetTrigger(HealthAds);
                else
                    RandomAdsSpawner();
                Debug.Log("HealthAds");
                break;
            case 2:
                _adsAnimator.SetTrigger(ScoreAds);
                Debug.Log("ScoreAds");
                break;
        }
    }

    public void SetImmortalityTrue()
    {
        IssImmortality = true;
    }
    
    public void SetImmortalityFalse()
    {
        IssImmortality = false;
    }

    public void PrizeFreeze()
    {
        Invoke(nameof(SetImmortalityFalse), 3);
        StartCoroutine(FreezeGame());
    }

    IEnumerator FreezeGame()
    {
        FreezeSpeedMultiplier = 0.5f;
        yield return new WaitForSeconds(20);
        FreezeSpeedMultiplier = 1;
    }

    public void PrizeHealth()
    {
        if (CurrentHealth < 3)
            CurrentHealth += 1;
    }

    public void PrizeScore()
    {
        Invoke(nameof(SetImmortalityFalse), 3);
        StartCoroutine(ScoreMult());
    }
    
    IEnumerator ScoreMult()
    {
        _scoreMultiplier = 2;
        yield return new WaitForSeconds(20);
        _scoreMultiplier = 0;
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
        StopAllCoroutines();
        CurrentHealth = 3;
        _scoreMultiplier = 0;
        FreezeSpeedMultiplier = 1;
        IssImmortality = false;
        _gameOverUI.SetActive(false);
        IsGamePaused = false;
        SpeedMultiplier = _tempSpeedMultiplier;

        _currentScoreTextGameUI.text = _currentScore.ToString();
        _currentScoreTextUI.text = _currentScore.ToString();

        if (_settingsPanelUI.activeSelf)
            _settingsPanelUI.SetActive(false);
    }
}