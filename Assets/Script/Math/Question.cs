[System.Serializable]
public class Question
{
    public string questionText;
    public string correctAnswer;

    public Question(string text, string answer)
    {
        questionText = text;
        correctAnswer = answer;
    }
}

