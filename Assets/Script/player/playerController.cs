using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    private Vector2 moveInput;
    public float moveSpeed = 5f;
    public Rigidbody2D rb2d;
    public Animator playerAnim;
    public SpriteRenderer spriteRenderer;

    public int direction;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); 
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        // Movement input
        moveInput.x = Input.GetAxisRaw("Horizontal"); // get horizontal input (A/D or Left/Right arrow keys)
        moveInput.y = Input.GetAxisRaw("Vertical"); // Get vertical input (W/S or Up/Down arrow keys)
        moveInput.Normalize(); //normalize to prevent faster diagonal movement

        // Determine whether to move vertically or horizontally
        if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
        {
            moveInput.y = 0; //zero out vertical movement
            rb2d.velocity = moveInput * moveSpeed; // set horizontal velocity
        }
        else
        {
            moveInput.x = 0; // zero out horizontal movement
            rb2d.velocity = moveInput * moveSpeed; // Set vertical velocity
        }

        // walking animations
        if (moveInput.y < 0) // Down
        {
            playerAnim.Play("playerWalkD");
            direction = 0;

        }
        else if (moveInput.x > 0) // Right
        {
            playerAnim.Play("playerWalkLR");
            direction = 1;
            spriteRenderer.flipX = false; // No flip for right
        }
        else if (moveInput.x < 0) // Left
        {
            playerAnim.Play("playerWalkLR");
            direction = 2;
            spriteRenderer.flipX = true; // Flip for left
        }
        else if (moveInput.y > 0) // Up
        {
            playerAnim.Play("playerWalkU");
            direction = 3;
        }

        // idle animations
        if (moveInput.y == 0 && moveInput.x == 0) // check if not moving
        {
            if (direction == 0) // down
            {
                playerAnim.Play("playerIdleD");
            }
            else if (direction == 1) // right
            {
                playerAnim.Play("playerIdleLR");
            }
            else if (direction == 2) // left
            {
                playerAnim.Play("playerIdleLR");
            }
            else if (direction == 3) // up
            {
                playerAnim.Play("playerIdleU");
            }
        }
    }
}




