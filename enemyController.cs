using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using UnityEngine;
using static UnityEngine.Random;

public class enemyController : MonoBehaviour
{

    public Animator anim;

    public float moveSpeed;

    public Transform leftPoint, rightPoint, rayCast;
    public Transform[] points;
    public LayerMask raycastMask;
    public float rayCastLenght, distanceToAttackPlayer, chaseSpeed, coolDown;

    private RaycastHit2D hit;
    private GameObject target;
    private float distance, intTimer;
    private bool attackMode, inRange, cooling;

    private bool movinRight;

    private Rigidbody2D rb;

    public SpriteRenderer sr;

    public float moveTime, waitTime;
    private float moveCount, waitCount;

    public int maxHealth = 3;
    private int currentHealth;

    [Range(0,100)]public float chanceToDrop;

    public GameObject collectible;

    private void Awake() {
        intTimer = coolDown;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        for(int i = 0; i < points.Length; i++)
        {
            points[i].parent = null;
        }

        leftPoint.parent = null;
        rightPoint.parent = null;

        movinRight = true;

        moveCount = moveTime;

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {


        if(inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, Vector2.right, rayCastLenght, raycastMask);

            RaycastDebugger();
        }

        //When player is detected
        if(hit.collider != null)
        {
            EnemyLogic();
        }else if (hit.collider == null)
        {
            inRange = false;
        }

        if(inRange == false)
        {
            Idle();
            
        }

        
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        anim.SetTrigger("hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Debug.Log("Enemy die");

        StartCoroutine(DeadCo());
        
    }

    private IEnumerator DeadCo()
    {
        anim.SetBool("isDead", true);

        float dropSelect = UnityEngine.Random.Range(0,100);

        if(dropSelect <= chanceToDrop)
        {
            Instantiate(collectible, transform.position, transform.rotation);
        }
        audioManager.instance.PlaySSFX(5);

        yield return new WaitForSeconds(1f);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Dissappear();
    }

    private void Idle(){

        if(moveCount > 0)
        {
            moveCount -= Time.deltaTime;

            if(movinRight)
            {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

            //sr.flipX = false;
            //transform.localScale = new Vector3(1, 1, 1);
            transform.Rotate(Vector2.up, 180);

            if(transform.position.x > rightPoint.position.x)
            {
                movinRight = false;
            }

            }else 
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

                //sr.flipX = true;
                //transform.localScale = new Vector3(-1, 1, 1);
                transform.Rotate(Vector2.up, 180);

                if(transform.position.x < leftPoint.position.x)
                {
                    movinRight = true;
                }
            }

            if(moveCount <= 0)
            {
                waitCount = UnityEngine.Random.Range(waitTime * .75f, waitTime * 1.25f);
            }

            anim.SetBool("isMoving", true);

        }else if(waitCount > 0)
        {
            waitCount -= Time.deltaTime;
            rb.velocity = new Vector2(0f, rb.velocity.y);

            if(waitCount <= 0)
            {
                moveCount = UnityEngine.Random.Range(moveCount * .75f, waitTime * 1.25f);
            }

            anim.SetBool("isMoving", false);
        }
    }

    private void Dissappear()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.tag == "Player")
        {
            target = other.gameObject;
            inRange = true;
        }

    }
    private void RaycastDebugger()
    {
        if(distance > distanceToAttackPlayer)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLenght, UnityEngine.Color.red);
        }
        else if (distance < distanceToAttackPlayer)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLenght, UnityEngine.Color.green);
        }
    }

    public void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if(distance > distanceToAttackPlayer)
        {
            Move();
            StopAttack();
        }
        else if(distance < distanceToAttackPlayer && cooling == false)
        {
            Attack();
        }

        if(cooling)
        {
            Cooldown();
            anim.SetBool("attack", false);
        }
    }

    public void Move()
    {
        anim.SetBool("isMoving", true);
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("attacking"))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, chaseSpeed * Time.deltaTime);
        }
    }

    public void Attack()
    {
        coolDown = intTimer;
        attackMode = true;
        anim.SetBool("isMoving", false);
        anim.SetBool("attack", true);
    }

    public void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("attack", false);

    }

    public void TriggerCooling()
    {
        cooling = true;
    }

    void Cooldown()
    {
        coolDown -= Time.deltaTime;

        if(coolDown <= 0 && cooling && attackMode)
        {
            cooling = false;
            coolDown = intTimer;
        }
    }
}
