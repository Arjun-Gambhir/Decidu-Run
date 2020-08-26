using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Life")
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        Destroy(this.gameObject);
    }

}
