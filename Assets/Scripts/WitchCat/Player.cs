using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public enum ColorType
{
    Yellow,
    Red,
    Green,
    Blue
}

public class Player : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Eat = Animator.StringToHash("Eat");
    [SerializeField] private TMP_Text helpText;
    private InputSystem_Actions controls; 
    private GameController _gameController;
    private bool _firstTap = true;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private AudioSource audioSource;

    private float _animSpeed = 1;
    void Awake()
    {
        controls = new InputSystem_Actions();
        controls.Player.Attack.started += ctx => Attack();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    private void Start()
    {
        _gameController = FindAnyObjectByType<GameController>();
        _animSpeed = 2 * _gameController.SpeedMultiplier;
    }

    private void Update()
    {
        if(_gameController.SpeedMultiplier == 0)
            _playerAnimator.SetFloat(Speed, 0);
        else
            _playerAnimator.SetFloat(Speed, _animSpeed);
    }

    void Attack()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        
        if (_gameController.IsGamePaused || !_gameController.IsGameRunning)
            return;
           
        CheckFirstTap();
        
        if (_animSpeed > 0)
            _animSpeed = -2 * _gameController.SpeedMultiplier;
        else
            _animSpeed = 2 * _gameController.SpeedMultiplier;
        
        _playerAnimator.SetFloat(Speed, _animSpeed);
    }

    private void CorrectSpeed()
    {
        
    }

    public void PlayEatAnim()
    {
        _playerAnimator.SetTrigger(Eat);
        audioSource.Play();
    }

    private void CheckFirstTap()
    {
        if(!_firstTap)
            return;
        _firstTap = false;
        _gameController.witch.StartWitch();
        StartCoroutine(FadeOutCor());
    }

    private IEnumerator FadeOutCor()
    {
        while (helpText.color.a > 0)
        {

            helpText.color = new Color(helpText.color.r, helpText.color.g, helpText.color.b, Mathf.Max(helpText.color.a - 1f * Time.deltaTime, 0f));

            yield return null; 
        }
        helpText.gameObject.SetActive(false);
    }
}
