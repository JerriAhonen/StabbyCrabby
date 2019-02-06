using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour {
    
    public GameObject bulletPrefab;

    private Vector3 spawnPoint0 = new Vector3(20, 0.2f, 20);
    private Vector3 spawnPoint1 = new Vector3(-20, 0.2f, 20);
    private Vector3 spawnPoint2 = new Vector3(20, 0.2f, -20);
    private Vector3 spawnPoint3 = new Vector3(-20, 0.2f, -20);

    private Vector3[] spawnPoints = new Vector3[4];

    private void Start()
    {
        spawnPoints[0] = spawnPoint0;
        spawnPoints[1] = spawnPoint1;
        spawnPoints[2] = spawnPoint2;
        spawnPoints[3] = spawnPoint3;

        DelayedBulletSpawn();
    }

    /// <summary>
    /// Use this to spawn new bullet on one of the platforms
    /// </summary>
    public void SpawnBullet()
    {
        int spawnPoint = Random.Range(0, 4);

        GameObject go = Instantiate(bulletPrefab, spawnPoints[spawnPoint], Quaternion.identity);
        go.GetComponent<BulletPickup>().bs = this;
    }

    /// <summary>
    /// Spawn bullet with a 2 second delay on one of the platforms
    /// </summary>
    public void DelayedBulletSpawn()
    {
        Invoke("SpawnBullet", 2.0f);
    }
}
