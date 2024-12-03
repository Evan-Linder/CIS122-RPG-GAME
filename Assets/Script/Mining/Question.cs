// written by Evan Linder

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

