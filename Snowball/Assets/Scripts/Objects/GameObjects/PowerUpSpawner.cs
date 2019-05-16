using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    private RandomGridSpawner randomGridSpawner;
    [SerializeField]
    private GameObject[] gameObjectSpawnPool;
    [SerializeField]
    private Transform spawnPlatform;
    [SerializeField]
    private float spawnInterval;

    private void Start()
    {
        randomGridSpawner = new RandomGridSpawner(gameObjectSpawnPool, spawnPlatform, spawnInterval);
        StartCoroutine(randomGridSpawner.SpawnLoop());
    }
}
