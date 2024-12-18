// written by Evan Linder

using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public static bool moving;
    public GameObject Player;
    public int health;
    public float knockBackForce;
    public float speed = 2.0f;
    public Rigidbody2D rb;

    public float interactRange;
    public bool seenPlayer;
    public GameObject coinPrefab;
    public int coinDropCount;

    public float respawnTime = 10f;
    public Vector2 originalSpawnPosition;
    public bool isAlive = true;
    public SpriteRenderer spriteRenderer;
    private Vector2 originalPosition;
    private int originalHealth = 4;
    private SoundEffectManager sound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalHealth = health;
        originalPosition = transform.position;
        sound = GameObject.FindObjectOfType<SoundEffectManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the enemy is within bounds of the player or if the bool = true so the slime will chase.
        if (Vector2.Distance(Player.transform.position, this.transform.position) < interactRange || seenPlayer)
        {
            seenPlayer = true;

            // Ensure the enemy is alive before moving.
            if (health > 0)
            {
                moving = true;
                // Move towards the player.
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
            }
            // Check if enemy's health is 0
            if (health <= 0)
            {
                moving = false;
                DropCoins();
                gameObject.SetActive(false);
                Debug.Log("Enemy defeated!");
                Invoke("Respawn", respawnTime);
                sound.PlayZone1EnemyDie();
            }
        }
    }

    // Method to detect collision with the weapons
    private void OnTriggerEnter2D(Collider2D collision)
    {
        sound.PlayZone1EnemySound();
        if (collision.gameObject.CompareTag("Sword1") ||
            collision.gameObject.CompareTag("Axe1") ||
            collision.gameObject.CompareTag("BigSword1") ||
            collision.gameObject.CompareTag("Hands")     ||
            collision.gameObject.CompareTag("FireBall"))
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

            // Change the enemy red on hit
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(WhiteColor());
        }
    }

    // Method to reduce enemy's health
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

    // Enemy hurting animations
    IEnumerator WhiteColor()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;
    }
    void Respawn()
    {
        health = originalHealth;
        spriteRenderer.color = Color.white;
        seenPlayer = false;
        Debug.Log("New enemy is created.");
        gameObject.SetActive(true);
    }
}
