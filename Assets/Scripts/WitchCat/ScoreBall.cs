using System;
using UnityEngine;

public class ScoreBall : MonoBehaviour
{
   private static readonly int IsActive = Animator.StringToHash("IsActive");
   private static readonly int Speed = Animator.StringToHash("Speed");
   [SerializeField] private int spawnDelay = 2;
   private Animator _animator;
   private GameController _gameController;
   private Collider2D _collider;
   
   private void Start()
   {
      _gameController = FindFirstObjectByType<GameController>();
      _animator = GetComponent<Animator>();
      _collider = GetComponent<Collider2D>();
      Invoke(nameof(ActivateBall), spawnDelay);
   }

   private void Update()
   {
      _animator.SetFloat(Speed, 1 * _gameController.SpeedMultiplier);
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      Player player = other.GetComponent<Player>();
      if(player == null) return;

      _collider.enabled = false;
      if(_gameController.CurrentHealth > 0)
         _gameController.CurrentScore += 1;
      _animator.SetBool(IsActive, false);
      player.PlayEatAnim();
      Invoke(nameof(ActivateBall), 3);
   }

   public void ActivateBall()
   {
      _animator.SetBool(IsActive, true);
   }

   public void ActivateCollider()
   {
      _collider.enabled = true;
   }
}
