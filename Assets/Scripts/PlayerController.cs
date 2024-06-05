using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]

//written with help from the following tutorials: 
//https://www.youtube.com/watch?v=XhwRYNie-aI base player movement
//https://www.youtube.com/watch?v=24-BkpFSZuI flip() and a better version of Jump
//Player stick to walls fix: https://www.youtube.com/watch?v=rcob41f6WVQ
//better version of ground check: https://www.youtube.com/watch?v=P_6W-36QfLA

//Please note that I figured out the player running and gradual increase of player speed on my own.

public class PlayerController : MonoBehaviour
{
    //used by onmove
    Rigidbody2D playerRbody; //rigidbody helps you with control of an object's position through physics simulation
    Vector2 moveInput; //vector2 is een positie (x,y) of vector. Basically een punt ergens in je gamescene.
 public bool IsMoving { get; private set; }
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;

    //used by flip
    bool isFacingRight = true;
    private float horizontal;


    //used by jump
    [SerializeField] float jumpTime;
    [SerializeField] int jumpPower;
    [SerializeField] float fallMultiplier;
    [SerializeField] float jumpMultiplier;
    bool isJumping;
    float jumpCounter;

    //used by run
    bool isRunning=false;
    float buildupSpeed;
    


    public Transform groundCheck;
    public LayerMask groundLayer; //calls a specific layer to be called in Raycast
    Vector2 vecGravity;

        
    
    private void Awake()
    {
        playerRbody = GetComponent<Rigidbody2D>();      
    }

    private void Update()
    {
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
               
        if(!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if(isFacingRight&&horizontal < 0f)
        {
            Flip();
        }
             
        if(playerRbody.velocity.y >0 && isJumping) //if the player is going up and jumping
        {
            jumpCounter += Time.deltaTime; //keeps track of how long we're jumping
            
            if(jumpCounter>jumpTime) isJumping = false; //stops jumping (increasing velocity y) after jumptime is smaller than the counter.
            
            float t = jumpCounter / jumpTime;
            float currentjumpM = jumpMultiplier;

            if (t>0.5f)  //if your jumpcounter is half of your jumptime:
            {
                currentjumpM = jumpMultiplier * (1 - t); //decrease your jump multiplier slowly until it stops adding anything, resulting in a nice curve.
            }

            playerRbody.velocity += vecGravity * currentjumpM * Time.deltaTime; //increases velocity (up) based on the jump multiplier and time.
            
        }

        if(playerRbody.velocity.y <= 0) //checks if we're falling downwards
        {
            playerRbody.velocity -= vecGravity * fallMultiplier * Time.deltaTime; //increases velocity (down) based on how long you're falling
        }

      
    }


    private void FixedUpdate()
    {
        if (isRunning == false)
        {
            if(buildupSpeed > walkSpeed)
            {
                buildupSpeed = buildupSpeed - 0.2f; //gradually decrease player speed from running to walking
                playerRbody.velocity = new Vector2(moveInput.x * buildupSpeed, playerRbody.velocity.y); //sets walking speed as velocity while maintaining horizontal velocity
            }
            else
            { 
                playerRbody.velocity = new Vector2(moveInput.x * walkSpeed, playerRbody.velocity.y); //sets walking speed as velocity while maintaining horizontal velocity
            }

        }
        if (isRunning == true)
        {
            
            if (buildupSpeed < runSpeed)
            {
                buildupSpeed = buildupSpeed + 0.1f; //gradually increase player speed from walking to running                
            }
            playerRbody.velocity = new Vector2(moveInput.x * buildupSpeed, playerRbody.velocity.y); //move at an increasingly speedier speed.
            
            if (playerRbody.velocity.x < walkSpeed &&isFacingRight) //if you move slower than walking speed and are facing right, reset the speed buildup when moving right
            {
                buildupSpeed = walkSpeed;
            }          
            
        }
                    
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded()) {
            playerRbody.velocity = new Vector2(playerRbody.velocity.x, jumpPower); //changes player velocity (up) to how high we can jump with jump power
            isJumping = true;
            jumpCounter = 0; //resets jump time            
        }
        if(context.canceled && playerRbody.velocity.y > 0f)
        {
            isJumping = false; //spring je niet meer (maar ga je nog wel door omhoog)
            jumpCounter = 0; //reset je de jump counter omdat je niet meer springt
            if (playerRbody.velocity.y > 0) //zolang je nog omhoog gaat:
            {
                playerRbody.velocity = new Vector2(playerRbody.velocity.x, playerRbody.velocity.y * 0.6f); //zorg ervoor dat je steeds minder snel omhoog gaat
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    bool isGrounded() //does the player capsule collider overlap with the ground?
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1f, 0.085f), CapsuleDirection2D.Horizontal, 0, groundLayer);

    }
    public void OnMove(InputAction.CallbackContext context) //dit refereert naar je input op je gamepad of je keyboard. in dit geval, onMove
    {
        horizontal = context.ReadValue<Vector2>().x; //get an x value based off of your horizontal input. You need this for Flip().
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;

    }

    public void IsRunning(InputAction.CallbackContext context) //dit refereert naar je input op je gamepad of je keyboard. in dit geval, check of je rent.
    {
        if (context.performed)
        {
            isRunning = true;            
        }
        if (context.canceled)
        {
            isRunning = false;//we're currently walking
        }
    }
   
}
