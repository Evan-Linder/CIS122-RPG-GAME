// written by Hieu Pham

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MathDropdown : MonoBehaviour
{
    public GameObject questionPanel;
    public TextMeshProUGUI questionText;
    public TMP_InputField answerInputField;
    public Button submitBtn;
    public int coinDropCount;
    public GameObject coinPrefab;
    public GameObject player;
    public TextMeshProUGUI feedbackText;

    private Dictionary<int, string> myQuestions = new Dictionary<int, string>();
    private int currentAnswer;

    void Start()
    {
        submitBtn.onClick.AddListener(OnQuestionAnswered);
        questionPanel.SetActive(false);

        // Add random questions (in form of question-answer pairs)
        for (int i = 0; i < 100; i++)
        {
            int randomnumberplus = Random.Range(0, 100);
            int key = i + randomnumberplus;
            if (!myQuestions.ContainsKey(key))
            {
                myQuestions.Add(key, i + " + " + randomnumberplus);
            }
        }

        for (int j = 0; j < 100; j++)
        {
            int randomnumberminus = Random.Range(0, 50);
            int key = j - randomnumberminus;
            if (!myQuestions.ContainsKey(key))
            {
                myQuestions.Add(key, j + " - " + randomnumberminus);
            }
        }

        for (int i = 0; i < 20; i++)
        {
            int randomnumbermul = Random.Range(0, 10);
            int key = i * randomnumbermul;
            if (!myQuestions.ContainsKey(key))
            {
                myQuestions.Add(key, i + " * " + randomnumbermul);
            }
        }
        for (int i = 10; i < 20; i++)
        {
            int randomnumber1 = Random.Range(0, 10);
            int randomnumber2 = Random.Range(10, 20);
            int key = i + randomnumber2 - randomnumber1;
            if (!myQuestions.ContainsKey(key))
            {
                myQuestions.Add(key, i + " + " + randomnumber2 + " - " + randomnumber1);
            }
        }
        for (int i = 1; i < 10; i++)
        {
            int randomnumber1 = Random.Range(1, 10);
            int randomnumber2 = Random.Range(10, 20);
            int key = i * randomnumber1 + randomnumber2;
            if (!myQuestions.ContainsKey(key))
            {
                myQuestions.Add(key, i + " * " + randomnumber1 + " + " + randomnumber2);
            }
        }
    }

    // Show the question panel and display a random math question
    public void ShowQuestionPanel()
    {
        questionPanel.SetActive(true);
        Time.timeScale = 0; // Pause the game

        // Select a random question
        int[] arrayKeys = new int[myQuestions.Count];
        myQuestions.Keys.CopyTo(arrayKeys, 0); // Copy keys to an array
        int randomIndex = Random.Range(0, arrayKeys.Length);
        int randomAnswer = arrayKeys[randomIndex];
        string randomQuestion = myQuestions[randomAnswer];

        // Display the question and store the answer
        questionText.text = randomQuestion;
        currentAnswer = randomAnswer;
    }

    // Handle when the player answers the question
    public void OnQuestionAnswered()
    {
        string playerAnswer = answerInputField.text;

        if (int.TryParse(playerAnswer, out int parsedAnswer) && parsedAnswer == currentAnswer)
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
            feedbackText.text = "Incorrect!";
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




