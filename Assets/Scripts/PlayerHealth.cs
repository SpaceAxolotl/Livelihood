using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //made using chat gpt and the following tutorial: https://www.youtube.com/watch?v=_1Oou4459Us

    public int maxHealth = 3;
    public int currentHealth;
    

  

    void Start()
    {
        currentHealth = maxHealth;
    }

     public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player took " + amount + " damage. Current Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Game Over");
            // Optionally, add game over logic here (e.g., restart game, show game over screen, etc.)
        }
    }

  
}
