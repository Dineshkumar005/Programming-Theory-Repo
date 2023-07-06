using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManagerUi : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text scoreTxtGo;
    public TMP_Text highScoreText;
    public Button pauseBtn;
    public GameObject pauseMenu;
    public GameObject gameUi;
    public GameObject gameOverUi;

    private GameManager gameManager;

    private void Start() {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }

    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        gameManager.isGamePaused = true;
        Time.timeScale = 0;
        pauseBtn.gameObject.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameManager.isGamePaused = false;
        pauseMenu.SetActive(false);
        pauseBtn.gameObject.SetActive(true);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    public void GameOver()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseBtn.gameObject.SetActive(false);
        gameUi.SetActive(false);
        gameOverUi.SetActive(true);
        scoreTxtGo.text = "score : " + gameManager.score;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
