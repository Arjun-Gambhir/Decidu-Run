using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timer;
    public float startTime;

    public TextMeshProUGUI timerText;
    public GameEnd gameEndRef;

    private void Start()
    {
        timer = startTime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            gameEndRef.CheckWin();
        }
        else
        {
            timerText.text = ((int)timer).ToString();
        }
    }

}
