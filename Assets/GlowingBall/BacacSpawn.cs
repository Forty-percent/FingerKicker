using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacacSpawn : MonoBehaviour
{
    public GameObject enemy;
    void Start()
    {
        Invoke("OnSpawn", 2f);
    }

    void OnSpawn()
    {
        Instantiate(enemy, Common.RandomPointOnPlane(gameObject), transform.rotation);
        Invoke("OnSpawn", Random.Range(30f, 50f));
    }
}
