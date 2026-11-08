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
    private bool cooldown; // Checks if the enemy is has attacked and is "cooling down"
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

            //When the enemy detects the Player
            if(hitPlayer.collider != null)
            {
                EnemyLogic();
            }
            else if(hitPlayer.collider == null)
            {
                inRange = false;
            }
        }
        else
        {
            anim.SetBool("canWalk", true);
            StopAttack();
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

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if(distance > attackDistance)
        {
            Move();
            StopAttack();
        }
        else if(attackDistance >= distance && cooldown != false)
        {
            Attack();
            Debug.Log("Attacking");
        }

        if(cooldown)
        {
            anim.SetBool("Attack", false);
            Cooldown();
        }
    }

    void Move()
    {
        anim.SetBool("canWalk", true);
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Claw Attack"))
       { 
           Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);
           transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
       }
    }

    void Attack()
    {
        tba = intTimer; // Resets tba when the Player enters range
        attack = true; // Checks if the enemy is still capable of attacking

        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
    }

    void Cooldown()
    {
        tba -= Time.deltaTime;

        if(tba <= 0 && cooldown && attack)
        {
            cooldown = false;
            tba = intTimer;
        }
    }

    void StopAttack()
    {
        cooldown = false;
        attack = false;
        anim.SetBool("Attack", false);
    }

    public void TriggerCooling()
    {
        cooldown = true;
    }
}
