using System.Collections.Generic;
using UnityEngine;

public class Kedenemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float interactRange = 5f;
    public float speed = 2f;
    public int health = 100;
    public int coinDropCount = 3;
    public float respawnTime = 5f;

    [Header("References")]
    public GameObject Player;
    public GameObject coinPrefab;
    public QuestionManager questionManager; // Reference to the QuestionManager

    private int originalHealth;
    private bool moving = false;
    private bool seenPlayer = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        originalHealth = health;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if ((Vector2.Distance(Player.transform.position, transform.position) < interactRange || seenPlayer) && health > 0)
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        moving = true;
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            seenPlayer = true;
            moving = false; // Stop the enemy from moving
            questionManager.ShowQuestion(this); // Trigger question popup and pass reference to this enemy
        }
    }

    // Method to reduce health if answer is incorrect
    public void TakeDamage(float damage)
    {
        health -= (int)damage;
        Debug.Log("Enemy took " + damage + " damage! Remaining health: " + health);

        if (health <= 0)
        {
            OnDeath();
        }
    }

    void OnDeath()
    {
        DropCoins();
        gameObject.SetActive(false);
        Debug.Log("Enemy defeated!");
        Invoke(nameof(Respawn), respawnTime);
    }

    void DropCoins()
    {
        for (int i = 0; i < coinDropCount; i++)
        {
            Vector2 dropPosition = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f));
            Instantiate(coinPrefab, dropPosition, Quaternion.identity);
        }
    }

    void Respawn()
    {
        health = originalHealth;
        spriteRenderer.color = Color.white;
        seenPlayer = false;
        gameObject.SetActive(true);
        Debug.Log("New enemy is created.");
    }
}
