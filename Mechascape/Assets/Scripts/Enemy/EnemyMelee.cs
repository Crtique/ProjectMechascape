using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyMelee : MonoBehaviour
{
    #region Public Variables
    public Transform rayCast;
    public LayerMask rayCastMask;
    public float rayCastLength;
    public float attackDistance; // Minimum distance required for attack
    public float moveSpeed;
    public float tba; // Time Between Attacks
    #endregion

    #region Private Variables
    private RaycastHit2D hitPlayer;
    private GameObject target;
    private Animator anim;
    private float distance; // Stores distance between enemy and Player
    private bool attack;
    private bool inRange; // Checks if Player is in range
    private bool cooldown = false; // Checks if the enemy is has attacked and is "cooling down"
    private float intTimer;
    #endregion

    void Awake()
    {
        intTimer = tba; // Stores initial value of tba
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        if(inRange)
        {
            hitPlayer = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLength, rayCastMask);
            RaycastDebugger();

            //When the enemy detects the Player
            if(hitPlayer.collider != null)
            {
                EnemyLogic();
            }
            else
            {
                inRange = false;
            }
        }
        else
        {
            anim.SetBool("canWalk", true);
            if (attack || cooldown)
            {
                StopAttack();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            target = trig.gameObject;
            inRange = true;
        }
    }

    void RaycastDebugger()
    {
        if(distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.red);
        }
        else if(attackDistance > distance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.green);
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if(distance > attackDistance)
        {
            Move();
            if (attack || cooldown)
            {
                StopAttack();
            }
        }
        else if(!cooldown)
        {
            Attack();
        }
        else
        {
            Debug.Log("Col");
            anim.SetBool("Attack", false);
            Cooldown();
        }
    }

    void Move()
    {
        Debug.Log("Moving");
        anim.SetBool("canWalk", true);
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Claw Attack"))
        { 
           Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);
           transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        Debug.Log("Attack");
        tba = intTimer; // Resets tba when the Player enters range
        attack = true; // Checks if the enemy is still capable of attacking

        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
    }

    void Cooldown()
    {
        tba -= Time.deltaTime;

        if(tba <= 0)
        {
            attack = false;
            cooldown = false;
            tba = intTimer;
            anim.SetBool("Attack", false);
        }
    }

    void StopAttack()
    {
        Debug.Log("Stop");
        cooldown = false;
        attack = false;
        anim.SetBool("Attack", false);
    }

    public void TriggerCooling()
    {
        cooldown = true;
    }
}