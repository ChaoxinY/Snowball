using UnityEngine;
using System.Collections;

public interface ISpawner
{
    void SpawnGameObject(GameObject prefabToSpawn,Vector3 location);
    IEnumerator SpawnLoop();
}
