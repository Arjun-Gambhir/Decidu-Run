using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameEnd : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    public LifeCounter lifeCounterRef;

    public void CheckWin()
    {
        if (lifeCounterRef.CheckPercentVictory())
        {
            WinGame();
        }
        else
        {
            LoseGame();
        }
    }

    public void WinGame()
    {
        winPanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoseGame()
    {
        losePanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }



}
