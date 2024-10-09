using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dropdown : MonoBehaviour
{
    public Dropdown dropbox;
    public Text inputAnswer;
    public string checkanswer;
    
    // Update is called once per frame
    void Update()
    {
        
    }
    void AddQuestion()
    {
        List<string> listquestion = new List<string>();
        int num1 = Random.Range(0, 100);
        int num2 = Random.Range(0, 100);
        // commented out to avoid warning.
        //string operatorMath = "*+-";
    }
}
