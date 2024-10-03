using UnityEngine;
using TMPro;

public class playerScript : MonoBehaviour
{
    private Vector2 moveInput;
    public float moveSpeed;
    public Rigidbody2D rb2d;
    public Animator playerAnim;
    public SpriteRenderer spriteRenderer;

    public int direction;
    public float attackingCoolDown;
    public int weaponInUse;
    public GameObject sword1;
    public GameObject axe;
    public GameObject bigSword;

    public TextMeshProUGUI ActiveWeaponText; // Reference to TextMeshPro component

    // Initialize components
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        // target the child SpriteRenderer with the visible sprite
        spriteRenderer = transform.Find("playerSprite").GetComponent<SpriteRenderer>();

        // set active weapon text to hands.
        UpdateWeaponDisplay("Hands");
    }

    // Update is called once per frame
    void Update()
    {
        // if cooldown is over, allow movement and attacking
        if (attackingCoolDown <= 0)
        {
            rb2d.constraints = RigidbodyConstraints2D.None;
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;

            // movement
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
                if (direction == 0) // down
                {
                    playerAnim.Play("playerIdleD");
                }
                if (direction == 1) // right
                {
                    playerAnim.Play("playerIdleLR");
                    spriteRenderer.flipX = false;
                }
                if (direction == 2) // left
                {
                    playerAnim.Play("playerIdleLR");
                    spriteRenderer.flipX = true;
                }
                if (direction == 3) // up
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
            // reduce the cooldown over time
            attackingCoolDown -= Time.deltaTime;
            rb2d.velocity = Vector2.zero; // stop movement
        }

        // Changing weapons
        if (Input.GetKey(KeyCode.Alpha1))
        {
            axe.SetActive(false);
            sword1.SetActive(true);
            bigSword.SetActive(false);
            weaponInUse = 0;
            UpdateWeaponDisplay("Slot1"); 
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            axe.SetActive(true);
            sword1.SetActive(false);
            bigSword.SetActive(false);
            weaponInUse = 1; 
            UpdateWeaponDisplay("Slot2"); 
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            axe.SetActive(false);
            sword1.SetActive(false);
            bigSword.SetActive(true);
            weaponInUse = 2; 
            UpdateWeaponDisplay("Slot3"); 
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            sword1.SetActive(false);
            axe.SetActive(false);
            bigSword.SetActive(false);
            weaponInUse = -1;
            UpdateWeaponDisplay("Hands"); 
        }
    }

    // Method to update the TextMeshPro display with the current weapon
    void UpdateWeaponDisplay(string weapon)
    {
        ActiveWeaponText.text = "Active: " + weapon; // Update the text to reflect the selected weapon
    }
}










