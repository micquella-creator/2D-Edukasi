using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class FinishTrigger : MonoBehaviour
{
    public GameObject finishPanel;
    public TextMeshProUGUI finishText;
    public Button mainMenuButton;

    void Start()
    {
        finishPanel.SetActive(false);

        // Pastikan tombol punya listener (kalau belum diset di Inspector)
        mainMenuButton.onClick.AddListener(LoadMainMenu);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            finishPanel.SetActive(true);
        }
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu"); 
    }
}
