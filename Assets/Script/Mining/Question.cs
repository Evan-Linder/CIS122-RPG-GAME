using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionText;
    public string answerText;

    // Constructor for easy question creation
    public Question(string question, string answer)
    {
        questionText = question;
        answerText = answer;
    }
}

