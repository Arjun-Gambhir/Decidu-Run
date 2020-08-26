using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float lookRadius = 10f;

    private AudioSource audioSource;
    private NavMeshAgent agent;
    private GameObject player;
    private Transform target;
    private GameObject[] pathPoints = null;
    private GameObject closestPoint;
    private float closestDist = 10000f;
    private bool ignorePlayer;

    public float wanderRadius = 3f;
    public float wanderTimer = 5f;
    private float timer;
    private bool isWandering;


    private MeshCreator meshCreatorRef;

    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
        meshCreatorRef = GameObject.FindGameObjectWithTag("TerritoryManager").GetComponent<MeshCreator>();
        audioSource = gameObject.GetComponent<AudioSource>();

        Follow();
    }

    void Update()
    {
        //AI Wandering
        if (isWandering)
        {
            timer += Time.deltaTime;

            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
        }
    }


    public void Follow()
    {
        //Find and move to player & points if they are within lookRadius
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (!ignorePlayer && (distanceToPlayer <= lookRadius || closestDist <= lookRadius))
        {
            isWandering = false;
            pathPoints = GameObject.FindGameObjectsWithTag("PathPoint");
            bool pathTest = false;

            try //Check if there are pathPoints
            {
                if (pathPoints[0] != null)
                    pathTest = true;
            }
            catch { }

            if (pathTest) //If at least one point was found, look for the closest one
            {
                closestPoint = pathPoints[0];
                closestDist = Vector3.Distance(closestPoint.transform.position, gameObject.transform.position);

                foreach (var point in pathPoints)
                {
                    float pointDist = Vector3.Distance(point.transform.position, gameObject.transform.position);
                    if (pointDist < closestDist)
                    {
                        closestPoint = point;
                        closestDist = pointDist;
                    }
                }
                target = closestPoint.transform;
            }
            else //If no points were found, set closestDist really high
            {
                closestDist = 10000f;
                pathPoints = null;
            }

            if (closestDist < 1f)//If player line has been reached
            {
                //START DECAYING PLAYER LINE
                //--------------------------
                audioSource.Play();
                meshCreatorRef.DecayLine(meshCreatorRef.FindInList(closestPoint));

                ignorePlayer = true;
                StartCoroutine(WaitToChasePlayer());
            }

            try
            {
                agent.SetDestination(target.position);
            }
            catch { }
        }
        else
        {
            isWandering = true;
        }

        StartCoroutine(CheckForNewTarget());
    }



    //Generate a random point within a sphere to move to (wandering)
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    IEnumerator WaitToChasePlayer()
    {
        yield return new WaitForSeconds(6f);
        ignorePlayer = false;
    }

    IEnumerator CheckForNewTarget()
    {
        yield return new WaitForSeconds(0.5f);
        Follow();
    }
}
