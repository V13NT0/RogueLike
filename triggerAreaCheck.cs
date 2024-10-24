using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerAreaCheck : MonoBehaviour
{
    private enemyAttack enemy;

    private void Start() 
    {
        enemy = GetComponentInParent<enemyAttack>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {           
            enemy.target = other.gameObject;
            enemy.inRange = true;
            gameObject.SetActive(false);
            enemy.hotZone.SetActive(true);
        }
    }
}
