using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawing : MonoBehaviour
{
    public GameObject PointPrefab;
    public float PointMinDist = 2f;
    public float timeBetweenPoints;
    public float flowerSpread = 0.75f;

    private Vector3 lastPointPlaced;

    public MeshCreator meshCreatorRef;


    // Start is called before the first frame update
    void Start()
    {
        lastPointPlaced = gameObject.transform.position;
        StartCoroutine(WaitToPlacePoint());
        meshCreatorRef = GameObject.FindGameObjectWithTag("TerritoryManager").GetComponent<MeshCreator>();
    }


    IEnumerator WaitToPlacePoint()
    {
        yield return new WaitForSeconds(timeBetweenPoints);
        if (Vector3.Distance(gameObject.transform.position, lastPointPlaced) > PointMinDist)
        {
            Vector3 spawnPoint = gameObject.transform.position - new Vector3(Random.Range(-flowerSpread, flowerSpread), 0.5f, Random.Range(-flowerSpread, flowerSpread));
            GameObject point = Instantiate(PointPrefab, spawnPoint, Quaternion.identity);
            lastPointPlaced = point.transform.position;

            meshCreatorRef.AddToList(point, meshCreatorRef.playerOneLocations);
        }

        StartCoroutine(WaitToPlacePoint());
    }

}
