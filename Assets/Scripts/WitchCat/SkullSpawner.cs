using System;
using UnityEngine;
using Random = UnityEngine.Random;


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
        _animator.SetFloat(SpeedAnim, 0.9f * gameController.SpeedMultiplier);
    }

    public void SpawnerAnimEvent()
    {
        if (gameController.IsGamePaused || !gameController.IsGameRunning)
            return;
        
        GameObject tempSkull = GameObject.Instantiate(skullPrefab, transform.position, Quaternion.identity);
        
        Skull skull = tempSkull.GetComponent<Skull>();
        Random.InitState((int)DateTime.Now.Ticks);
        // if (Random.Range(0, 100) < 50)
        // {
        //     // skull.moveDirection =
        //     //     (player.transform.position - transform.transform.position).normalized;
        //     skull.moveDirection = GetRandomDirection();
        // }
        // else
        // {
        //     skull.moveDirection = GetRandomDirection();
        // }
        skull.moveDirection = GetRandomDirection();
        
        skull.GameController = gameController;
    }
    
    Vector2 GetRandomDirection()
    {
        Vector2 randomVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        
        return randomVector.normalized;
    }
}