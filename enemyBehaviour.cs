using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{
    public static enemyBehaviour instance;

    private Animator anim;
    public SpriteRenderer sr;

    //vida
    public int maxHealth = 3;
    private int currentHealth;
    public bool isDead;
    public enemyHealthBarBehavior healBar;
    private bool canReciveDamage;


    //movimiento
    public float moveSpeed, moveTime, waitTime;
    private float waitCount, moveCount;
    public Transform leftPoint, rightPoint;
    private bool movinRight;
    public Rigidbody2D rb;


    //ataque
    private bool attackingMode;
    private enemyAttack enemy;


    //muerte
    [Range(0,100)]public float chanceToDrop;
    public GameObject collectible;

    private void Awake() {
        instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        enemy = GetComponent<enemyAttack>();

        moveCount = moveTime;

        leftPoint.parent = null;
        rightPoint.parent = null;

        movinRight = true;

        attackingMode = false;

        currentHealth = maxHealth;
        healBar.SetHealthBar(currentHealth, maxHealth);

        isDead = false;
        canReciveDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
       if(!enemy.inRange) 
        {
            Idle();
            //Debug.Log("Idle");

        } else
        {
            //Debug.Log("No idle"); 
            if(enemy.isGroundEnemy) rb.velocity = new Vector2(0f, rb.velocity.y);
            if(enemy.isFlyEnemy) rb.velocity = new Vector2(0f, 0f);
        } 
       
    }


    private void Idle()
    {
        if(moveCount > 0 && !isDead)
        {
            moveCount -= Time.deltaTime;

            if((transform.position.y < leftPoint.position.y -0.2f) && enemy.isFlyEnemy)
            {
                rb.velocity = new Vector2(rb.velocity.x, moveSpeed);

            } else if(enemy.isFlyEnemy)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);

            } else if((transform.position.y > leftPoint.position.y + 0.2f) && enemy.isFlyEnemy)
            {
                rb.velocity = new Vector2(rb.velocity.x, -moveSpeed);
            }

            if(movinRight)
            {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

            //sr.flipX = false;
            transform.localScale = new Vector3(1, 1, 1);

            if(transform.position.x > rightPoint.position.x)
            {
                movinRight = false;
            }

            }else 
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

                //sr.flipX = true;
                transform.localScale = new Vector3(-1, 1, 1);

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
            if(enemy.isGroundEnemy) rb.velocity = new Vector2(0f, rb.velocity.y);
            if(enemy.isFlyEnemy) rb.velocity = new Vector2(0f, 0f);

            if(waitCount <= 0)
            {
                moveCount = UnityEngine.Random.Range(moveCount * .75f, waitTime * 1.25f);
            }

            anim.SetBool("isMoving", false);
        }
    }

    public void TakeDamage(int damage)
    {
        if(canReciveDamage)
        {
            currentHealth -= damage;

            healBar.SetHealthBar(currentHealth, maxHealth);

            audioManager.instance.PlaySSFX(2);

            enemy.KnockBack();

            anim.SetTrigger("hurt");

            if(currentHealth == 0)
            {
                canReciveDamage = false;
                Die();
            }
        }
    }

    private void Die()
    {
        //Debug.Log("Enemy die");
        anim.SetBool("isDead", true);
        isDead = true;

        float dropSelect = UnityEngine.Random.Range(0,100);

        if(dropSelect <= chanceToDrop)
        {
            Instantiate(collectible, transform.position, transform.rotation);
        }
        audioManager.instance.PlaySSFX(5);

        //GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        
    }

    public void Dissappear()
    {
        Destroy(gameObject);
    }
}
