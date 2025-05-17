using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    public GameObject quizPanel;
    public TextMeshProUGUI questionText;
    public Button[] optionButtons;

    public QuestionData[] questions;
    private int currentQuestionIndex = 0;

    private int wrongAnswerCount = 0;
    public int maxWrongAnswers = 3; // Ubah jadi 3

    [Header("Heart System")]
    public Image[] heartImages; // Isi 3 Image saja
    public Sprite heartFull;
    public Sprite heartEmpty;

    private void Start()
    {
        quizPanel.SetActive(false);
        ResetHearts();
    }

    public void StartQuiz()
    {
        quizPanel.SetActive(true);
        wrongAnswerCount = 0;
        ResetHearts();
        LoadQuestion();
    }

    void LoadQuestion()
    {
        if (currentQuestionIndex >= questions.Length)
        {
            quizPanel.SetActive(false);
            return;
        }

        QuestionData q = questions[currentQuestionIndex];
        questionText.text = q.questionText;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = q.options[i];
            int index = i;
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => Answer(index));
        }
    }

    void Answer(int selectedIndex)
    {
        if (selectedIndex == questions[currentQuestionIndex].correctAnswerIndex)
        {
            quizPanel.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            wrongAnswerCount++;
            UpdateHearts();

            if (wrongAnswerCount >= maxWrongAnswers)
            {
                GameOver();
                return;
            }
        }

        currentQuestionIndex++;
        LoadQuestion();
    }

    void UpdateHearts()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].sprite = i < (maxWrongAnswers - wrongAnswerCount) ? heartFull : heartEmpty;
        }
    }

    void ResetHearts()
    {
        foreach (Image heart in heartImages)
        {
            heart.sprite = heartFull;
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over: Kehabisan nyawa.");
        Time.timeScale = 1f;

        // Reset score ke 0
        Scoring.totalScore = 0;

        // Jika kamu menampilkan skor di UI juga, bisa reset di sana kalau perlu
        // scoreText.text = "0"; // kalau punya referensinya di sini

        SceneManager.LoadScene("Level1");
    }
}
