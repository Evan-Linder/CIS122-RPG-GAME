using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMath
{
    private int answwer;
    private string question;
    public QuestionMath(int answer, string question)
    {
        this.answwer = answer;
        this.question = question;
    }
    public int Answer
    {

        get { return answwer; }
        set { answwer = value; }
    }
   
}
