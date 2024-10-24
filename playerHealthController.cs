using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealthController : MonoBehaviour
{

    public static playerHealthController instance;
    public int currentHealth, maxHealth;

    public float invincibleLegth;
    public bool playerDeath;
    private float invincibleCount;

    public SpriteRenderer sr;


    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        //sr = GetComponentInChildren<SpriteRenderer>();

        playerDeath = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (invincibleCount > 0)
        {
            invincibleCount -= Time.deltaTime;

            if(invincibleCount <= 0)
            {
                sr.color = new Color(sr.color.r, 1f, 1f, 1f);
            }
        }


    }

    public void DealDamage(int damage)
    {

        if(invincibleCount <= 0)
        {
            
            currentHealth -= damage;
            playerController.instance.anim.SetTrigger("hurt");
            audioManager.instance.PlaySSFX(6);

            if(currentHealth <= 0)
            {
                currentHealth = 0;

                playerController.instance.anim.SetTrigger("death");

                playerDeath = true;

                //while(deathTime != 0) deathTime -= Time.deltaTime;                         
                //if (deathTime == 0) gameObject.SetActive(false);

                //Poner anim de muerte antes de destruir el objeto
                audioManager.instance.PlaySSFX(7); 


            }
            else{

                invincibleCount = invincibleLegth;
                sr.color = new Color(sr.color.r, 0f, 0f, .5f);

                playerController.instance.KnockBack();

            }

            playerCombat.instance.FinishCombo();

            uiController.instance.UpdateHealthDisplay();


        }


    }


    public void HealPlayer(){

        currentHealth++;
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }

        uiController.instance.UpdateHealthDisplay();
    }

    public void PlayerDeath()
    {
        gameObject.SetActive(false);
    }

}
