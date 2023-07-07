using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

public class MainMenuUi : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject instructions;
    public TMP_InputField playerNameInput;

    private void Start() {
        if(MainManager.Instance.playerName!=null)
        {
            playerNameInput.text=MainManager.Instance.playerName;
        }
    }

    public void StartGame()
    {
        MainManager.Instance.playerName=playerNameInput.text;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ViewInstructions()
    {
        mainMenu.SetActive(false);
        instructions.SetActive(true);
    }

    public void BackToMenu()
    {
        mainMenu.SetActive(true);
        instructions.SetActive(false);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
