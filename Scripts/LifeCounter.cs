using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounter : MonoBehaviour
{
    public List<GameObject> livingObjects = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();

    public int growthCount = 0;
    public int decayCount = 0;

    public float percentVictory = 1.0f;

    public GameEnd gameEndRef;
    public bool isChecking = false;

    public float percentage;

    private void Start()
    {
        CheckLifeStatus();
    }

    public void CheckLifeStatus()
    {

        if (!isChecking)
        {
            growthCount = 0;
            decayCount = 0;

            isChecking = true;
            foreach (GameObject lifeObjects in livingObjects)
            {

                if (lifeObjects.GetComponent<LifeDeathController>().isGrowth)
                {
                    growthCount++;
                }
                else
                {
                    decayCount++;
                }
            }

            StartCoroutine(DelayGameEnd());

            //if (CheckPercentVictory())
            //{
            //    gameEndRef.WinGame();
            //}
        }

    }


    public bool CheckPercentVictory()
    {
        percentage = (float)growthCount / (((float)growthCount) + (float)decayCount);


        if ((float)growthCount / (((float)growthCount) + (float)decayCount) >= percentVictory)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator DelayGameEnd()
    {

        yield return new WaitForSeconds(1.0f);
        isChecking = false;
        if (CheckPercentVictory())
        {
            gameEndRef.WinGame();
        }
    }

}
