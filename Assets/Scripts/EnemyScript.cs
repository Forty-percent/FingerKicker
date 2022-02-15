using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//https://blog.studica.com/unity-tutorial-animator-controllers

public class EnemyScript : MonoBehaviour
{
    public float speed = 100.0f;

    private Transform target;
    private GameObject healthBar;
    private HealthbarScript healthbarScript;

    private GameObject score;
    private Text scoreText;
    
    public ParticleSystem fog;

    private Animator animator;
    public Transform Ruka;

    private bool run;
    private bool attack;
    private bool die;
    private bool jump;
    private bool end;

    private bool setScore;

    void Start()
    {
        speed = 10.0f;

        setScore = false;

        animator = gameObject.GetComponent<Animator>();

        target = GameObject.Find("invisibleWall").transform;

        healthBar = GameObject.Find("Health bar");
        healthbarScript = healthBar.GetComponent<HealthbarScript>();

        animator.Play("jump");
        gameObject.GetComponent<Rigidbody>().AddForce(0, 7, 0, ForceMode.Impulse);

        AddEvent(GetClipIndexByName("Cross Punch"), 0.9f, "DealDamage", 0);
        AddEvent(GetClipIndexByName("Zombie Death"), 0.9f, "ResizeCollider", 0);
        Instantiate(fog, transform.position, transform.rotation);

        score = GameObject.Find("ScoreText");
        scoreText = score.GetComponent<Text>();
    }

    public void DealDamage()
    {
        healthbarScript.SetHealth(GlobalVariableStorrage.Health -= 1);
    }

    public void ResizeCollider()
    {
        var boxColider = gameObject.GetComponent<BoxCollider>();
        boxColider.size = new Vector3(boxColider.size.x, 0.1f, boxColider.size.z);
    }

    void Update()
    {
        /*if (Vector3.Dot((transform.position - Ruka.position), Ruka.forward) > 0)
        {
            //Debug.Log("bla");
            Destroy(gameObject);
        }*/

        if (run && !die && !end && !GlobalVariableStorrage.GameOver)
        {
            Vector3 targetDirection = target.position - transform.position;

            float step = speed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);

            transform.rotation = Quaternion.LookRotation(newDirection);
        }


        if (Vector3.Distance(transform.position, target.position) < 6.0f && !die && !end && !GlobalVariableStorrage.GameOver)
        {
            animator.SetBool("Attack", true);
            run = false;
            attack = true;
        }
        else if (Vector3.Distance(transform.position, target.position) >= 6.0f && !die && !GlobalVariableStorrage.GameOver)
        {
            animator.SetBool("Attack", false);
            run = true;
            attack = false;
        }


        if (GlobalVariableStorrage.GameOver)
        {
            end = true;
            run = false;
            attack = false;
            //die = false;

            //animator.SetBool("Run", false);
            animator.SetBool("Attack", false);
            //animator.SetBool("Die", false);

            animator.SetBool("End", true);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.transform.root.name == "ruka")
        {
            if (GlobalVariableStorrage.DeltaFlick > 0.1)
            {
                Vector3 directionVector = -Ruka.forward;
                directionVector.y = 0.8f;

                gameObject.GetComponent<Rigidbody>().AddForce(directionVector * 15, ForceMode.Impulse);
                animator.SetBool("Attack", false);
                animator.SetBool("Die", true);
                die = true;
                run = false;
                attack = false;

                if (!setScore)
                {
                    GlobalVariableStorrage.Score++;
                    scoreText.text = GlobalVariableStorrage.Score.ToString();
                    setScore = true;
                }
            }

        }

        if (other.gameObject.transform.root.name == "windowcollision")
        {
            GlobalVariableStorrage.Score += 100;
            scoreText.text = GlobalVariableStorrage.Score.ToString();
        }
    }

    void AddEvent(int Clip, float time, string functionName, float floatParameter)
    {
        AnimationEvent animationEvent = new AnimationEvent();
        animationEvent.functionName = functionName;
        animationEvent.floatParameter = floatParameter;
        animationEvent.time = time;
        AnimationClip clip = animator.runtimeAnimatorController.animationClips[Clip];
        clip.AddEvent(animationEvent);
    }

    int GetClipIndexByName(string name)
    {
        int i = 0;
        foreach (AnimationClip animationClip in animator.runtimeAnimatorController.animationClips)
        {
            if (animationClip.name == name)
                return i;
            i++;
        }

        return 0;
    }
}
