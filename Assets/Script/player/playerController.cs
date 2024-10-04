using System.Collections;
using UnityEngine;
using TMPro;

public class playerScript : MonoBehaviour
{
    // reference variables
    private Vector2 moveInput;
    public float moveSpeed;
    public Rigidbody2D rb2d;
    public Animator playerAnim;
    public SpriteRenderer spriteRenderer;

    public GameObject playerSprite;
    public bool hurting;
    public bool stillInEnemyRange;

    public int direction;
    public float attackingCoolDown;
    public int weaponInUse;
    public GameObject sword1;
    public GameObject axe;
    public GameObject bigSword;

    public int playerHealth;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;

    public TextMeshProUGUI ActiveWeaponText;
    public TextMeshProUGUI PlayerCoinText;
    public int coinCount;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        // target the child SpriteRenderer with the visible sprite
        spriteRenderer = transform.Find("playerSprite").GetComponent<SpriteRenderer>();

        // set active weapon text to hands
        UpdateWeaponDisplay("Hands");

        // Initialize coin count and update the display
        coinCount = 0;
        UpdateCoinDisplay();
    }

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

        // set hearts according to health
        if (playerHealth == 5)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
            heart4.SetActive(true);
            heart5.SetActive(true);
        }
        if (playerHealth == 4)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
            heart4.SetActive(true);
            heart5.SetActive(false);
        }
        if (playerHealth == 3)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
            heart4.SetActive(false);
            heart5.SetActive(false);
        }
        if (playerHealth == 2)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(false);
            heart4.SetActive(false);
            heart5.SetActive(false);
        }
        if (playerHealth == 1)
        {
            heart1.SetActive(true);
            heart2.SetActive(false);
            heart3.SetActive(false);
            heart4.SetActive(false);
            heart5.SetActive(false);
        }
    }
    //enemy contanct / hurting
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hurting == false && playerHealth > 0)
        {
            playerSprite.GetComponent<SpriteRenderer>().color = Color.red;
            playerHealth--;
            StartCoroutine(whitecolor());
            if (playerHealth > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, collision.gameObject.transform.position, -70 * Time.deltaTime);
            }
            hurting = true;
        }
    }

    // handle enemy hurting animations
    IEnumerator whitecolor()
    {

        yield return new WaitForSeconds(1);
        if (playerHealth > 0)
        {
            playerSprite.GetComponent<SpriteRenderer>().color = Color.white;
        }
        hurting = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;

    }

    // Method to update the TextMeshPro display with the current weapon
    void UpdateWeaponDisplay(string weapon)
    {
        ActiveWeaponText.text = "Active: " + weapon; // Update the text to reflect the selected weapon
    }

    // Method to update the coin count display in the UI
    void UpdateCoinDisplay()
    {
        PlayerCoinText.text = "" + coinCount; // Display the current coin count
    }

    // Handle collision with coins
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            coinCount++; // Increment coin count
            collision.gameObject.SetActive(false); // Deactivate the coin GameObject
            UpdateCoinDisplay(); // Update the coin display when a coin is collected
        }

    }

}









