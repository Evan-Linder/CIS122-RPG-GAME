using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    public GameObject questionPanel;
    public Text questionText;
    public Button option1Button;
    public Button option2Button;

    private Kedenemy currentEnemy; // Reference to the enemy that triggered the question

    void Start()
    {
        questionPanel.SetActive(false); // Hide the question panel initially
    }

    public void ShowQuestion(Kedenemy enemy)
    {
        currentEnemy = enemy;
        questionPanel.SetActive(true); // Show the question panel

        // Example question and options
        questionText.text = "What is 5 + 3?";
        option1Button.GetComponentInChildren<Text>().text = "8";
        option2Button.GetComponentInChildren<Text>().text = "7";

        // Set button responses
        option1Button.onClick.AddListener(() => AnswerQuestion(true));
        option2Button.onClick.AddListener(() => AnswerQuestion(false));
    }

    private void AnswerQuestion(bool correct)
    {
        questionPanel.SetActive(false); // Hide the question panel

        // Clear previous button listeners
        option1Button.onClick.RemoveAllListeners();
        option2Button.onClick.RemoveAllListeners();

        if (correct)
        {
            Debug.Log("Correct answer!");
            currentEnemy.TakeDamage(currentEnemy.health); // Defeat enemy with correct answer
        }
        else
        {
            Debug.Log("Incorrect answer!");
            currentEnemy.TakeDamage(10); // Apply small damage for incorrect answer
        }

        currentEnemy = null;
    }
}

