using System;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public ColorType ColorType;
    private GameController _gameController;
    private AudioSource _audioSource;
    private void Start()
    {
        _gameController = FindAnyObjectByType<GameController>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Bullet bullet = other.GetComponent<Bullet>();
        if(bullet == null)
            return;
        
        if (bullet.ColorType.Equals(ColorType))
        {
            if(_gameController.CurrentHealth > 0)
                _gameController.CurrentScore += 1;
            bullet.Delete(false);
        }
        else
        {
            if(_gameController.CurrentHealth > 0)
                _gameController.CurrentHealth -= 1;
            
            _audioSource.Play();
            bullet.Delete(true);
        }
    }
}
