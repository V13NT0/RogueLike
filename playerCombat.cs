using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{

    public static playerCombat instance;
    
    private Animator anim;

    public Transform attackPoint;
    public LayerMask enemyLayer;
    public float attackRange;
    public int attackDamage;
    public int attackRate;
    public float waitForAttack;
    private float waitForAttackCount; 
    private bool canAttack;
    private float nextAttackTime = 0f;

    public AudioSource au;
    public AudioClip[] sonido;

    private int combo;
    public Boolean attacking;
    public cooldownBarBehavior cooldownBar;

    private void Awake() {
        instance = this;
    }



    void Start()
    {
        anim = GetComponent<Animator>();
        au = GetComponent<AudioSource>();
        cooldownBar = GetComponentInChildren<cooldownBarBehavior>();
        waitForAttackCount = 0;
        canAttack = true;
        
    }

    // Update is called once per frame
    void Update()
    {

        if(playerController.instance.isFlip)
        {
            attackPoint.position = new Vector2(playerController.instance.transform.position.x -1, attackPoint.position.y);
        } else {
            attackPoint.position = new Vector2(playerController.instance.transform.position.x +1, attackPoint.position.y);
        }
        
        if(!canAttack)
        {
            waitForAttackCount += Time.deltaTime;
            cooldownBar.CooldownBar(waitForAttackCount, waitForAttack);
            if(waitForAttackCount >= waitForAttack)
            {
                canAttack = true;
                waitForAttackCount = 0;
            }
        } 
        //Combo();

        /*if(Time.time >= nextAttackTime){
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }*/
        
    }


    public void Combo()
    {
        if(Time.time >= nextAttackTime && canAttack){
            if(Input.GetKeyDown(KeyCode.Space) && !attacking)
            {
                //Attack();
                nextAttackTime = Time.time + 1f / attackRate;
                attacking = true;
                anim.SetTrigger("attack"+combo);
                au.clip = sonido[combo];
                au.Play();
            }
        }

    }

    public void StartCombo()
    {   
        attacking = false;
        if (combo < 3)
        {
            combo++;
        }

    }

    public void FinishCombo()
    {
        canAttack = false;
        attacking = false;
        combo = 0;         
    }

    private void Attack()
    {   
        //Hacer animacion de ataque
        //anim.SetTrigger("attack1");

        //Detectar enemigos en rango

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            //Hacer da;o a esos enemigos
            foreach(Collider2D enemy in hitEnemies)
            {  
                if(enemy.GetComponent<enemyBehaviour>() != null)
                {
                    enemy.GetComponent<enemyBehaviour>().TakeDamage(attackDamage);
                    audioManager.instance.PlaySSFX(2);
                }
                
            }
     
        
    }

    private void OnDrawGizmosSelected() {

        if(attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        
    }
}
