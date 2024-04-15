using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DespawnScript : MonoBehaviour
{

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "WorldBorder")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && gameObject.tag == "EnemyProjectile")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Enemy" && gameObject.tag == "PlayerProjectile")
        {
            Destroy(gameObject);
        }
    }
}
