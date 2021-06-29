using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour
{
    public GameObject enemy;
    public GameObject pos;
    private GameObject spawnedEnemy;

    public void Spawn()
    {
        Debug.Log("Spawning");
        Instantiate(enemy, pos.transform.position, Quaternion.identity);
    }

    public void DeSpawn()
    {
        Destroy(spawnedEnemy, 0);
    }
}
