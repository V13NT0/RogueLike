using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hotZone : MonoBehaviour
{
    private enemyAttack enemy;
    private bool inRange;
    private Animator anim;
    void Start()
    {
        enemy = GetComponentInParent<enemyAttack>();
        anim = GetComponentInParent<Animator>();
    }

    private void Update() 
    {
        if(inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("attacking"))
        {
            enemy.Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            inRange = true;
            enemy.target = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            enemy.triggerArea.SetActive(true);
            enemy.inRange = false;
            anim.SetBool("isRunning", false);
        }
    }
}
