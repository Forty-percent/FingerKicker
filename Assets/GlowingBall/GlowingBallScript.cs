using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingBallScript : MonoBehaviour
{
    private Transform target;
    public Rigidbody rigidbody;

    private GameObject healthBar;
    private HealthbarScript healthbarScript;

    public float force = 20f;

    void Start()
    {
        target = GameObject.Find(Constants.Hand).transform;
        rigidbody = gameObject.GetComponent<Rigidbody>();

        healthBar = GameObject.Find("Health bar");
        healthbarScript = healthBar.GetComponent<HealthbarScript>();
    }

    void Update()
    {
        Vector3 targetPos = target.position;
        targetPos.y += 1.5f;

        var vel = (targetPos - transform.position).normalized;
        
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
            if (collision.gameObject.name == "Root_joint")
            {
                //Debug.Log("bla");
                healthbarScript.SetHealth(GlobalVariableStorrage.Health -= 5);
            }

            Destroy(gameObject);
        }
    }
}
