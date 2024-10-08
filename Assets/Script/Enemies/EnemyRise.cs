using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRise : MonoBehaviour
{
    public static bool moving;
    public GameObject Player;
    public int health = 4;  // Default health
    public float knockBackForce;
    public float speed = 2.0f;
    public Rigidbody2D rb;
    public float interactRange;
    public bool seenPlayer;
    public GameObject coinPrefab;
    public int coinDropCount;
    public float respawnTime = 1f;
    public Vector2 originalSpawnPosition;
    public bool isAlive = true;
    public GameObject enemyPrefab;
    public SpriteRenderer spriteRenderer;

    private int originalHealth;
    private Vector2 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Save the original properties of the enemy
        originalHealth = health;
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Ensure the enemy is alive before doing anything
        if (isAlive)
        {
            // Check if the enemy is close to the player or if seenPlayer is true
            if (Vector2.Distance(Player.transform.position, transform.position) < interactRange || seenPlayer)
            {
                seenPlayer = true;
                moving = true;
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
            }

            // Check if the enemy's health is 0 or less
            if (health <= 0)
            {
                moving = false;
                DropCoins();
                gameObject.SetActive(false);
                Debug.Log("Enemy defeated!");
                isAlive = false;
                spriteRenderer.enabled = false;
                Invoke("Respawn", respawnTime);  // Respawn after delay
            }
        }
    }

    // Method to detect collision with the player's weapons
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sword1") || collision.gameObject.CompareTag("Axe1") ||
            collision.gameObject.CompareTag("BigSword1") || collision.gameObject.CompareTag("Hands"))
        {
            seenPlayer = true;
            if (health > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, -100 * Time.deltaTime);
            }

            // Check if the object has a DamageSource component
            DamageSource damageSource = collision.gameObject.GetComponent<DamageSource>();
            if (damageSource != null)
            {
                // Apply the damage to the enemy
                TakeDamage(damageSource.damageAmount);
            }

            // Change the enemy to red on hit
            spriteRenderer.color = Color.red;
            StartCoroutine(WhiteColor());
        }
    }

    // Method to reduce the enemy's health
    void TakeDamage(float damage)
    {
        health -= (int)damage; // Reduce health by the damage amount
        Debug.Log("Enemy took " + damage + " damage! Remaining health: " + health);
    }

    // Method to drop coins upon enemy defeat
    void DropCoins()
    {
        for (int i = 0; i < coinDropCount; i++)
        {
            // Instantiate a coin at the enemy's position
            Vector2 dropPosition = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f));
            Instantiate(coinPrefab, dropPosition, Quaternion.identity);
        }
        Debug.Log(coinDropCount + " coins dropped!");
    }

    // Coroutine to briefly turn the enemy white after being hit
    IEnumerator WhiteColor()
    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    // Method to respawn the enemy
    void Respawn()
    {
        // Instantiate a new enemy at the original position with the original properties
        GameObject newEnemy = Instantiate(enemyPrefab, originalPosition, Quaternion.identity);

        // Reset the new enemy's health and position to the original values
        health = originalHealth;
        isAlive = true;
        spriteRenderer.color = Color.white;
        spriteRenderer.enabled = true;
        gameObject.SetActive(true);


        Debug.Log("Enemy respawned!");
    }
}



