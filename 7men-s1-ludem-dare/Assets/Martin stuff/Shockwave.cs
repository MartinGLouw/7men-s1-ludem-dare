using System;
using UnityEngine;
using UnityEngine.AI;

public class Shockwave : MonoBehaviour
{
    private float range;
    private int damage;
    private GameObject player;

    public void Initialize(float range, int damage, GameObject player)
    {
        this.range = range;
        this.damage = damage;
        this.player = player;
    }

    private void Update()
    {
        // Move the shockwave forward
        transform.Translate(Vector3.forward * Time.deltaTime);

        // Destroy the shockwave if it has traveled beyond its range
        if (Vector3.Distance(transform.position, player.transform.position) > range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the shockwave has collided with the player
        if (other.gameObject == player)
        {
            // Apply damage to the player
            //player.GetComponent<PlayerHandling>().TakeDamage(damage);

            // Destroy the shockwave
            Destroy(gameObject);
        }
    }
}