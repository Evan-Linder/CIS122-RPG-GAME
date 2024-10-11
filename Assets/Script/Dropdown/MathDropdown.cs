using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

        // add more questions here (question, answer) form. It will pick 1 of them at random.

    private QuestionMath currentQuestion;

    void Start()
    {
        submitBtn.onClick.AddListener(OnQuestionAnswered);
        questionPanel.SetActive(false);
        for (int i = 0; i < 100; i++)
        {
            int randomnumberplus = Random.Range(0, 100);
            myquestions.Add(i + randomnumberplus, i + "+" + randomnumberplus);
        }
        for (int j = 0; j < 100; j++)
        {
            int randomnumberminus = Random.Range(0, 50);
            myquestions.Add( j + randomnumberminus, j + "-" + randomnumberminus);
        }
        for (int i = 0; i < 20; i++)
        {
            int randomnumbermul = Random.Range(0, 10);
            myquestions.Add( i + randomnumbermul, i + "*" + randomnumbermul);
        }
        for (int j = 0; j < 20; j++)
        {
            int randomnumbercom = Random.Range(0, 10);
            int randomnumbercom1 = Random.Range(10, 20);
            myquestions.Add(randomnumbercom1 - randomnumbercom + j, randomnumbercom1 + "-" + randomnumbercom + "+" + j);
        }


    }

    // Method to display the question panel and handle the question
    public void ShowQuestionPanel()
    {
        questionPanel.SetActive(true);
        Time.timeScale = 0; // Pause the game
        int[] arrayKeys = new int[myquestions.Count];
        //Take all of keys
        foreach (var key in myquestions.Keys)
        {
            int index = 0;
            arrayKeys[index] = key;
            index++;
        }
        

        int randomIndex = Random.Range(0,arrayKeys.Length );
        int randomQuestion = arrayKeys[randomIndex];
        string question = myquestions[randomQuestion];
        //Through a value ( key )  , take key ( value ) . At this Dictionary , value will play the role as question and key is the answer.
        currentQuestion.Answer = randomQuestion;
    }

    // Method to be called when the player answers the question
    public void OnQuestionAnswered()
    {
        string playerAnswer = answerInputField.text;
        int checkAnswer = int.Parse(playerAnswer);

        if (checkAnswer == currentQuestion.Answer )
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


