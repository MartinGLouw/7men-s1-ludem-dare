using System;
using System.Collections;
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
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    // Current health for this specific enemy
    private int currentHealth;

    private void FireBullet(float speed, int bulletCount = 1, float spreadAngle = 0)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * speed;

            // Apply spread if more than one bullet
            if (bulletCount > 1)
            {
                float angle = spreadAngle * (i - (bulletCount - 1) / 2.0f);
                bullet.transform.Rotate(0, angle, 0);
            }
        }
    }
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
            ShotgunAttack();
        }
        else if (gameObject.CompareTag("SniperEnemy"))
        {
            StartCoroutine(SniperAttack());
        }
        else if (gameObject.CompareTag("CrowbarEnemy"))
        {
            Debug.Log("CrowbarEnemy is attacking");
        }
        else if (gameObject.CompareTag("SprayerEnemy"))
        {
            StartCoroutine(SprayerAttack());
        }
    }
    private IEnumerator SniperAttack()
    {
        while (true)
        {
            FireBullet(50); // High-speed bullet
            yield return new WaitForSeconds(3); // Wait a few seconds between shots
        }
    }

    private void ShotgunAttack()
    {
        FireBullet(30, 5, 10); // 5 bullets with 10 degrees spread
    }

    private IEnumerator SprayerAttack()
    {
        while (true)
        {
            FireBullet(20, 3); // Burst of 3 bullets
            yield return new WaitForSeconds(0.5f); // Short burst delay
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
        }
    }

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


public class BossHandling : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public GameObject player;

    public int bossHealth = 300; // Total health for the boss

    private void Update()
    {
        Debug.Log("Boss Health: " + bossHealth);

        if (bossHealth > 200)
        {
            Phase1();
        }
        else if (bossHealth > 100)
        {
            Phase2();
        }
        else
        {
            Phase3();
        }
    }

    private void Phase1()
    {
        BulletStorm();
        FrontKick();
    }

    private void Phase2()
    {
        FrontKick();
        BulletStorm();
        HeavySwipe();
        ShotgunStrike();
        CallToArms();
        
    }

    private void Phase3()
    {
        FrontKick();
        BulletStorm();
        HeavySwipe();
        ShotgunStrike();
        CallToArms();
        MegaStomp();
    }

    private void BulletStorm()
    {
        // Implement BulletStorm behavior here
    }

    private void FrontKick()
    {
        // The range of the shockwave
        float shockwaveRange = 10.0f;

        // The damage of the kick
        int kickDamage = 20;

        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Check if the player is within the shockwave's range
        if (distanceToPlayer <= shockwaveRange)
        {
            // Play the kick animation
            // animator.SetTrigger("FrontKick");

            // Apply damage to the player
          //  player.GetComponent<PlayerHandling>().TakeDamage(kickDamage);
        }
    }


    private void HeavySwipe()
    {
        // The range of the swipe
        float swipeRange = 5.0f;

        // The damage of the swipe
        int swipeDamage = 30;

        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Check if the player is within the swipe's range
        if (distanceToPlayer <= swipeRange)
        {
            // Play the swipe animation
            // animator.SetTrigger("HeavySwipe");

            // Apply damage to the player
           // player.GetComponent<PlayerHandling>().TakeDamage(swipeDamage);
        }
    }


    private void ShotgunStrike()
    {
        // Implement ShotgunStrike behavior here
    }

    private void CallToArms()
    {
        // Implement CallToArms behavior here
    }

    private void MegaStomp()
    {
        // The range of the shockwave
        float shockwaveRange = 10.0f;

        // The damage of the stomp
        int stompDamage = 50;

        // Play the stomp animation
        // animator.SetTrigger("MegaStomp");

        // Create a new Shockwave object
        GameObject shockwave = new GameObject("Shockwave");
        shockwave.transform.position = transform.position;

        // Add a Rigidbody component to the Shockwave object to enable physics
        Rigidbody rb = shockwave.AddComponent<Rigidbody>();
        rb.useGravity = false; // The shockwave shouldn't fall to the ground

        // Add a Collider component to the Shockwave object to detect collisions
        SphereCollider collider = shockwave.AddComponent<SphereCollider>();
        collider.isTrigger = true; // The shockwave shouldn't physically block other objects

        // Add a Shockwave component to the Shockwave object to handle movement and collisions
        Shockwave shockwaveComponent = shockwave.AddComponent<Shockwave>();
        shockwaveComponent.Initialize(shockwaveRange, stompDamage, player);
    }
    /*private void MegaStomp()                                  Old implementation but left incase we need to revert
    {
        // The range of the shockwave
        float shockwaveRange = 10.0f;

        // The damage of the stomp
        int stompDamage = 50;

        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Check if the player is within the shockwave's range
        if (distanceToPlayer <= shockwaveRange)
        {
            // Play the stomp animation
            // animator.SetTrigger("MegaStomp");

            // Apply damage to the player
            //  player.GetComponent<PlayerHandling>().TakeDamage(stompDamage);
        }
    }*/


    public void TakeDamage(int damageAmount)
    {
        bossHealth -= damageAmount;
        if (bossHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle boss death (e.g., play death animation, destroy GameObject, etc.)
        Destroy(gameObject);
    }
}
