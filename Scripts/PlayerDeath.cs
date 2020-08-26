using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{

    public PlayerTrail playerTrailRef;
    public GameObject playerRef;
    public MeshCreator meshCreatorRef;

    public void KillPlayer()
    {
        playerRef.GetComponent<CharacterController>().enabled = false;
        playerRef.transform.position = playerTrailRef.deathLocation;
        playerRef.GetComponent<CharacterController>().enabled = true;
        meshCreatorRef.EmptyList();
    }

}
