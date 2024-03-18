using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    Vector2 moveInput;
    public bool IsMoving { get; private set; }
    [SerializeField] float walkSpeed;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();      
    }
    
    

    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(moveInput.x * walkSpeed, rigidbody.velocity.y);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
    }
   
}
