using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]

//written thanks to the following tutorials: https://www.youtube.com/watch?v=XhwRYNie-aI

public class PlayerController : MonoBehaviour
{
    //used by onmove
    Rigidbody2D playerRbody; //rigidbody helps you with control of an object's position through physics simulation
    Vector2 moveInput; //vector2 is een positie (x,y) of vector. Basically een punt ergens in je gamescene.
 public bool IsMoving { get; private set; }
    [SerializeField] float walkSpeed;

    //used by jump
    [SerializeField] float jumpTime;
    [SerializeField] int jumpPower;
    [SerializeField] float fallMultiplier;
    [SerializeField] float jumpMultiplier;
    bool isJumping;
    float jumpCounter;

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
        
        if (Input.GetButtonDown("Jump") && isGrounded()) //check if we're pressing jump btn and if we are grounded
        {
            playerRbody.velocity = new Vector2(playerRbody.velocity.x, jumpPower); //changes player velocity (up) to how high we can jump with jump power
            isJumping = true;
            jumpCounter = 0; //resets jump time            
        }

        if(playerRbody.velocity.y >0 && isJumping) //if the player is going up and jumping
        {
            jumpCounter += Time.deltaTime; //keeps track of how long we're jumping
            
            if(jumpCounter>jumpTime) isJumping = false; //stops jumping (increasing velocity) after jumptime is smaller than the counter.
            
            float t = jumpCounter / jumpTime;
            float currentjumpM = jumpMultiplier;

            if (t>0.5f) 
            {
                currentjumpM = jumpMultiplier * (1 - t);
            }

            playerRbody.velocity += vecGravity * currentjumpM * Time.deltaTime; //increases velocity (up) based on the jumpmultiplier and time.
            
        }

        if(playerRbody.velocity.y < 0) //checks if we're falling
        {
            playerRbody.velocity -= vecGravity * fallMultiplier * Time.deltaTime; //increases velocity (down) based on how long you're falling
            
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            jumpCounter = 0;
            if (playerRbody.velocity.y > 0)
            {
                playerRbody.velocity = new Vector2(playerRbody.velocity.x, playerRbody.velocity.y * 0.6f);
            }
        }
    }
    private void FixedUpdate()
    {
        playerRbody.velocity = new Vector2(moveInput.x * walkSpeed, playerRbody.velocity.y);
               
    }

    bool isGrounded() //does the player capsule collider overlap with the ground?
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1f, 0.085f), CapsuleDirection2D.Horizontal, 0, groundLayer);

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
    }
   
}
