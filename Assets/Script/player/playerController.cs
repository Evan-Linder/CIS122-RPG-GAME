using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class playerScript : MonoBehaviour
{
    // reference variables
    private SoundEffectManager sound;
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
    public float miningCoolDown;
    public int weaponInUse;
    public GameObject sword1;
    public GameObject axe;
    public GameObject bigSword;
    public GameObject hands;
    public GameObject katana;
    public GameObject lance;

    public int playerHealth;
    public Animator gameOver;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;

    private WeaponShop weaponShop;

    public TextMeshProUGUI ActiveWeaponText;
    public TextMeshProUGUI playerCoinText;
    public TextMeshProUGUI playerDiamondText;
    public TextMeshProUGUI playerIronOreText;
    public TextMeshProUGUI playerCrystalText;
    public int coinCount;
    public int ironCount = 0;
    public int diamondCount = 0;
    public int crystalCount = 0;

    public Button playAgainButton;
    PlayerActions playerActions;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        sound = GameObject.FindObjectOfType<SoundEffectManager>();
        playerActions = GetComponent<PlayerActions>();

        // target the child SpriteRenderer with the visible sprite
        spriteRenderer = transform.Find("playerSprite").GetComponent<SpriteRenderer>();

        // set active weapon text to hands
        UpdateWeaponDisplay("Hands");
        hands.SetActive(true);

        UpdateCoinDisplay();
        UpdateDiamondDisplay();
        UpdateIronOreDisplay();
        UpdateCrystalDisplay();

        playAgainButton.onClick.AddListener(PlayAgain);
    }

    void Update()
    {
        weaponShop = FindObjectOfType<WeaponShop>();

        if (playerActions.isFishing)
        {
            return;
        }

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
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (direction == 0)
                {
                    playerAnim.Play("playerMineD");
                    attackingCoolDown = 0.6f;
                }
                if (direction == 1)
                {
                    playerAnim.Play("playerMineR");
                    attackingCoolDown = 0.6f;
                    spriteRenderer.flipX = false;
                }
                if (direction == 2)
                {
                    playerAnim.Play("playerMineL");
                    attackingCoolDown = 0.6f;
                    spriteRenderer.flipX = true;
                }
                if (direction == 3)
                {
                    playerAnim.Play("playerMineU");
                    attackingCoolDown = 0.6f;
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
            sword1.SetActive(false);
            bigSword.SetActive(false);
            hands.SetActive(true);
            lance.SetActive(false);
            katana.SetActive(false);
            weaponInUse = 0;
            UpdateWeaponDisplay("Hands");
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            if (weaponShop != null && weaponShop.hasWeapon)
            {
                if (weaponShop.currentWeapon == "Sword")
                {
                    axe.SetActive(false);
                    sword1.SetActive(true);
                    bigSword.SetActive(false);
                    hands.SetActive(false);
                    lance.SetActive(false);
                    katana.SetActive(false);
                    weaponInUse = 1;
                    UpdateWeaponDisplay("Sword");
                }
                else if (weaponShop.currentWeapon == "Axe")
                {
                    axe.SetActive(true);
                    sword1.SetActive(false);
                    bigSword.SetActive(false);
                    hands.SetActive(false);
                    weaponInUse = 2;
                    UpdateWeaponDisplay("Axe");
                }
                else if (weaponShop.currentWeapon == "BigSword")
                {
                    axe.SetActive(false);
                    sword1.SetActive(false);
                    bigSword.SetActive(true);
                    hands.SetActive(false);
                    lance.SetActive(false);
                    katana.SetActive(false);
                    weaponInUse = 3;
                    UpdateWeaponDisplay("Big Sword");
                }
                else if (weaponShop.currentWeapon == "Katana")
                {
                    axe.SetActive(false);
                    sword1.SetActive(false);
                    bigSword.SetActive(false);
                    hands.SetActive(false);
                    lance.SetActive(false);
                    katana.SetActive(true);
                    weaponInUse = 4;
                    UpdateWeaponDisplay("Katana");
                }
                else if (weaponShop.currentWeapon == "Lance")
                {
                    axe.SetActive(false);
                    sword1.SetActive(false);
                    bigSword.SetActive(false);
                    hands.SetActive(false);
                    lance.SetActive(true);
                    katana.SetActive(false);
                    weaponInUse = 4;
                    UpdateWeaponDisplay("Lance");
                }
            }
            else { }
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
        if (playerHealth <= 0)
        {
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(false);
            heart4.SetActive(false);
            heart5.SetActive(false);

            gameOver.Play("gameOver");
            gameObject.GetComponent<Animator>().speed = 0;
        }
    }
    //enemy contanct / hurting
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hurting == false && playerHealth > 0)
        {
            sound.PlayPlayerHitSound();
            playerSprite.GetComponent<SpriteRenderer>().color = Color.red;
            playerHealth--;
            StartCoroutine(WhiteColor());
            if (playerHealth > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, collision.gameObject.transform.position, -70 * Time.deltaTime);
            }
            hurting = true;
        }
    }

    // handle hurting animations
    IEnumerator WhiteColor()
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
    public void UpdateWeaponDisplay(string weapon)
    {
        ActiveWeaponText.text = "Active: " + weapon; 
    }

    // Method to update the coin count display in the UI
    public void UpdateCoinDisplay()
    {
        playerCoinText.text = "" + coinCount; 
    }
    public void UpdateDiamondDisplay()
    {
        playerDiamondText.text = "" + diamondCount;
    }
    public void UpdateIronOreDisplay()
    {
        playerIronOreText.text = "" + ironCount;
    }
    public void UpdateCrystalDisplay()
    {
        playerCrystalText.text = "" + crystalCount;
    }

    // Handle collision with coins
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            coinCount++; 
            collision.gameObject.SetActive(false); 
            UpdateCoinDisplay();
            sound.PlayGoldPickUpSound();

        }
        if (collision.gameObject.CompareTag("DiamondMaterial"))
        {
            diamondCount++;
            collision.gameObject.SetActive(false);
            UpdateDiamondDisplay();
        }
        if (collision.gameObject.CompareTag("IronMaterial"))
        {
            ironCount++;
            collision.gameObject.SetActive(false);
            UpdateIronOreDisplay();
        }
        if (collision.gameObject.CompareTag("CrystalMaterial"))
        {
            crystalCount++;
            collision.gameObject.SetActive(false);
            UpdateCrystalDisplay();
        }

    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

}
