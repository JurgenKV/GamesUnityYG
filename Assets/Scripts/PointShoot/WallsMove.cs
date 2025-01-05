using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WallsMove : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Big = Animator.StringToHash("Big");
    private static readonly int Normal = Animator.StringToHash("Normal");
    private static readonly int Little = Animator.StringToHash("Little");
    private static readonly int Indicator = Animator.StringToHash("Indicator");
    [SerializeField] private Animator _wallAnimator;
    [SerializeField] private Animator _crosshairAnimator;
    [SerializeField] private GameController _gameController;
    
    private float _speed = -0.3f;
    private int _prevChoice = -1;
    private void Start()
    {
        _wallAnimator = GetComponent<Animator>();
        _wallAnimator.SetFloat(Speed, _speed);
       // InvokeRepeating(nameof(RandomState), 15,15);
        InvokeRepeating(nameof(PlayIndicator), 12,12);
    }

    private void OnDisable()
    {
        _wallAnimator.SetFloat(Speed, 0);
    }

    private void OnEnable()
    {
        _wallAnimator.SetFloat(Speed, _speed);
    }

    private void RandomState()
    {
        if(!_gameController.IsGameRunning)
            return;
        
        Random.InitState((int)Time.time);
        int choice = Random.Range(0, 5);
        Debug.Log("Wall random: " + choice);
        
        if(_prevChoice.Equals(choice))
            return;
        
        SetChoice(choice);
    }

    private void SetChoice(int choice)
    {
        switch (choice)
        {
            case 0:
                _wallAnimator.SetTrigger(Big);
                break;
            case 1:
                _wallAnimator.SetTrigger(Normal);
                break;
            case 2:
                _wallAnimator.SetTrigger(Little);
                break;
            case 3:
                ChangeAnimatorSpeed();
                break;
            case 4:
                ChangeAnimatorSpeed();
                break;
            
            default:
                break;
        }

        _prevChoice = choice;
    }

    private void PlayIndicator()
    {
        _wallAnimator.SetTrigger(Indicator);
        
        Invoke(nameof(RandomState), 3);
    }

    private void ChangeAnimatorSpeed()
    {
        if (_speed < 0)
        {
            _speed = Random.Range(30, 100) / 100f;
            _crosshairAnimator.SetFloat(Speed, -1f);
        }
        else
        {
            _speed = Random.Range(-30, -100) / 100f;
            _crosshairAnimator.SetFloat(Speed, 1f);
        }
        _wallAnimator.SetFloat(Speed, _speed);
    }
    
    
}
