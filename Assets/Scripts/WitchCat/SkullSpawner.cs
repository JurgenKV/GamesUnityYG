using System;
using UnityEngine;

public class SkullSpawner : MonoBehaviour
{
    private static readonly int SpeedAnim = Animator.StringToHash("Speed");

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject skullPrefab;
    [SerializeField] private Player player;
    [SerializeField] private Animator _animator;

    private void Update()
    {
        _animator.SetFloat(SpeedAnim, 1 * gameController.SpeedMultiplier);
    }

    public void SpawnerAnimEvent()
    {
        if (gameController.IsGamePaused || !gameController.IsGameRunning)
            return;
        
        GameObject tempSkull = GameObject.Instantiate(skullPrefab, transform.position, Quaternion.identity);
        Skull skull = tempSkull.GetComponent<Skull>();
        skull.moveDirection =
            (player.transform.position - transform.transform.position).normalized;
        skull.GameController = gameController;
    }
}