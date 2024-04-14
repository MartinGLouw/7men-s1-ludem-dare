using System.Collections;
using System.Collections.Generic;
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
}
