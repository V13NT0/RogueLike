using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagePlayer : MonoBehaviour
{
    public int damageDeal;
    private bool isBlocking;


    void Start()
    {
        isBlocking = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Block")
        {
            playerController.instance.KnockBack();
            isBlocking = true;
            audioManager.instance.PlaySSFX(8);
            //Debug.Log("Blocked");
        } else 
        {
            if(other.tag == "Player" && isBlocking == false)
            {
                //Debug.Log("Damaged");
                playerHealthController.instance.DealDamage(damageDeal);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Player") isBlocking = false;
    }
    
}
