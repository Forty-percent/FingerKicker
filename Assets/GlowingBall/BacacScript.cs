using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacacScript : MonoBehaviour
{
    private Transform target;
    private Vector3 newLocation;

    public GameObject projectile;
    public GameObject spawnArea;
    public ParticleSystem fog;
    public Animator animator;

    private bool move = false;
    public float speed = 2f;

    void Start()
    {
        Invoke("LaunchAnimation", 2f);
        Invoke("Activity", 0f);
        animator = gameObject.GetComponent<Animator>();
        target = GameObject.Find(Constants.Hand).transform;
        fog.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            Move();
        }
        else
        {
            transform.LookAt(target.position, transform.up);
        }
    }

    void Move()
    {
        animator.SetBool(Constants.Walk, true);
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, newLocation, step);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, (newLocation - transform.position).normalized, step, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);

        if (transform.position == newLocation)
        {
            animator.SetBool(Constants.Walk, false);
            move = false;
        }
    }

    void Activity()
    {
        int activityNum = Random.Range(1, 4);
        Debug.Log($"Activity num {activityNum}");

        switch (activityNum)
        {
            case 1:
                NewLocation();
                break;
            case 2:
                Destroy(gameObject);
                break;
        }

        Invoke("Activity", Random.Range(20f, 40f));
    }

    void NewLocation()
    {
        move = true;
        newLocation = Common.RandomPointOnPlane(spawnArea);
    }

    public void LaunchAnimation()
    {
        if (!move)
        {
            int animationNum = Random.Range(1, 3);
            Debug.Log($"Animation num ${animationNum}");

            switch (animationNum)
            {
                case 1:
                    LaunchProjectileAnimation();
                    break;
                case 2:
                    LaunchJumpProjectileAnimation();
                    break;
            }
        }

        Invoke("LaunchAnimation", Random.Range(6f, 20f));
    }

    private void LaunchProjectileAnimation()
    {
        animator.SetBool(Constants.Shoot, true);
    }

    private void LaunchJumpProjectileAnimation()
    {
        animator.SetBool(Constants.JumpShoot, true);
    }

    private void EndAnimations()
    {
        animator.SetBool(Constants.Shoot, false);
        animator.SetBool(Constants.JumpShoot, false);
    }

    public void LaunchProjectile()
    {
        Vector3 position = transform.position;
        position.y = 3;
        Instantiate(projectile, position, transform.rotation);
        EndAnimations();
    }

    public void LaunchJumpProjectile()
    {
        Vector3 position = transform.position;
        position.y = 1;
        Instantiate(projectile, position, transform.rotation);
        EndAnimations();
    }
}


