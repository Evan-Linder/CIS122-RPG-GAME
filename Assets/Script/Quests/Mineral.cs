using System.Collections;
using TMPro;
using UnityEngine;

public class Mineral : MonoBehaviour
{
    public int health = 3;
    public string mineralTypeMessage = "You destroyed a mineral!";
    public TextMeshProUGUI messageTextBox;

    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log("Mineral initialized with health: " + health);
    }

    // when something collides with this object
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision detected with: " + other.gameObject.name);

        if (other.CompareTag("PickAxe"))
        {
            Debug.Log("Collision with PickAxe detected.");
            TakeDamage(1);
        }
        else
        {
            Debug.Log("Collision with non-PickAxe object detected.");
        }
    }

    // method to apply damage to the mineral
    void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Damage applied: " + damage + ". Remaining health: " + health);

        if (health <= 0)
        {
            StartCoroutine(ShowMessageAndDestroy());
        }
    }

    IEnumerator ShowMessageAndDestroy()
    {
        // Disable the sprite renderer and box collider to make the mineral invisible and non-interactive
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;

        // Display the message
        messageTextBox.text = mineralTypeMessage;
        messageTextBox.color = Color.white;
        messageTextBox.gameObject.SetActive(true);

        // Wait for 2 seconds
        yield return new WaitForSeconds(2);

        // Hide the message
        messageTextBox.gameObject.SetActive(false);

        // Destroy the mineral object after the message has been hidden
        gameObject.SetActive(false);
    }
}
