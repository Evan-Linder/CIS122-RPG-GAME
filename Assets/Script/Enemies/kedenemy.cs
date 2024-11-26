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
        "Is water wet? A: Yes, B: No",
        "Is the sun a star? A: Yes, B: No",
        "Is Earth the third planet from the Sun? A: Yes, B: No",
        "Do humans have gills? A: Yes, B: No",
        "Is fire cold? A: Yes, B: No",
       "Can fish breathe underwater? A: Yes, B: No",
       "Do birds have feathers? A: Yes, B: No",
       "Is 10 greater than 5? A: Yes, B: No",
       "Is the square root of 4 equal to 2? A: Yes, B: No",
       "Is ice a liquid at room temperature? A: Yes, B: No",
       "Do plants need sunlight to grow? A: Yes, B: No",
       "Is the moon made of cheese? A: Yes, B: No",
       "Can penguins fly? A: Yes, B: No",
       "Is the Great Wall of China visible from space? A: Yes, B: No",
       "Do elephants have trunks? A: Yes, B: No",
       "Is water composed of hydrogen and oxygen? A: Yes, B: No",
       "Can humans live without water for weeks? A: Yes, B: No",
       "Does the color green result from mixing blue and yellow? A: Yes, B: No",
       "Is the Amazon River the longest river in the world? A: Yes, B: No",
       "Is Mount Everest the highest mountain on Earth? A: Yes, B: No",
       "Is the capital of France Madrid? A: Yes, B: No"
    };

    private string[] correctAnswers = new string[] {
        
     "A", //The sky is blue
     "B", // 2+2 = 4
     "A",// water is wet 
     "A", // The sun is a star.
     "A", // Earth is the third planet from the Sun.
     "B", // Humans do not have gills.
     "B", // Fire is not cold.
     "A", // Fish breathe underwater.
     "A", // Birds have feathers.
     "A", // 10 is greater than 5.
     "A", // The square root of 4 is 2.
     "B", // Ice is not a liquid at room temperature.
     "A", // Plants need sunlight to grow.
     "B", // The moon is not made of cheese.
     "B", // Penguins cannot fly.
     "B", // The Great Wall of China is not visible from space.
     "A", // Elephants have trunks.
     "A", // Water is composed of hydrogen and oxygen.
     "B", // Humans cannot live without water for weeks.
     "A", // Green is a result of mixing blue and yellow.
     "B", // The Amazon River is not the longest; the Nile is longer.
     "A", // Mount Everest is the highest mountain on Earth
     "B"  // The capital of France is Paris, not Madrid.
   
     }; // Correct answers for each question
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
        // Pop out a question when the player collides with the enemy
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

            if (gameObject.activeInHierarchy)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                StartCoroutine(FlashDamageEffect());
            }
        }

            
    }

    private void TakeDamage(float damage)
    {
        health -= (int)damage;

        if (health <= 0)
        {
            //Drop coins and remmove enemy
            DisappearAndDropCoins();


        }
        
      
    }

   
    private IEnumerator FlashDamageEffect()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red; // Change color to indicate damage
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = Color.white; // Reset to original color
        }

        // Temporarily disable collider to prevent stacking hits
        if (boxCollider != null)
        {
            boxCollider.enabled = false;
            yield return new WaitForSeconds(0.2f);
            boxCollider.enabled = true;
        }
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
        isAlive = false; // mark the enemy as no longer alive 

        //Disable the enemy game object 
        gameObject.SetActive(false);

        // Drop coins at the enemy's current location 
        for (int i = 0; i < coinDropCount; i++)
        {  
            Vector2 dropPosition = new Vector2(transform.position.x , transform.position.y );

            Instantiate(coinPrefab, dropPosition, Quaternion.identity);
        }
       

    }

    
}




