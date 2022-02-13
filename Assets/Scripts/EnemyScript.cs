using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://blog.studica.com/unity-tutorial-animator-controllers

public class EnemyScript : MonoBehaviour
{
    public float speed = 100.0f;

    private Transform target;
    private Transform ruka;

    private Animator animator;

    private bool run;
    private bool attack;
    private bool die;

    int attackAnimation;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        ruka = gameObject.GetComponent<Transform>().Find("ruka");

        attackAnimation = Animator.StringToHash("Cross Punch");

        speed = 10.0f;

        transform.position = new Vector3(397, 0, 490);

        target = GameObject.Find("invisibleWall").transform;
    }


    void Update()
    {
        if (run && !die)
        {
            Vector3 targetDirection = target.position - transform.position;

            float step = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);

            transform.rotation = Quaternion.LookRotation(newDirection);
        }


        if (Vector3.Distance(transform.position, target.position) < 6.0f && !die)
        {
            animator.SetBool("Attack", true);
            run = false;
            attack = true;
        }
        else if (Vector3.Distance(transform.position, target.position) >= 6.0f && !die)
        {
            animator.SetBool("Attack", false);
            run = true;
            attack = false;
        }

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.transform.root.name == "ruka")
        {

            //Debug.Log(other.relativeVelocity);

            Debug.Log(GlobalVariableStorrage.DeltaFlick);

            if (GlobalVariableStorrage.DeltaFlick > 0.6)
            {
                gameObject.GetComponent<Rigidbody>().AddRelativeForce(0, 1000, -2500);
                animator.SetBool("Attack", false);
                animator.SetBool("Die", true);
                die = true;
                run = false;
                attack = false;
            }

        }
    }
}