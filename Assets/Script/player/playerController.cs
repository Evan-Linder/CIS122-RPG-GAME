using UnityEngine;

public class playerScript : MonoBehaviour
{
    private Vector2 moveInput;
    public float moveSpeed;
    public Rigidbody2D rb2d;
    public Animator playerAnim;
    public SpriteRenderer spriteRenderer;

    public int direction;
    public float attackingCoolDown; // Cooldown period for attacking
    public GameObject sword1;

    // Initialize components
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        // Find the child SpriteRenderer with the visible sprite
        spriteRenderer = transform.Find("playerSprite").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // If cooldown is over, allow movement and attacking
        if (attackingCoolDown <= 0)
        {
            rb2d.constraints = RigidbodyConstraints2D.None;
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;

            // Movement
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
                playerAnim.Play("playerWalkD");
                direction = 0;
            }
            else if (moveInput.x > 0)
            {
                playerAnim.Play("playerWalkLR");
                direction = 1;
                spriteRenderer.flipX = false;
            }
            else if (moveInput.x < 0)
            {
                playerAnim.Play("playerWalkLR");
                direction = 2;
                spriteRenderer.flipX = true;
            }
            else if (moveInput.y > 0)
            {
                playerAnim.Play("playerWalkU");
                direction = 3;
            }

            // Idle animations
            if (moveInput.y == 0 && moveInput.x == 0)
            {
                if (direction == 0)
                {
                    playerAnim.Play("playerIdleD");
                }
                if (direction == 1)
                {
                    playerAnim.Play("playerIdleLR");
                    spriteRenderer.flipX = false;
                }
                if (direction == 2)
                {
                    playerAnim.Play("playerIdleLR");
                    spriteRenderer.flipX = true;
                }
                if (direction == 3)
                {
                    playerAnim.Play("playerIdleU");
                }
            }

            // Attacking
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (direction == 0)
                {
                    playerAnim.Play("playerAttackD");
                    attackingCoolDown = 0.4f;
                }
                if (direction == 1)
                {
                    playerAnim.Play("playerAttackR");
                    attackingCoolDown = 0.4f;
                    spriteRenderer.flipX = false;
                }
                if (direction == 2)
                {
                    playerAnim.Play("playerAttackL");
                    attackingCoolDown = 0.4f;
                    spriteRenderer.flipX = true;
                }
                if (direction == 3)
                {
                    playerAnim.Play("playerAttackU");
                    attackingCoolDown = 0.4f;
                }
            }
        }
        else
        {
            // Reduce the cooldown over time
            attackingCoolDown -= Time.deltaTime;

            // Stop movement during cooldown (optional)
            rb2d.velocity = Vector2.zero;
        }
    }
}








