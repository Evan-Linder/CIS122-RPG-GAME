using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Kedemeny : MonoBehaviour
{
    // Enemy-specific properties
    public GameObject Player;
    public int health = 4;
    public GameObject coinPrefab;
    public int coinDropCount;
    public bool isAlive = true;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb; // Reference to Rigidbody2D
    public float attackSpeed = 2.0f; // Enemy movement speed
    public BoxCollider2D boxCollider; // Enemy's BoxCollider2D

    // Animation-related properties
    private Animator enemyAnim;
    private int currentDirection = -1; // Direction: 0=Down, 1=Right, 2=Left, 3=Up

    // Question-related properties
    public GameObject questionPanel;
    public TextMeshProUGUI questionText;
    public Button buttonA;
    public Button buttonB;
    public TextMeshProUGUI feedbackText;

    private string[] questions = new string[]
    {
        "Is the sky blue? A: Yes, B: No",
        "Is 2+2=5? A: Yes, B: No",
        "Is water wet? A: Yes, B: No"
    };

    private string[] correctAnswers = new string[] { "A", "B", "A" }; // Correct answers for each question
    private int currentQuestionIndex = -1; // Set when a random question is chosen
    private bool awaitingAnswer = false;
    private bool attackingPlayer = false; // Flag for attacking mode

    void Start()
    {
        // Enemy setup
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        enemyAnim = GetComponent<Animator>();

        // Button setup
        buttonA.onClick.AddListener(() => OnAnswerSelected("A"));
        buttonB.onClick.AddListener(() => OnAnswerSelected("B"));

        questionPanel.SetActive(false);
    }

    void Update()
    {
        // Move toward the player if attacking mode is enabled
        if (attackingPlayer && isAlive)
        {
            MoveTowardsPlayer();
            UpdateRunningAnimation();
        }

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Prompt the question when the player collides with the enemy
        if (collision.gameObject == Player && isAlive && !awaitingAnswer)
        {
            PromptQuestion();
        }
        if (collision.gameObject.CompareTag("Sword1") || collision.gameObject.CompareTag("Axe1") || collision.gameObject.CompareTag("BigSword1") || collision.gameObject.CompareTag("Hands") || collision.gameObject.CompareTag("FireBall"))
        {
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

    private void TakeDamage(float damage)
    {
        health -= (int)damage;
    }

    private IEnumerator WhiteColor()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void PromptQuestion()
    {
        awaitingAnswer = true;
        SelectRandomQuestion();
        ShowQuestionPanel();

        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Stop enemy movement
        }
    }

    private void SelectRandomQuestion()
    {
        currentQuestionIndex = Random.Range(0, questions.Length); // Choose a random question
    }

    private void ShowQuestionPanel()
    {
        questionPanel.SetActive(true);
        Time.timeScale = 0; // Pause the game

        // Display the current question
        questionText.text = questions[currentQuestionIndex];
    }

    private void OnAnswerSelected(string answer)
    {
        if (awaitingAnswer)
        {
            awaitingAnswer = false;
            Time.timeScale = 1; // Resume the game
            questionPanel.SetActive(false);

            // Check if the answer is correct
            if (answer == correctAnswers[currentQuestionIndex])
            {
                feedbackText.text = "Correct!";
                feedbackText.color = Color.green;
                HandleCorrectAnswer();
            }
            else
            {
                feedbackText.text = "Incorrect!";
                feedbackText.color = Color.red;
                HandleIncorrectAnswer();
            }
        }
    }

    private void HandleCorrectAnswer()
    {
        // Correct answer: Make the enemy disappear and drop coins
        DisappearAndDropCoins();
    }

    private void HandleIncorrectAnswer()
    {
        // Incorrect answer: Start moving toward the player
        attackingPlayer = true;

        // Disable the trigger on the enemy's collider
        if (boxCollider != null)
        {
            boxCollider.isTrigger = false;
        }
    }

    private void MoveTowardsPlayer()
    {
        if (rb != null)
        {
            Vector2 direction = (Player.transform.position - transform.position).normalized;
            rb.velocity = direction * attackSpeed;

            // Determine movement direction for animation
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                currentDirection = (direction.x > 0) ? 1 : 2; // Right or Left
            }
            else
            {
                currentDirection = (direction.y > 0) ? 3 : 0; // Up or Down
            }
        }
    }

    private void UpdateRunningAnimation()
    {
        if (enemyAnim != null)
        {
            switch (currentDirection)
            {
                case 0: // Down
                    enemyAnim.Play("enemyWalkD");
                    break;
                case 1: // Right
                    enemyAnim.Play("enemyWalkLR");
                    spriteRenderer.flipX = false;
                    break;
                case 2: // Left
                    enemyAnim.Play("enemyWalkLR");
                    spriteRenderer.flipX = true;
                    break;
                case 3: // Up
                    enemyAnim.Play("enemyWalkU");
                    break;
            }
        }
    }

    private void DisappearAndDropCoins()
    {
        isAlive = false;
        gameObject.SetActive(false);

        // Drop coins as a reward
        for (int i = 0; i < coinDropCount; i++)
        {
            float xOffset = Random.Range(-1.0f, 1.0f);
            float yOffset = Random.Range(-1.0f, 1.0f);
            Vector2 dropPosition = new Vector2(transform.position.x + xOffset, transform.position.y + yOffset);

            Instantiate(coinPrefab, dropPosition, Quaternion.identity);
        }
    }
}




