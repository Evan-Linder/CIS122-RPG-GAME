using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro

public class EnemyRise : MonoBehaviour
{
    public static bool moving;
    public GameObject Player;
    public int health = 4;  // Default health
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
    private Vector2 originalPosition;
    private int originalHealth = 0;

    // Reference to the QuestionManager
    public QuestionManager questionManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalHealth = health;
        originalPosition = transform.position;
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
                transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
            }

            if (health <= 0)
            {
                moving = false;
                gameObject.SetActive(false);
                isAlive = false;

                questionManager.ShowQuestionPanel(); // Call the QuestionManager to show the panel
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







