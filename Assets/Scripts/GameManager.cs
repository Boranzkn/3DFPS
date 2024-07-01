using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Score
    [SerializeField] private TMP_Text scoreText;
    private int score = 0;

    // Panels
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject winPanel;
    private bool isGamePaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = "SCORE: " + score;
    }

    void PauseGame()
    {
        pausePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        isGamePaused = true;
    }
    
    void ResumeGame()
    {
        pausePanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        isGamePaused = false;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
}
