using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecayMatLerp : MonoBehaviour
{
    public Material growthMat;
    public Material decayMat;
    public float lerpDuration = 1.0f;

    private float timeElapsed = 0f;
    private GameObject flowerBody;

    private void Start()
    {
        GameObject flower = transform.GetChild(0).gameObject;
        flowerBody = flower.transform.GetChild(1).gameObject;
        flowerBody.GetComponent<Renderer>().material = decayMat;
    }

    private void Update()
    {
        /*
        timeElapsed += Time.time;
        if (timeElapsed / lerpDuration <= 1f)
        {
            flowerBody.GetComponent<Renderer>().material.Lerp(growthMat, decayMat, timeElapsed / lerpDuration);
        }
        else
        {
            
        }
        */
    }
}
