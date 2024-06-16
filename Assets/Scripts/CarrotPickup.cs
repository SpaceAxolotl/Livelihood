using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarrotPickup : MonoBehaviour
{

    //made using chatgpt and the amazing help of THIS video by BMo https://www.youtube.com/watch?v=HmXU4dZbaMw
    // modified by me

    public float detectionRadius = 5f; // Radius within which the player can interact with the carrot
    public Transform player; // Reference to the player's transform
    [SerializeField] private int requiredPresses = 10; // Number of presses required to pull out the carrot

    private int currentPressCount = 0; // Tracks the current number of button presses
    private bool isPlayerNearby = false; // Whether the player is within the detection radius

    private InputAction interact;

    public _2DBart playerControls;


    private static int totalCarrotsCollected = 0; // Keeps track of the total carrots collected by the player

    private void Awake()
    {
        playerControls = new _2DBart(); //2Dbart is my script for input actions, I'm too lazy to change it
    }
    private void OnEnable()
    {
        interact = playerControls.Player.Interact;
        interact.Enable();
        interact.performed += Interact;
    }

    private void OnDisable()
    {
        interact.Disable();
    }



    private void FixedUpdate()
    {
        // Check the distance between the player and the carrot
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRadius)
        {
            if (!isPlayerNearby)
            {
                isPlayerNearby = true;
                // Prompt the player to press the button
                Debug.Log("Press the button to pull out the carrot!");
            }
        }
        else
        {
            if (isPlayerNearby)
            {
                isPlayerNearby = false;
                // Reset the current press count if the player leaves the detection radius
                currentPressCount = 0;
            }
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed && isPlayerNearby)
        {
            // Increment the current press count
            currentPressCount++;
            Debug.Log($"Button pressed {currentPressCount} times");

            // Check if the required number of presses is reached
            if (currentPressCount >= requiredPresses)
            {
                PullOutCarrot();
            }
        }
    }

    private void PullOutCarrot()
    {
        // Add the carrot to the player's collection
        totalCarrotsCollected++;
        Debug.Log("Carrot pulled out! Total carrots collected: " + totalCarrotsCollected);

        // Optionally, you can destroy the carrot game object or disable it
        Destroy(gameObject);
    }
   

    private void OnDrawGizmosSelected()
    {
        // Draw a sphere in the editor to show the detection radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}




