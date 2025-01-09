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
    [SerializeField] private float speedMultiplier = 2;
    private InputSystem_Actions controls; 
    private GameController _gameController;
    private bool _firstTap = true;
    private AudioSource _audioSource;
    [SerializeField] private Animator _playerAnimator;

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
        _audioSource = GetComponent<AudioSource>();
    }

    void Attack()
    {
        if (_gameController.IsGamePaused || !_gameController.IsGameRunning)
            return;
        
        CheckFirstTap();
        
        if (_animSpeed > 0)
            _animSpeed = -1 * speedMultiplier;
        else
            _animSpeed = 1 * speedMultiplier;
        
        _playerAnimator.SetFloat(Speed, _animSpeed);
    }

    public void PlayEatAnim()
    {
        _playerAnimator.SetTrigger(Eat);
    }

    private void CheckFirstTap()
    {
        if(!_firstTap)
            return;
        _firstTap = false;
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
