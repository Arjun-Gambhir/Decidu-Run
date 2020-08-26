using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    private MeshCreator meshCreatorRef;

    public GameObject[] crystals;
    public float maxDistance;

    public Vector3 deathLocation;

    private void Start()
    {
        meshCreatorRef = GameObject.FindGameObjectWithTag("TerritoryManager").GetComponent<MeshCreator>();
        deathLocation = transform.position;
    }

    private void Update()
    {
        foreach (GameObject crystal in crystals)
        {
            if (Vector3.Distance(transform.position, crystal.transform.position) < maxDistance)
            {
                meshCreatorRef.CreateAllTriangles("Life", meshCreatorRef.playerOneLocations);
                deathLocation = transform.position;
            }

        }

    }



}
