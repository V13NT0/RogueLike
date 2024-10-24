using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using UnityEngine;

public class enemyAttack : MonoBehaviour
{
    public static enemyAttack instance;

    #region Public Variables
    //public Transform rayCast;
    //public LayerMask raycastMask;
    //public float rayCastLenght;
    public bool isFlyEnemy, isGroundEnemy;
    public float attackDistance; //Distancia minima para atacar ataque
    public float moveSpeed;
    public float timer; //Cooldown entre ataques
    public bool inRange; //Si el objetivo esta a rango
    public float knockBackLengh, knockBackForce;
    public GameObject target;
    public GameObject hotZone;
    public GameObject triggerArea;
    #endregion

    #region Private Variables
    private RaycastHit2D hit;
    private Animator anim;
    private Rigidbody2D rb;
    private float distance;
    private bool attackMode;
    private bool cooling; //Si sigue teniendo cooldown para atacar
    private float intTimer;
    private float knockBackCount;
    private enemyBehaviour behaviour;
    #endregion

    private void Awake() {
        instance = this;
    }

    private void Start() {

        intTimer = timer;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        behaviour = GetComponent<enemyBehaviour>();

    }
    
    void Update()
    {
        if(knockBackCount <= 0)
        {

            if(inRange && !behaviour.isDead)
            {
                EnemyLogic();
            }

            if(!inRange && attackMode) StopAttack();
            /* if(inRange)
            {
                hit = Physics2D.Raycast(rayCast.position, new Vector2(target.transform.position.x-rayCast.position.x, 0f), rayCastLenght, raycastMask);
                RaycastDebugger();
            }

            //Cuando el jugador es detectado
            if(hit.collider != null)
            {
                EnemyLogic();

            } else if (hit.collider == null)
            {
                inRange = false;
                anim.SetBool("isRunning", false);
            }

            if(inRange == false)
            {
                StopAttack();
            } */
        }
        else
            {
                knockBackCount -= Time.deltaTime;
                if(behaviour.transform.localScale == new Vector3(1,1,1))
                {
                    rb.velocity = new Vector2(-knockBackForce, rb.velocity.y);
                }else
                {
                    rb.velocity = new Vector2(knockBackForce, rb.velocity.y);
                }
            }

    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, new Vector2(target.transform.position.x, target.transform.position.y - 0.8f));

        if(distance > attackDistance)
        {
            if(!attackMode) Run();
        }
        else if(distance <= attackDistance)
        {
            anim.SetBool("isRunning", false);
            if(!cooling && !attackMode) Attack();
        }
        
        if(cooling)
        {
            Cooldown();
            anim.SetBool("attack", false);
        }
    }

    public void KnockBack()
    {

        knockBackCount = knockBackLengh;
        if(isGroundEnemy) rb.velocity = new Vector2(0f, knockBackForce);

    }

    void Run()
    {
        anim.SetBool("isRunning", true);
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("attacking"))
        {
            if(isFlyEnemy) 
            {
                Vector2 targetPosition = new Vector2(target.transform.position.x, target.transform.position.y);

                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }

            if(isGroundEnemy)
            {
                //enemyBehaviour.instance.rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

                Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);

                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime); 
                               
            }
            
        }
    }

    public void Attack()
    {
        timer = intTimer;
        attackMode = true;
        anim.SetBool("isRunning", false);
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

    

    /* private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            target = other.gameObject;
            inRange = true;
            Flip();
        }
    } */


    /* private void RaycastDebugger()
    {
        if(distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, new Vector2(target.transform.position.x-rayCast.position.x, 0f) * rayCastLenght, UnityEngine.Color.red);
            //Debug.Log("Enemigo Detectado");
        }
        else if (distance < attackDistance)
        {
            Debug.DrawRay(rayCast.position, new Vector2(target.transform.position.x-rayCast.position.x, 0f) * rayCastLenght, UnityEngine.Color.green);
            //Debug.Log("Enemigo al alcance");
        }
    } */

    void Cooldown()
    {
        timer -= Time.deltaTime;
        attackMode = false;

        if(timer <= 0 && cooling)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x < target.transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //transform.eulerAngles = rotation;
    }
}
