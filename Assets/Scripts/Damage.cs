using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach to a damage source
//this script made use of the following tutorials: https://www.youtube.com/watch?v=S61J3kDQ5Mk, https://www.youtube.com/watch?v=_1Oou4459Us

public class Damage : MonoBehaviour
{
    public PlayerHealth playerHealth;
    [SerializeField] int damage;
    public bool isInvincible = false;
    public bool playerTookDmg=false;
    float invincibleCounter=0f;
    public float invincibilityDuration = 1.0f; // Duration of invincibility frames after being hit

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            print("player entered");
            if (playerHealth.currentHealth > 0 && isInvincible == false)
            {
              playerHealth.TakeDamage(damage);
                playerTookDmg = true;
            }
            if (playerTookDmg == true)
            {
                StartCoroutine(BecomeInvincible());
                
            }
               
            
        }
       IEnumerator BecomeInvincible()
        {
            playerTookDmg = false;
            isInvincible = true;
            print("you are now invincible");
            yield return new WaitForSeconds(2f);
            isInvincible = false;
            print("You are no longer invincible");
          
                       
        }
    }
}
