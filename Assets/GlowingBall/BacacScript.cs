using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacacScript : MonoBehaviour
{
    private Transform target;
    public GameObject projectile;
    public Animator animator;
    void Start()
    {
        Invoke("LaunchAnimation", 2f);
        animator = gameObject.GetComponent<Animator>();
        target = GameObject.Find(Constants.Hand).transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.position, transform.up);
    }

    public void LaunchAnimation()
    {
        int animationNum = Random.Range(1, 3);
        //Debug.Log(animationNum);

        switch (animationNum)
        {
            case 1:
                LaunchProjectileAnimation();
                break;
            case 2:
                LaunchJumpProjectileAnimation();
                break;
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


