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

    private Dictionary<int, string> myquestions = new Dictionary<int, string>();
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
            if (!myquestions.ContainsKey(key))
            {
                myquestions.Add(key, i + " + " + randomnumberplus);
            }
        }

        for (int j = 0; j < 100; j++)
        {
            int randomnumberminus = Random.Range(0, 50);
            int key = j - randomnumberminus;
            if (!myquestions.ContainsKey(key))
            {
                myquestions.Add(key, j + " - " + randomnumberminus);
            }
        }

        for (int i = 0; i < 20; i++)
        {
            int randomnumbermul = Random.Range(0, 10);
            int key = i * randomnumbermul;
            if (!myquestions.ContainsKey(key))
            {
                myquestions.Add(key, i + " * " + randomnumbermul);
            }
        }
    }

    // Show the question panel and display a random math question
    public void ShowQuestionPanel()
    {
        questionPanel.SetActive(true);
        Time.timeScale = 0; // Pause the game

        // Select a random question
        int[] arrayKeys = new int[myquestions.Count];
        myquestions.Keys.CopyTo(arrayKeys, 0); // Copy keys to an array
        int randomIndex = Random.Range(0, arrayKeys.Length);
        int randomAnswer = arrayKeys[randomIndex];
        string randomQuestion = myquestions[randomAnswer];

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
            Debug.Log("Correct answer!");
            Time.timeScale = 1;
            questionPanel.SetActive(false);
            DropCoins(); // Drop coins as a reward
        }
        else
        {
            Debug.Log("Incorrect answer.");
            Time.timeScale = 1;
            questionPanel.SetActive(false);
        }
    }

    // Drop coins as a reward near the player
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



