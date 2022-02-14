using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public bool spawn = true;
    void Start()
    {
        Invoke("OnSpawnEnemy", 2f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSpawnEnemy()
    {
        SpawnEnemy();
        //Debug.Log("Spawned enemy");
        Invoke("OnSpawnEnemy", Random.Range(1f, 10f)); // promjeniti interval min i max sec do iduceg spawna ako treba
    }

    void SpawnEnemy()
    {
        if (spawn)
        {
            Instantiate(enemy, Common.RandomPointOnPlane(gameObject), transform.rotation);
        }
    }
}
