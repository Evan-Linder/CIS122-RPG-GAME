using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeographyManager : MonoBehaviour
{
    public GameObject questionPanel;
    public TextMeshProUGUI questionText;
    public TMP_InputField answerInputField;
    public Button submitBtn;
    public int coinDropCount;
    public GameObject coinPrefab;
    public GameObject player;
    public TextMeshProUGUI feedbackText;

    private List<Question> myQuestions = new List<Question>(); 
    private Question currentQuestion;

    

    void Start()
    {
        // Add listener to submit button
        submitBtn.onClick.AddListener(OnQuestionAnswered);
        questionPanel.SetActive(false);

        myQuestions.Add(new Question("What is the capital of the United States?", "washington, d.c."));
        myQuestions.Add(new Question("What is the largest state in the U.S. by area?", "alaska"));
        myQuestions.Add(new Question("Which state is known as the 'Sunshine State'?", "florida"));
        myQuestions.Add(new Question("What is the longest river in the United States?", "missouri river"));
        myQuestions.Add(new Question("Which U.S. state is known as the 'Land of 10,000 Lakes'?", "minnesota"));
        myQuestions.Add(new Question("What is the capital of California?", "sacramento"));
        myQuestions.Add(new Question("Which U.S. state is home to the Grand Canyon?", "arizona"));
        myQuestions.Add(new Question("What is the smallest U.S. state by area?", "rhode island"));
        myQuestions.Add(new Question("What is the tallest mountain in the United States?", "denali"));
        myQuestions.Add(new Question("Which U.S. state is known as the 'Lone Star State'?", "texas"));
        myQuestions.Add(new Question("What is the capital of Texas?", "austin"));
        myQuestions.Add(new Question("Which U.S. state is famous for its potatoes?", "idaho"));
        myQuestions.Add(new Question("What is the capital of New York?", "albany"));
        myQuestions.Add(new Question("Which U.S. state is home to Mount Rushmore?", "south dakota"));
        myQuestions.Add(new Question("Which state is known as the 'Peach State'?", "georgia"));
        myQuestions.Add(new Question("What is the capital of Illinois?", "springfield"));
        myQuestions.Add(new Question("What is the largest city in the U.S. by population?", "new york city"));
        myQuestions.Add(new Question("Which U.S. state is the furthest west?", "hawaii"));
        myQuestions.Add(new Question("What is the capital of Pennsylvania?", "harrisburg"));
        myQuestions.Add(new Question("Which state is known as the 'Beehive State'?", "utah"));
        myQuestions.Add(new Question("What is the capital of Colorado?", "denver"));
        myQuestions.Add(new Question("Which U.S. state shares the longest border with Canada?", "alaska"));
        myQuestions.Add(new Question("Which state is home to the U.S. space agency NASA?", "florida"));
        myQuestions.Add(new Question("What is the capital of Michigan?", "lansing"));
        myQuestions.Add(new Question("Which U.S. state is known for the Great Salt Lake?", "utah"));
        myQuestions.Add(new Question("What is the capital of Ohio?", "columbus"));
        myQuestions.Add(new Question("Which U.S. state is known as the 'Bluegrass State'?", "kentucky"));
        myQuestions.Add(new Question("What is the capital of Oregon?", "salem"));
        myQuestions.Add(new Question("Which U.S. state is known for the Rocky Mountains?", "colorado"));
        myQuestions.Add(new Question("What is the capital of Nevada?", "carson city"));

    }

    // Show the question panel and display a random geography question
    public void ShowQuestionPanel()
    {
        questionPanel.SetActive(true);
        Time.timeScale = 0; 

        // Select a random question
        int randomIndex = Random.Range(0, myQuestions.Count);
        currentQuestion = myQuestions[randomIndex];
        questionText.text = currentQuestion.questionText;
    }

    // Handle when the player answers the question
    public void OnQuestionAnswered()
    {
        string playerAnswer = answerInputField.text.Trim();
        if (playerAnswer.Equals(currentQuestion.answerText, System.StringComparison.OrdinalIgnoreCase))
        {
            feedbackText.text = "Correct!";
            feedbackText.color = Color.green;
            Debug.Log("Correct answer!");
            Time.timeScale = 1;
            questionPanel.SetActive(false);
            DropCoins(); 
        }
        else
        {
            feedbackText.text = "Incorrect! The correct answer is: " + currentQuestion.answerText;
            feedbackText.color = Color.red;
            Debug.Log("Incorrect answer.");
            Time.timeScale = 1;
            questionPanel.SetActive(false);
        }

        StartCoroutine(HandleFeedbackDisplay());
    }

    // handle feedback display
    private IEnumerator HandleFeedbackDisplay()
    {
        feedbackText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        feedbackText.gameObject.SetActive(false);
    }

    // drop coins as a reward near the player
    void DropCoins()
    {
        for (int i = 0; i < coinDropCount; i++)
        {
            float xOffset = Random.Range(-1.0f, 1.0f);
            float yOffset = Random.Range(-1.0f, 1.0f);
            Vector2 dropPosition = new Vector2(player.transform.position.x + xOffset, player.transform.position.y + yOffset);

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
