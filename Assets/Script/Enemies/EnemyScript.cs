using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health;
    public float knockBackForce;
    public Rigidbody2D rb;

    public GameObject coinPrefab; // Coin prefab to be dropped upon death
    public int coinDropCount;     // Number of coins to drop

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // check if enemy's health is 0
        if (health <= 0)
        {
            DropCoins(); // Drop coins upon defeat
            gameObject.SetActive(false); // Disable the enemy
            Debug.Log("Enemy defeated!");
        }
    }

    // Method to detect collision with the weapons
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sword1") || (collision.gameObject.CompareTag("Axe1") || (collision.gameObject.CompareTag("BigSword1"))))
        {
            // Check if the object has a DamageSource component
            DamageSource damageSource = collision.gameObject.GetComponent<DamageSource>();
            if (damageSource != null)
            {
                // Apply the damage to the enemy
                TakeDamage(damageSource.damageAmount);

                // Apply knockback
                ApplyKnockback(collision.transform.position);
            }
        }
    }

    // Method to reduce enemy's health
    void TakeDamage(float damage)
    {
        health -= (int)damage; // Reduce health by the damage amount
        Debug.Log("Enemy took " + damage + " damage! Remaining health: " + health);
    }

    // Method to apply knockback when hit
    void ApplyKnockback(Vector2 attackPosition)
    {
        Vector2 knockBackDirection = (rb.position - (Vector2)attackPosition).normalized;

        // Apply knockback force to the enemy's Rigidbody
        rb.AddForce(knockBackDirection * knockBackForce, ForceMode2D.Impulse);
        Debug.Log("Knockback applied!");
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
}


