using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialPopUp : MonoBehaviour
{
    public GameObject tutorialPanel;
    private bool isShowing = false;
    public TextMeshProUGUI textRef;

    public GameObject player;

    public List<string> tutorialText = new List<string>();
    public List<GameObject> tutorialNodes = new List<GameObject>();


    public float tutorialTime = 2.5f;
    public float maxDistance;


    private void Update()
    {
        for (int i = 0; i < tutorialNodes.Count; i++)
        {
            if (Vector3.Distance(player.transform.position, tutorialNodes[i].transform.position) < maxDistance)
            {
                OpenPopup(i);
            }
        }
    }

    private void OpenPopup(int textIndex)
    {
        if (!isShowing)
        {
            isShowing = true;
            textRef.text = tutorialText[textIndex];
            tutorialPanel.SetActive(true);
            StartCoroutine(KillPopUp(tutorialTime));
        }
        else
        {
            // Debug.Log("1");
        }


    }

    private IEnumerator KillPopUp(float timeToWait)
    {
        yield return new WaitForSecondsRealtime(timeToWait);
        tutorialPanel.SetActive(false);
        isShowing = false;

    }


}
