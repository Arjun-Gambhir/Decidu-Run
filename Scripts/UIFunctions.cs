using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIFunctions : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject[] UIPanel = null;


    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "playtest")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    UnpauseGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }

    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1.0f;
    }

    public void PauseGame()
    {
        OpenPanel(0);
        isPaused = true;
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void UnpauseGame()
    {
        ClosePanel(0);
        isPaused = false;
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void EndGame()
    {
        Debug.Log("Closing Game");
        Application.Quit();
    }

    public void ClosePanel(int panelNumber)
    {
        UIPanel[panelNumber].SetActive(false);
    }
    public void OpenPanel(int panelNumber)
    {
        UIPanel[panelNumber].SetActive(true);
    }

    

}
