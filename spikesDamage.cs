using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikesDamage : MonoBehaviour
{
    public int damageDeal;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            playerHealthController.instance.DealDamage(damageDeal);
        }
    }
}
