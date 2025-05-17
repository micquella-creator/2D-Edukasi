using UnityEngine;

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz/Question")]
public class QuestionData : ScriptableObject
{
    [TextArea] public string questionText;
    public string[] options;
    public int correctAnswerIndex;
}
