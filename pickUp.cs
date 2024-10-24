using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUp : MonoBehaviour
{

    public bool isCoin, isHeal;

    private bool isCollected;

    public GameObject pickUpEffect;

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.CompareTag("Player") && !isCollected)
        {
            if(isCoin) 
            {
                levelManager.instance.coinCollected++;

                uiController.instance.UpdateCoinCount();

                Instantiate(pickUpEffect ,transform.position, transform.rotation);

                isCollected = true;
                audioManager.instance.PlaySSFX(4);
                Destroy(gameObject);
            }

            if(isHeal)
            {
                if(playerHealthController.instance.currentHealth != playerHealthController.instance.maxHealth)
                {
                    playerHealthController.instance.HealPlayer();

                    isCollected = true;
                    audioManager.instance.PlaySSFX(3);
                    Destroy(gameObject);
                }
            }
        }
    }
}
