// written by Evan Linder

using System.Collections;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public static bool moving;
    public GameObject Player;
    public int health = 4;
    public float knockBackForce;
    public float speed = 2.0f;
    public Rigidbody2D rb;
    public float interactRange;
    public float attackRange = 3f; 
    public bool seenPlayer = false;
    public GameObject coinPrefab;
    public GameObject silverCupPrefab;
    public int coinDropCount;
    public float respawnTime = 1f;
    public Vector2 originalSpawnPosition;
    public bool isAlive = true;
    public SpriteRenderer spriteRenderer;

    private SoundEffectManager sound;
    private Animator enemyAnim;
    private Vector2 originalPosition;
    private int originalHealth = 4;
    private int direction = -1; 
    private float lastAttackTime = -Mathf.Infinity; // track the last attack time
    public float attackCooldown = 1.5f;
    public bool isAttacking = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalHealth = health;
        originalPosition = transform.position;
        enemyAnim = GetComponent<Animator>();
        sound = GameObject.FindObjectOfType<SoundEffectManager>();
    }

    void Update()

    {
        if (isAttacking) { return; }
        if (Vector2.Distance(Player.transform.position, this.transform.position) < interactRange || seenPlayer)
        {
            seenPlayer = true;

            if (health > 0)
            {
                moving = true;

                // Check if player is in attack range
                if (Vector2.Distance(Player.transform.position, transform.position) <= attackRange)
                {
                    CheckAttackRange();
                    StartCoroutine(WaitToFinish());
                }
                else
                {
                    MoveTowardsPlayer();
                    UpdateAnimation();
                }
            }

            if (health <= 0)
            {
                sound.PlayEnemyDieSound();
                moving = false;
                gameObject.SetActive(false);
                isAlive = false;
                Invoke("Respawn", respawnTime);
                DropCoins();
                DropSilverCup();
                sound.PlayBossDieSound();
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

        if (Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.y))
        {
            direction = moveDirection.x > 0 ? 1 : 2;
        }
        else
        {
            direction = moveDirection.y > 0 ? 3 : 0;
        }
    }

    private void UpdateAnimation()
    {
        enemyAnim.Play("enemyWalk");
        spriteRenderer.flipX = direction == 3;
    }

    private void UpdateIdleAnimation()
    {
        enemyAnim.Play("enemyIdle");
    }

    private void CheckAttackRange()
    {
        isAttacking = true;
        // Ensure enough time has passed since the last attack
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time; // Reset last attack time

            Vector2 playerPosition = Player.transform.position;
            Vector2 bossPosition = transform.position;

            if (playerPosition.x > bossPosition.x)
            {
                enemyAnim.Play("enemyAttackR");
                direction = 1;
            }
            else
            {
                enemyAnim.Play("enemyAttackL");
                direction = 2;
            }

            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sword1") || collision.gameObject.CompareTag("Axe1") || collision.gameObject.CompareTag("BigSword1") || collision.gameObject.CompareTag("Hands") || collision.gameObject.CompareTag("FireBall"))
        {
            sound.PlayBossHitSound();
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

    public void DropSilverCup()
    {

            Vector2 dropPosition = new Vector2(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f));
            Instantiate(silverCupPrefab, dropPosition, Quaternion.identity);

    }

    IEnumerator WhiteColor()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;
    }
    IEnumerator WaitToFinish()
    {
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
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

