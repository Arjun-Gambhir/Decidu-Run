using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCamera : MonoBehaviour
{
    private Transform playerCam;

    // Update is called once per frame
    void Update()
    {
        playerCam = Camera.main.transform;
        gameObject.transform.LookAt(playerCam.position);
        gameObject.transform.Rotate(0f, 180f, 0f);
    }
}
