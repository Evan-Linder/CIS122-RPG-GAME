using System.Collections;
using UnityEngine;
using TMPro; 

public class EnemyRise : MonoBehaviour
{
    public static bool moving;
    public GameObject Player;
    public int health = 4;  
    public float knockBackForce;
    public float speed = 2.0f;
    public Rigidbody2D rb;
    public float interactRange;
    public bool seenPlayer = false;
    public GameObject coinPrefab;
    public int coinDropCount;
    public float respawnTime = 1f;
    public Vector2 originalSpawnPosition;
    public bool isAlive = true;
    public GameObject enemyPrefab;
    public SpriteRenderer spriteRenderer;


    public MathDropdown mathScript;

 
    private Animator enemyAnim;
    private Vector2 originalPosition;
    private int originalHealth = 0;


    private int direction = -1; // -1 means idle

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalHealth = health;
        originalPosition = transform.position;
        enemyAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(Player.transform.position, this.transform.position) < interactRange || seenPlayer)
        {
            seenPlayer = true;

            if (health > 0)
            {
                moving = true;
                MoveTowardsPlayer();
                UpdateAnimation();
            }

            if (health <= 0)
            {
                moving = false;
                gameObject.SetActive(false);
                isAlive = false;

                mathScript.ShowQuestionPanel(); 
            }
        }
        else
        {
            moving = false;
            UpdateIdleAnimation();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 moveDirection = (Player.transform.position - transform.position).normalized;
        rb.velocity = moveDirection * speed;

        // Set direction based on movement
        if (Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.y))
        {
            if (moveDirection.x > 0)
            {
                direction = 1; // Right
            }
            else
            {
                direction = 2; // Left
            }
        }
        else
        {
            if (moveDirection.y > 0)
            {
                direction = 3; // Up
            }
            else
            {
                direction = 0; // Down
            }
        }
    }

    private void UpdateAnimation()
    {
        // Walking animations
        if (direction == 0) // down
        {
            enemyAnim.Play("enemyWalkD");
        }
        else if (direction == 1) // right
        {
            enemyAnim.Play("enemyWalkLR");
            spriteRenderer.flipX = false;
        }
        else if (direction == 2) // left
        {
            enemyAnim.Play("enemyWalkLR");
            spriteRenderer.flipX = true;
        }
        else if (direction == 3) // up
        {
            enemyAnim.Play("enemyWalkU");
        }
    }

    private void UpdateIdleAnimation()
    {
        // Idle animations
        if (direction == 0) // down
        {
            enemyAnim.Play("enemyIdleD");
        }
        else if (direction == 1) // right
        {
            enemyAnim.Play("enemyIdleLR");
            spriteRenderer.flipX = false;
        }
        else if (direction == 2) // left
        {
            enemyAnim.Play("enemyIdleLR");
            spriteRenderer.flipX = true;
        }
        else if (direction == 3) // up
        {
            enemyAnim.Play("enemyIdleU");
        }
        else
        {
            
            if (direction == -1)
            {
                enemyAnim.Play("enemyIdleD"); // Default to down idle
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sword1") || collision.gameObject.CompareTag("Axe1"))
        {
            seenPlayer = true;

            if (health > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, -100 * Time.deltaTime);
            }

            DamageSource damageSource = collision.gameObject.GetComponent<DamageSource>();
            if (damageSource != null)
            {
                TakeDamage(damageSource.damageAmount);
            }

            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(WhiteColor());
        }
    }

    void TakeDamage(float damage)
    {
        health -= (int)damage;
    }

    public void DropCoins()
    {
        for (int i = 0; i < coinDropCount; i++)
        {
            Vector2 dropPosition = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f));
            Instantiate(coinPrefab, dropPosition, Quaternion.identity);
        }
    }

    IEnumerator WhiteColor()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    // Handle respawns
    void Respawn()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, originalPosition, Quaternion.identity);
        health = originalHealth;
        spriteRenderer.color = Color.white;
        gameObject.SetActive(true);
    }
}







