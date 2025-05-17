using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject pausePanel;  // ini buat masukin panel pause
    public GameObject settingPanel; // ini buat panel setting, gampang e sih dijadiin prefab dulu yang di main menu itu

    [Header("Buttons")]
    public Button pauseButton; // tombol pause yang ada di pojok UI
    public Button playButton;
    public Button settingButton;
    public Button backButton; // tombol back buat keluar dari setting e nab
    public Button homeButton;
    public Button restartButton; // tombol restart game

    void Start()
    {
        pausePanel.SetActive(false);
        settingPanel.SetActive(false);

        // Tambahkan listener untuk semua tombol
        pauseButton.onClick.AddListener(PauseGame);
        playButton.onClick.AddListener(ResumeGame);
        settingButton.onClick.AddListener(OpenSettings);
        backButton.onClick.AddListener(CloseSettings);
        homeButton.onClick.AddListener(GoToHome);
        restartButton.onClick.AddListener(RestartGame); // listener baru
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausePanel.activeSelf && !settingPanel.activeSelf)
            {
                PauseGame();
            }
            else if (pausePanel.activeSelf)
            {
                ResumeGame();
            }
            else if (settingPanel.activeSelf)
            {
                CloseSettings();
            }
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    public void OpenSettings()
    {
        settingPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void CloseSettings()
    {
        settingPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void GoToHome()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene("Main Menu"); // nama scene main menu mu nab lupa aku rek
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}