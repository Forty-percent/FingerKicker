using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
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
        Instantiate(enemy, RandomPointOnPlane(gameObject), transform.rotation);
    }

    Vector3 RandomPointOnPlane(GameObject plane)
    {
        List<Vector3> VerticeList = new List<Vector3>(plane.GetComponent<MeshFilter>().sharedMesh.vertices);
        Vector3 leftTop = plane.transform.TransformPoint(VerticeList[0]);
        Vector3 rightTop = plane.transform.TransformPoint(VerticeList[10]);
        Vector3 leftBottom = plane.transform.TransformPoint(VerticeList[110]);
        Vector3 rightBottom = plane.transform.TransformPoint(VerticeList[120]);
        Vector3 XAxis = rightTop - leftTop;
        Vector3 ZAxis = leftBottom - leftTop;
        return leftTop + XAxis * Random.value + ZAxis * Random.value;
    }
}
