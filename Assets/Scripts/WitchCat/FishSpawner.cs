using UnityEngine;
using Random = UnityEngine.Random;

public class FishSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameController gameController;
    [SerializeField] private GameObject fishPrefab;
    [SerializeField] private Player player;
    void Start()
    {
        InvokeRepeating(nameof(Spawner), 15,15);
    }

    private void Spawner()
    {
        if(gameController.IsGamePaused || !gameController.IsGameRunning || gameController.CurrentHealth >= 3)
            return;
        gameObject.transform.Rotate(0,0,Random.Range(0,360));
        
        GameObject tempFish = GameObject.Instantiate(fishPrefab, spawnPoint.position, Quaternion.identity);
        Fish fish = tempFish.GetComponent<Fish>();
        fish.moveDirection =
            (player.transform.position - spawnPoint.transform.position).normalized;
        fish.GameController = gameController;
    }
}
