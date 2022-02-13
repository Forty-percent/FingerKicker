using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacacScript : MonoBehaviour
{
    public GameObject projectile;
    public Animator animator;
    void Start()
    {
        InvokeRepeating("LaunchAnimation", 2f, 6f);
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LaunchAnimation()
    {
        int animationNum = Random.Range(1, 3);
        Debug.Log(animationNum);

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


