using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRise : MonoBehaviour
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
    public float respawnTime = 1f;
    public Vector2 lastDeathPosition; // Track the death position of the enemy

    public bool isAlive = true;
    public GameObject enemyPrefab; // Add the enemy prefab reference for respawn

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastDeathPosition = transform.position; // Initialize to the current position
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(Player.transform.position, this.transform.position) < interactRange || seenPlayer == true)
        {
            seenPlayer = true;

            if (health > 0)
            {
                moving = true;
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
            }

            if (health <= 0)
            {
                moving = false;
                DropCoins();
                lastDeathPosition = transform.position; // Record the death position
                Debug.Log("Enemy defeated at position: " + lastDeathPosition);
                isAlive = false;
                Destroy(gameObject); // Destroy the current enemy
                Respawn(); // Start the respawn coroutine
            }
        }
    }

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
        health -= (int)damage; // Reduce health by the damage amount
        Debug.Log("Enemy took " + damage + " damage! Remaining health: " + health);
    }

    void DropCoins()
    {
        for (int i = 0; i < coinDropCount; i++)
        {
            Vector2 dropPosition = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f));
            Instantiate(coinPrefab, dropPosition, Quaternion.identity);
        }
        Debug.Log(coinDropCount + " coins dropped!");
    }

    IEnumerator WhiteColor()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    void Respawn()
    {
        

        // Spawn a new enemy prefab at the last recorded death position
        Vector2 dropPosition = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f));
        Instantiate(enemyPrefab, dropPosition, Quaternion.identity);
        Debug.Log("Enemy respawned at position: " + lastDeathPosition);
    }
}

