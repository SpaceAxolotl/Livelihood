using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach to a damage source

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
            if (playerHealth.currentHealth>0)
            {
              playerHealth.TakeDamage(damage);
              StartCoroutine (BecomeInvincible());
            }
               
            
        }
       IEnumerator BecomeInvincible()
        {
            Physics2D.IgnoreLayerCollision(7, 8, true);
            print("you are now invincible");
            yield return new WaitForSeconds(1f);
            print("You are no longer invincible");
            Physics2D.IgnoreLayerCollision(7, 8, false);
                       
        }
    }
}
