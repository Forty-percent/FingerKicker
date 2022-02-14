using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingBallScript : MonoBehaviour
{
    private Transform target;
    public Rigidbody rigidbody;

    public float force = 20f;

    void Start()
    {
        target = GameObject.Find(Constants.Hand).transform;
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        var vel = (target.position - transform.position).normalized;
        force -= 0.01f;
        rigidbody.velocity = vel * force;
    }

    void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.name == Constants.Hand)
        {
            Destroy(gameObject);
            Debug.Log("Collided");
        }*/

        if (collision.gameObject.transform.root.name == Constants.Hand 
            || collision.gameObject.transform.root.name == Constants.InvisibleWall)
        {
            Destroy(gameObject);
            //Debug.Log("Collided");
        }
    }
}
