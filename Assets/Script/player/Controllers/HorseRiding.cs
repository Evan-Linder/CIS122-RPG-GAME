using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class HorseRiding : MonoBehaviour
{
    private Vector2 moveInput;
    public float moveSpeed;
    public SpriteRenderer spriteRenderer;
    public Animator playerAnim;
    public Rigidbody2D rb2d;
    public int direction;
    public bool isRiding = false;
    ZoneLabelDisplay newAreaSpawn;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = transform.Find("playerSprite").GetComponent<SpriteRenderer>();
        newAreaSpawn = FindObjectOfType<ZoneLabelDisplay>();
    }

    // Update is called once per frame
    void Update()
    {
        newAreaSpawn = FindObjectOfType<ZoneLabelDisplay>();
        if  (newAreaSpawn.isRideAbleZone != true)
        {
            DismountHorse();
        }
        // Toggle riding state with R key
        if (Input.GetKeyDown(KeyCode.R) && newAreaSpawn.isRideAbleZone)
        {
            isRiding = !isRiding;
            if (isRiding)
            {
                rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            else
            {
                DismountHorse();
            }
        }

        // Handle movement and animations if riding
        if (isRiding)
        {
            RideHorse();
        }
    }

    public void RideHorse()
    {
        // Movement input
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        if (Mathf.Abs(moveInput.x) > Mathf.Abs(moveInput.y))
        {
            moveInput.y = 0;
            rb2d.velocity = moveInput * moveSpeed;
        }
        else
        {
            moveInput.x = 0;
            rb2d.velocity = moveInput * moveSpeed;
        }

        // Walking animations
        if (moveInput.y < 0)
        {
            playerAnim.Play("horseWalkD");
            direction = 0;
        }
        else if (moveInput.x > 0)
        {
            playerAnim.Play("horseWalkLR");
            direction = 1;
            spriteRenderer.flipX = false;
        }
        else if (moveInput.x < 0)
        {
            playerAnim.Play("horseWalkLR");
            direction = 2;
            spriteRenderer.flipX = true;
        }
        else if (moveInput.y > 0)
        {
            playerAnim.Play("horseWalkU");
            direction = 3;
        }

        // Idle animations
        if (moveInput.y == 0 && moveInput.x == 0)
        {
            if (direction == 0) // down
            {
                playerAnim.Play("horseIdleD");
            }
            if (direction == 1) // right
            {
                playerAnim.Play("horseIdleLR");
                spriteRenderer.flipX = false;
            }
            if (direction == 2) // left
            {
                playerAnim.Play("horseIdleLR");
                spriteRenderer.flipX = true;
            }
            if (direction == 3) // up
            {
                playerAnim.Play("horseIdleU");
            }
        }
    }

    public void DismountHorse()
    {
        isRiding = false;
    }
}
