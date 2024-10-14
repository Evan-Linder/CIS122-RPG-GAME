using System.Collections;
using TMPro;
using UnityEngine;

public class Mineral : MonoBehaviour
{
    public int health = 3;
    public string mineralTypeMessage = "You destroyed a mineral";
    public TextMeshProUGUI messageTextBox;
    public GameObject itemPrefab;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // when something collides with this object
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PickAxe"))
        {
            Debug.Log("Collision with PickAxe detected.");
            TakeDamage(1);
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
            DropItem();
        }
    }

    IEnumerator ShowMessageAndDestroy()
    {
        // Disable the sprite renderer and box collider to make the mineral invisible and non-interactive
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;

        messageTextBox.text = mineralTypeMessage;
        messageTextBox.color = Color.white;
        messageTextBox.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        messageTextBox.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    void DropItem()
    {
        Instantiate(itemPrefab, transform.position, Quaternion.identity);
    }
}

