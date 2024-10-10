using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MathDropdown : MonoBehaviour
{
    public Dropdown dropbox;
    public Text inputAnswer;
    public Text questiont;
    public TextMeshProUGUI showAsnwer;
    public string checkanswer;
    

     void Questions()
    {
        int num1 = Random.Range(0, 100);
        int num2 = Random.Range(0, 100);
        // commented out to avoid warning.
        string mathOperator = "*+-%";
        int randomOperator = Random.Range(0, 3);
        StringBuilder question = new StringBuilder();
        question.Append(num1 + mathOperator[randomOperator] + num2);
        string questionoff = question.ToString();
        questiont.text = question.ToString();
        int correctAnswer = 0;
        if (questionoff.Contains("+"))
            {
            correctAnswer = num1 + num2;
        }
        if (questionoff.Contains("-"))
        {
            correctAnswer = num1 - num2;
        }
        if (questionoff.Contains("/"))
        {
            correctAnswer = num1 + num2;
        }
        if (questionoff.Contains("%"))
        {
            correctAnswer = num1 + num2;
        }
        dropbox.options.Clear();






    }
    
}
