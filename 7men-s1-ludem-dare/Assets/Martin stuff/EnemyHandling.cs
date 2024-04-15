using System;
using Managers;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHandling : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private GameObject _player;
    
    // Health values for different enemy types
    public int boxEnemyHealth = 50;
    public int shotgunEnemyHealth = 80;
    public int sniperEnemyHealth = 120;
    public int crowbarEnemyHealth = 70;
    public int sprayerEnemyHealth = 100;
    // Current health for this specific enemy
    private int currentHealth;

    public void Init(GameObject player)
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        _player = player;
        
        Debug.Log($"Player: {_player.name}");
        
        if (gameObject.CompareTag("BoxEnemy"))
        {
            currentHealth = boxEnemyHealth;
            BoxEnemy();
        }
        else if (gameObject.CompareTag("ShotgunEnemy"))
        {
            currentHealth = shotgunEnemyHealth;
            ShotgunEnemy();
        }
        else if (gameObject.CompareTag("SniperEnemy"))
        {
            currentHealth = sniperEnemyHealth;
            SniperEnemy();
        }
        else if (gameObject.CompareTag("CrowbarEnemy"))
        {
            currentHealth = crowbarEnemyHealth;
            CrowbarEnemy();
        }
        else if (gameObject.CompareTag("SprayerEnemy"))
        {
            currentHealth = sprayerEnemyHealth;
            SprayerEnemy();
        }
        
        else
        {
            Debug.Log("Enemy not found");
        }
    }

    private void BoxEnemy()
    {
        Vector3 targetPosition = _player.transform.position;
        navMeshAgent.SetDestination(targetPosition);
    }

    private void ShotgunEnemy()
    {
        Debug.Log("ShotgunEnemy behavior");
        float attackDistance = 6.0f; // The distance at which the enemy will stop and attack
        float retreatDistance = 1.0f; // The distance at which the enemy will start retreating

        Vector3 targetPosition = _player.transform.position;
        float distanceToPlayer = Vector3.Distance(transform.position, targetPosition);

        if (distanceToPlayer > attackDistance)
        {
            // Move towards the player
            navMeshAgent.stoppingDistance = attackDistance;
            navMeshAgent.SetDestination(targetPosition);
        }
        else if (distanceToPlayer <= attackDistance)
        {
            // Stop moving and attack
            navMeshAgent.stoppingDistance = 0;
            AttackPlayer();
        }

        if (distanceToPlayer <= retreatDistance)
        {
            // Move away from the player
            var position = transform.position;
            Vector3 retreatDirection = (position - targetPosition).normalized;
            navMeshAgent.stoppingDistance = 0;
            navMeshAgent.SetDestination(position + retreatDirection);
        }
    }

    private void AttackPlayer()
    {
        if (gameObject.CompareTag("BoxEnemy"))
        {
            Debug.Log("BoxEnemy is attacking");
        }
        else if (gameObject.CompareTag("ShotgunEnemy"))
        {
            Debug.Log("ShotgunEnemy is attacking");
        }
        else if (gameObject.CompareTag("SniperEnemy"))
        {
            Debug.Log("SniperEnemy is attacking");
        }
        else if (gameObject.CompareTag("CrowbarEnemy"))
        {
            Debug.Log("CrowbarEnemy is attacking");
        }
        else if (gameObject.CompareTag("SprayerEnemy"))
        {
            Debug.Log("SprayerEnemy is attacking");
        }
    }


    private void SniperEnemy()
    {
        Debug.Log("SniperEnemy behavior");
        float attackDistance = 15.0f; // The distance at which the enemy will stop and attack
        float retreatDistance = 8.0f; // The distance at which the enemy will start retreating

        Vector3 targetPosition = _player.transform.position;
        float distanceToPlayer = Vector3.Distance(transform.position, targetPosition);

        if (distanceToPlayer > attackDistance)
        {
            // Move towards the player
            navMeshAgent.stoppingDistance = attackDistance;
            navMeshAgent.SetDestination(targetPosition);
        }
        else if (distanceToPlayer <= attackDistance)
        {
            // Stop moving and attack
            navMeshAgent.stoppingDistance = 0;
            AttackPlayer();
        }

        if (distanceToPlayer <= retreatDistance)
        {
            // Move away from the player
            var position = transform.position;
            Vector3 retreatDirection = (position - targetPosition).normalized;
            navMeshAgent.stoppingDistance = 0;
            navMeshAgent.SetDestination(position + retreatDirection);
        }
        
    }

    private void CrowbarEnemy()
    {
        Debug.Log("CrowbarEnemy behavior");
        float attackDistance = 1.0f; // The distance at which the enemy will stop and attack

        Vector3 targetPosition = _player.transform.position;
        float distanceToPlayer = Vector3.Distance(transform.position, targetPosition);

        if (distanceToPlayer > attackDistance)
        {
            // Move towards the player
            navMeshAgent.SetDestination(targetPosition);
        }
        else if (distanceToPlayer <= attackDistance)
        {
            // Stop moving and attack
            navMeshAgent.SetDestination(transform.position);
            AttackPlayer();
            Debug.Log("CrowbarEnemy is attacking");
        }
    }
    private void SprayerEnemy()
    {
        Debug.Log("SprayerEnemy behavior");
        float attackDistance = 10.0f; // The distance at which the enemy will stop and attack
        float retreatDistance = 5.0f; // The distance at which the enemy will start retreating

        Vector3 targetPosition = _player.transform.position;
        float distanceToPlayer = Vector3.Distance(transform.position, targetPosition);

        if (distanceToPlayer > attackDistance)
        {
            // Move towards the player
            navMeshAgent.stoppingDistance = attackDistance;
            navMeshAgent.SetDestination(targetPosition);
        }
        else if (distanceToPlayer <= attackDistance)
        {
            // Stop moving and attack
            navMeshAgent.stoppingDistance = 0;
            AttackPlayer();
        }

        if (distanceToPlayer <= retreatDistance)
        {
            // Move away from the player
            var position = transform.position;
            Vector3 retreatDirection = (position - targetPosition).normalized;
            navMeshAgent.stoppingDistance = 0;
            navMeshAgent.SetDestination(position + retreatDirection);
        }    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle enemy death (e.g., play death animation, destroy GameObject, etc.)
        Destroy(gameObject);
    }
    
}