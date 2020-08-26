using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeDeathController : MonoBehaviour
{
    public Material lifeMaterial;
    public Material deathMaterial;

    public LifeCounter lifeCounterRef;

    public bool isGrowth;

    private Renderer rendRef;

    private void Start()
    {
        rendRef = GetComponent<Renderer>();
        lifeCounterRef = GameObject.FindGameObjectWithTag("GameMan").GetComponent<LifeCounter>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Life")
        {
            MakeAlive();
        }
        if (other.tag == "Death")
        {
            MakeDead();
        }
    }

    private void MakeAlive()
    {
        isGrowth = true;
        rendRef.material = lifeMaterial;
        transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);

        lifeCounterRef.CheckLifeStatus();
    }

    private void MakeDead()
    {
        isGrowth = false;
        rendRef.material = deathMaterial;
        transform.localScale = new Vector3(1f, 1f, 1f);

        lifeCounterRef.CheckLifeStatus();
    }
}
