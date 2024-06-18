using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResponsibilityAI : MonoBehaviour
{
    //inspiratie: https://www.youtube.com/watch?v=RuvfOl8HhhM
    //https://www.youtube.com/watch?v=l7VyxIzAIAc
    //https://learn.unity.com/tutorial/switch-statements#5c8a6f91edbc2a067d4753d4 voor gebruik switch statements
    //voor het vinden van een locatie van een gameobject https://gamedev.stackexchange.com/questions/118441/how-do-i-find-the-location-of-a-gameobject-in-c

    //wat voor data heb je nodig?

    //Player:
    Vector2 playerPosition;
    Vector2 spawnpoint;
    enemystate state = enemystate.wander;
    [SerializeField] float wanderSpeed;
    [SerializeField] Rigidbody2D enemyRB;
   
    //State 1: stationary
    int damage;

    //State 2: Wander
    [SerializeField] int wanderrange=3;
    Vector2 currentposition;
    Vector2 targetPositionLeft;
    Vector2 targetPositionRight;
   
    //State 3: Chase
    bool ischasing;
    [SerializeField] int chaserange=5;
    [SerializeField] float chaseSpeed=5;
    [SerializeField] GameObject Player;

    private void Awake()
    {
        spawnpoint = transform.position;
        WanderOrChaseRight();
        
    }

    //Wandering
    enum enemystate //dit is een switch state. Je enemy heeft 3 statussen: stilstaan, wanderen, chase. Stationary doet momenteel niets, maar dit was het idee voor een stilstaande enemy (Responsibilities 1)
    {
        stationary,
        wander,
        chase
    }



    void Update()
    {
        //als de speler in range is en je staat op case wander, verander de state naar enemystate.chase
        if(state==enemystate.wander && PlayerIsInChaseRange() == true) { 
            state = enemystate.chase;
            print("CHASING THE ENEMY");
        }

        if(state==enemystate.chase && PlayerIsInChaseRange()==false)
        {
            state = enemystate.wander;
            print("I am now wandering");
        }

        switch(state)
        {
            case enemystate.stationary:  
                //je hebt een player collision check nodig waar je schade doet als de speler hurtbox collide met de hitbox van de enemy
                break;
                case enemystate.wander:
                //we willen: enemy beweegt richting een punt (range) gekeken vanuit je spawnpoint.

                IsInWanderRange();
                
                if(IsInWanderRange() == true) //eerst: zijn wij in onze wanderrange?
                {                    
                    if(currentposition.x >= targetPositionRight.x)
                    {
                        WanderOrChaseLeft();                        
                    }
                    if(currentposition.x <= targetPositionLeft.x)
                    {
                        WanderOrChaseRight();                        
                    }
                }

                if(!IsInWanderRange())
                {
                    if (currentposition.x >= targetPositionRight.x)
                    {
                        WanderOrChaseLeft();                        
                    }
                    if (currentposition.x <= targetPositionLeft.x)
                    {
                        WanderOrChaseRight();                        
                    }
                }
                               
                break;
                case enemystate.chase:
                //first we get the playerposition. Then we compare the playerposition to the current enemy position. 
                playerPosition = GetCurrentPlayerPosition();
                currentposition = gameObject.transform.position;
                PlayerIsInChaseRange();

                if(PlayerIsInChaseRange() == true)
                {
                    if (playerPosition.x <= (currentposition.x -1))
                    {
                        WanderOrChaseLeft();
                    }
                    if (playerPosition.x >= (currentposition.x +1))
                    {
                        WanderOrChaseRight();
                    }
                }
                
                //If the player position.x is higher than the current enemy position, move right. If not, move left with the new chase speed for as long as the player is in range.

                break;
        }
        //wat te doen bij wandering
        //wat te doen bij chasing
    }

    
   private bool PlayerIsInChaseRange()
    {
        playerPosition = GetCurrentPlayerPosition();
        if ( playerPosition.x <= spawnpoint.x + chaserange &&  playerPosition.x >= spawnpoint.x-chaserange)
            //if playerposition is within the chaserange on either side, player is in chase range.
        {
            return true;
        }
        else
        {
            return false; 
        }
    }
    private bool IsInWanderRange()
    {
        currentposition = gameObject.transform.position; //kijk naar de momentele positie van het gameobject waar dit script aan gelinkt is
        targetPositionLeft = new Vector2(spawnpoint.x - wanderrange, 0); //dit is de uiterste plek waar je enemy naartoe beweegt.
        targetPositionRight = new Vector2(spawnpoint.x + wanderrange, 0);

        if (currentposition.x <= targetPositionRight.x && currentposition.x >= targetPositionLeft.x)
        {
            return true;
        }
        else return false;
    }

    public Vector2 GetCurrentPlayerPosition()
    {
        playerPosition = Player.transform.position;
        return playerPosition;
    }

    private void WanderOrChaseRight()
    {
        if(state==enemystate.wander)
        {
            enemyRB.velocity = new Vector2(wanderSpeed, 0); //wander naar rechts
        }
        if (state == enemystate.chase)
        {
            enemyRB.velocity = new Vector2(chaseSpeed, 0); //wander naar rechts
        }
        
    }
    private void WanderOrChaseLeft()
    {
        if (state == enemystate.wander) //als de enemystate wander is,
        {
            enemyRB.velocity = new Vector2(-wanderSpeed, 0); //zet de nieuwe snelheid van je gameobject: ga naar links
        }
        if (state == enemystate.chase)
        {
            enemyRB.velocity = new Vector2(-chaseSpeed, 0); //chase naar links
        }
    }

    
    
}
