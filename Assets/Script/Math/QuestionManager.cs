using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class QuestionManager : MonoBehaviour
{
    // math question UI
    public GameObject questionPanel;
    public TextMeshProUGUI questionText;
    public TMP_InputField answerInputField;
    public Button submitBtn;
    public int coinDropCount;
    public GameObject coinPrefab;
    public GameObject player;

    private Question[] questions = {
        new Question("What is 2 + 2?", "4"),
        new Question("What is 4 + 4?", "8")
        // add more questions here (question, answer) form. It will pick 1 of them at random.
    };

    private Question currentQuestion; 

    void Start()
    {
        submitBtn.onClick.AddListener(OnQuestionAnswered);
        questionPanel.SetActive(false); 
    }

    // Method to display the question panel and handle the question
    public void ShowQuestionPanel()
    {
        questionPanel.SetActive(true);
        Time.timeScale = 0; // Pause the game

        int randomIndex = Random.Range(0, questions.Length);
        currentQuestion = questions[randomIndex];
        questionText.text = currentQuestion.questionText;
    }

    // Method to be called when the player answers the question
    public void OnQuestionAnswered()
    {
        string playerAnswer = answerInputField.text;

        if (playerAnswer == currentQuestion.correctAnswer)
        {
            Debug.Log("Correct answer!");
            Time.timeScale = 1;
            questionPanel.SetActive(false);
            DropCoins();
            // Add logic to drop coins or give rewards if needed
        }
        else
        {
            Debug.Log("Incorrect answer.");
            Time.timeScale = 1; 
            questionPanel.SetActive(false); 
        }
    }
    void DropCoins()
    {
        for (int i = 0; i < coinDropCount; i++)
        {
            // define a range for the drop position around the player
            float xOffset = Random.Range(-1.0f, 1.0f); 
            float yOffset = Random.Range(-1.0f, 1.0f); 

            // calculate the drop position near the player
            Vector2 dropPosition = new Vector2(player.transform.position.x + xOffset, player.transform.position.y + yOffset);

            // ensure the coin doesn't drop directly on the player
            while (Vector2.Distance(dropPosition, player.transform.position) < 0.5f) 
            {
                xOffset = Random.Range(-1.0f, 1.0f);
                yOffset = Random.Range(-1.0f, 1.0f);
                dropPosition = new Vector2(player.transform.position.x + xOffset, player.transform.position.y + yOffset);
            }

            Instantiate(coinPrefab, dropPosition, Quaternion.identity);
        }
    }

}
