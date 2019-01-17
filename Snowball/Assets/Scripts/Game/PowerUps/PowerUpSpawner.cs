using UnityEngine;
using System.Collections.Generic;
using System.Collections;

//Coupling : None, there is no coupling between this class and other classes in the codebase.
public class RandomGridSpawner : ISpawner
{   
    private GameObject[] gameObjectSpawnPool;
    private Transform spawnPlatform;
    private float spawnInterval;
    public bool isSpawning;

    public RandomGridSpawner(GameObject[] gameObjectSpawnPool,Transform spawnPlatform, float spawnInterval,bool isSpawning = true) {
        this.gameObjectSpawnPool = gameObjectSpawnPool;
        this.spawnPlatform = spawnPlatform;
        this.spawnInterval = spawnInterval;
        this.isSpawning = isSpawning;
    }

    public IEnumerator SpawnLoop()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(spawnInterval);
            GameObject objectToSpawn = gameObjectSpawnPool[Random.Range(0, gameObjectSpawnPool.Length)];
            Vector3 spawnPositon = DetermineSpawnPosition(spawnPlatform);
            SpawnGameObject(objectToSpawn, spawnPositon);
        }
        yield break;
    }

    public void SpawnGameObject(GameObject prefabToSpawn, Vector3 location)
    {   
        GameObject.Instantiate(prefabToSpawn, location,Quaternion.identity);
    }

    private Vector3 DetermineSpawnPosition(Transform referenceTransform) {

        float maxHorizontalLength = referenceTransform.position.x + referenceTransform.localScale.x / 2;
        float minHorizontalLength = referenceTransform.position.x - referenceTransform.localScale.x / 2;
        float maxDepthLength = referenceTransform.position.z + referenceTransform.localScale.z / 2;
        float minDepthLength = referenceTransform.position.z - referenceTransform.localScale.z / 2;
        float spawnHeight = referenceTransform.position.y + referenceTransform.localScale.y / 2 + 0.15f;
        Debug.Log(maxDepthLength + " " + minDepthLength);

        Vector3 spawnPositon = new Vector3(Random.Range(minHorizontalLength,maxHorizontalLength),spawnHeight,
            Random.Range(minDepthLength, maxDepthLength));
        
        return spawnPositon;
    }


}
