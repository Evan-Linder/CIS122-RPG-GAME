using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health;
    public float knockBackForce; 
    public Rigidbody2D rb; 

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
            // disable the enemy if health is depleted
            gameObject.SetActive(false);
            Debug.Log("Enemy defeated!");
        }
    }

    // method to detect collision with the weapons
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sword1") || (collision.gameObject.CompareTag("Axe1") || (collision.gameObject.CompareTag("BigSword1"))))
        {
            // check if the object has a DamageSource component
            DamageSource damageSource = collision.gameObject.GetComponent<DamageSource>();
            if (damageSource != null)
            {
                // apply the damage to the enemy
                TakeDamage(damageSource.damageAmount);

                //  apply knockback
                ApplyKnockback(collision.transform.position);
            }
        }
    }

    // method to reduce enemy's health
    void TakeDamage(float damage)
    {
        health -= (int)damage; // reduce health by the damage amount
        Debug.Log("Enemy took " + damage + " damage! Remaining health: " + health);
    }

    // method to apply knockback when hit
    void ApplyKnockback(Vector2 attackPosition)
    {

        Vector2 knockBackDirection = (rb.position - (Vector2)attackPosition).normalized;

       // apply knockback force to the enemy's Rigidbody
        rb.AddForce(knockBackDirection * knockBackForce, ForceMode2D.Impulse);
        Debug.Log("Knockback applied!");
    }
}


