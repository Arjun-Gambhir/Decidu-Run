using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCreator : MonoBehaviour
{
    private AudioSource audioSource;

    public List<GameObject> playerOneLocations = new List<GameObject>();
    public GameObject growthNodeParent;
    public Vector3 centerPosition;
    public PlayerDeath playerDeathRef;
    public AudioClip pathComplete;
    public float decayDelayTime = 0.25f;

    private GameObject gameManager;
    //private int plantsChanged = 0;
    private bool canPlayAudio = true;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager");
    }


    public void AddToList(GameObject objectToAdd, List<GameObject> listToAddTo)
    {
        if (listToAddTo.Contains(objectToAdd))
        {
            Debug.Log("Already have: " + objectToAdd.name);
        }
        else
        {
            listToAddTo.Add(objectToAdd);
        }

        objectToAdd.transform.SetParent(growthNodeParent.transform);

    }

    public void EmptyList()
    {
        playerOneLocations.Clear();

        foreach (Transform child in growthNodeParent.transform)
        {
            Destroy(child.gameObject);
        }

        StopCoroutine("ChainDecay");

    }



    public void CreateAllTriangles(string _tag, List<GameObject> playerLocations)
    {
        if (playerLocations.Count > 2)
        {

            //plantsChanged = gameManager.GetComponent<LifeCounter>().growthCount;
            //Debug.Log("Plants Changed Before: " + gameManager.GetComponent<LifeCounter>().growthCount);

            centerPosition = CreateAverage(playerLocations);

            for (int i = 0; i < playerLocations.Count; i++)
            {
                if (i == playerLocations.Count - 1)
                {
                    CreateTriangle(playerLocations[i].transform.position, playerLocations[1].transform.position, centerPosition, _tag);
                }
                else
                {
                    CreateTriangle(playerLocations[i].transform.position, playerLocations[i + 1].transform.position, centerPosition, _tag);
                }
            }

            StartCoroutine(DestroyTriangles());
            EmptyList();

        }
        else
        {
           // Debug.Log("too small");
        }
    }


    public void CreateTriangle(Vector3 Pos1, Vector3 Pos2, Vector3 centerPos, string playerTag)
    {
        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[3];

        vertices[0] = Pos1;
        vertices[1] = Pos2;
        vertices[2] = centerPos;

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        GameObject newObject = new GameObject("Mesh", typeof(MeshFilter));

        newObject.GetComponent<MeshFilter>().mesh = mesh;

        newObject.AddComponent<MeshCollider>();
        newObject.tag = "Life";
        newObject.transform.parent = this.gameObject.transform;
    }

    public Vector3 CreateAverage(List<GameObject> playerLocations)
    {
        float xAverage = 0, ZAverage = 0;

        for (int i = 0; i < playerLocations.Count; i++)
        {
            xAverage += playerLocations[i].transform.position.x;
            ZAverage += playerLocations[i].transform.position.z;
        }

        xAverage = xAverage / (playerLocations.Count + 1f);
        ZAverage = ZAverage / (playerLocations.Count + 1f);

        return new Vector3(xAverage, 0f, ZAverage);
    }

    private IEnumerator DestroyTriangles()
    {
        pathCompleteAudio();
        yield return new WaitForSecondsRealtime(1.0f);
        
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public int FindInList(GameObject objectToFind)
    {
        for (int i = 0; i < playerOneLocations.Count; i++)
        {
            if (objectToFind == playerOneLocations[i])
            {
                return i;
            }
        }
        return -1;
    }


    public void DecayLine(int spot)
    {
        if (spot < playerOneLocations.Count) 
        {
            if (spot >= 0)
            {
                // Debug.Log(playerOneLocations[spot].transform.position);

                //start decay animation~  playerOneLocations[spot]
                playerOneLocations[spot].GetComponent<DecayMatLerp>().enabled = true;
                StartCoroutine(ChainDecay(spot+1));
            }
        }
        else if (spot < 0)
        {
            Debug.Log("spot too large");
        }
        else if (spot == playerOneLocations.Count)
        {
            playerDeathRef.KillPlayer();
            // Debug.Log("Player died");
        }
    }

    public IEnumerator ChainDecay(int newSpot)
    {
        yield return new WaitForSecondsRealtime(decayDelayTime);
        DecayLine(newSpot); //* 0.75f
    }

    private void pathCompleteAudio()
    {
        if(canPlayAudio)
        {
            audioSource.PlayOneShot(pathComplete);
        }
        canPlayAudio = false;
        StartCoroutine(WaitToPlayAudio());
        /*
        plantsChanged -= gameManager.GetComponent<LifeCounter>().growthCount;
        Mathf.Abs(plantsChanged);

        switch (plantsChanged)
        {
            case 0:
                audioSource.pitch = 0.8f;
                break;
            case 1:
                audioSource.pitch = 0.85f;
                break;
            case 2:
                audioSource.pitch = 0.9f;
                break;
            case 3:
                audioSource.pitch = 0.95f;
                break;
            case 4:
                audioSource.pitch = 1f;
                break;
            case 5:
                audioSource.pitch = 1.05f;
                break;
            case 6:
                audioSource.pitch = 1.1f;
                break;
            case 7:
                audioSource.pitch = 1.15f;
                break;
            case 8:
                audioSource.pitch = 1.2f;
                break;
            case 9:
                audioSource.pitch = 1.25f;
                break;
            default:
                audioSource.pitch = 1.35f;
                break;
        }

        Debug.Log("Plants Changed After: " + gameManager.GetComponent<LifeCounter>().growthCount);
        if (plantsChanged > 0)
        {
            
        }

        plantsChanged = 0;
        StartCoroutine(Test());
        */
    }

    IEnumerator WaitToPlayAudio()
    {
        yield return new WaitForSeconds(3);
        canPlayAudio = true;
        //Debug.Log("Plants Changed Way After: " + gameManager.GetComponent<LifeCounter>().growthCount);
    }


}
