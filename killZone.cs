using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "Player")
        {
            //Death
            playerHealthController.instance.DealDamage(1);

            if(!playerHealthController.instance.playerDeath) levelManager.instance.RespawnPlayer();

        }
    }
}
