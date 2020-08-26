using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialConditions : MonoBehaviour
{
    public List<GameObject> tutorialPlants = new List<GameObject>();
    public List<GameObject> tutorialDoors = new List<GameObject>();
    public int totalDoors = 0;

    private void Start()
    {
        totalDoors = tutorialDoors.Count;
    }
    private void Update()
    {
        for (int i = 0; i < tutorialPlants.Count; i++)
        {
            if (tutorialPlants[i].GetComponent<LifeDeathController>().isGrowth)
            {
                Destroy(tutorialDoors[i]);
                totalDoors -= 1;

                if (totalDoors <= 0)
                {
                    StartCoroutine(killThis());
                }
            }
        }
    }

    private IEnumerator killThis()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        Destroy(this);
    }

}
