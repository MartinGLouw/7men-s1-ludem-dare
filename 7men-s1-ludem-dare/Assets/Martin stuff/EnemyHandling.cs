using System;
using System.Collections;
using Managers;
using Managers.Lawyer;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHandling : MonoBehaviour
{
    
    // Health values for different enemy types
    [Header("Settings")]
    public int boxEnemyHealth = 50;
    public int shotgunEnemyHealth = 80;
    public int sniperEnemyHealth = 120;
    public int crowbarEnemyHealth = 70;
    public int sprayerEnemyHealth = 100;

    [Header("Attack Settings:")] 
    public Vector3 cubeAttackSize = new Vector3(2, 1, 2);
    public LayerMask detectionLayer;
    public float meleeAttackForce = 80f;
    public DamageData damageData;

    [Header("References")] 
    public Animator enemyAnim;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject playerRef;
    
    private GameObject _player;
    private bool canShoot = true;
    
    private bool isAttacking = false;
    private float attackDelay = 0.0f;
    // Current health for this specific enemy
    private int currentHealth;
    private NavMeshAgent navMeshAgent;

    private Collider[] _playerCollider = new Collider[3];
    

    private void Start()
    {
        
    }

    public void Update()
    {
        Init(playerRef);
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

    


    private IEnumerator StartAttack()
    {
        isAttacking = true; // Start attacking

        // Choose the attack method based on the enemy type
        if (gameObject.CompareTag("BoxEnemy"))
        {
            attackDelay = 0.8f; // Set delay for BoxEnemy
            StartCoroutine(FighterAttack());
        }
        else if (gameObject.CompareTag("ShotgunEnemy"))
        {
            attackDelay = 4.0f; // Set delay for ShotgunEnemy
            StartCoroutine(ShotgunAttack());
        }
        else if (gameObject.CompareTag("SniperEnemy"))
        {
            attackDelay = 3.0f; // Set delay for SniperEnemy
            StartCoroutine(SniperAttack());
        }
        else if (gameObject.CompareTag("CrowbarEnemy"))
        {
            attackDelay = 1.0f; // Set delay for CrowbarEnemy
            StartCoroutine(CrowbarAttack());
        }
        else if (gameObject.CompareTag("SprayerEnemy"))
        {
            attackDelay = 0.5f; // Set delay for SprayerEnemy
            StartCoroutine(SprayerAttack());
        }

        yield return new WaitForSeconds(attackDelay); // Wait for the attack delay

        isAttacking = false; // Stop attacking
    }

   private IEnumerator CrowbarAttack()
{
    float attackCooldown = 1.0f; // Time between attacks
    float attackRange = 1.5f; // Distance at which the enemy can attack

    while (true)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

        if (distanceToPlayer <= attackRange && isAttacking)
        {
            // Play attack animation 

            Physics.OverlapBoxNonAlloc(firePoint.position, cubeAttackSize, _playerCollider, Quaternion.identity, detectionLayer);
            
            foreach (var hit in _playerCollider)
            {
                Debug.Log("frontKick");
                if (hit == null) continue;
                if (hit.CompareTag("Player"))
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();
                    Vector3 direction = hit.transform.position - transform.position;
                    rb.AddForce(direction * meleeAttackForce);

                    if (hit.TryGetComponent<IDamageable<DamageData>>(out IDamageable<DamageData> player))
                    {
                        player.TakeDamage(damageData);
                    }
                }
                //
            }
            // Cause damage to the player
            //_player.GetComponent<PlayerHealth>().TakeDamage(10); // Placeholder damage value

            isAttacking = false; // Stop attacking after one attack
            yield return new WaitForSeconds(attackCooldown);
        }

        yield return null; 
    }
}

private IEnumerator FighterAttack()
{
    float attackCooldown = 0.8f; 
    float attackRange = 2.0f;  

    while (true)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

        if (distanceToPlayer <= attackRange && isAttacking)
        {
            // Play attack animation

            Physics.OverlapBoxNonAlloc(firePoint.position, cubeAttackSize, _playerCollider, Quaternion.identity, detectionLayer);

            foreach (var hit in _playerCollider)
            {
                Debug.Log("frontKick");
                if (hit == null) continue;
                if (hit.CompareTag("Player"))
                {
                    Rigidbody rb = hit.GetComponent<Rigidbody>();
                    Vector3 direction = hit.transform.position - transform.position;
                    rb.AddForce(direction * meleeAttackForce);

                    if (hit.TryGetComponent<IDamageable<DamageData>>(out IDamageable<DamageData> player))
                    {
                        player.TakeDamage(damageData);
                    }
                }
            }

            isAttacking = false; // Stop attacking after one attack
            yield return new WaitForSeconds(attackCooldown);
        }

        yield return null; 
    }
}

private IEnumerator SniperAttack()
{
    if (canShoot)
    {
        canShoot = false;
        // GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        // bullet.GetComponent<Rigidbody>().velocity = firePoint.forward * 100;
        yield return new WaitForSeconds(5); // Wait for 5 seconds before the next attack
        canShoot = true;
    }
}

private IEnumerator ShotgunAttack()
{
    if (canShoot)
    {
        canShoot = false;
        for (int i = 0; i < 5; i++)
        {
            // GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Vector3 spread = new Vector3((i - 2) * 1, 0, 0); // Adjust the 2 here to control the spread
            Vector3 forward = firePoint.forward * 90;
            bullet.GetComponent<Rigidbody>().velocity = spread + forward;
        }
        yield return new WaitForSeconds(5);
        canShoot = true;
    }
}

private IEnumerator SprayerAttack()
{
    if (canShoot)
    {
        canShoot = false;
        for (int i = 0; i < 8; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().velocity = firePoint.forward * 80;

            yield return new WaitForSeconds(0.1f); // Adjust this to control the speed of the rapid fire
        }
        yield return new WaitForSeconds(5); // Wait for 5 seconds before the next attack
        canShoot = true;
    }
}


    private void BoxEnemy()
    {
        Vector3 targetPosition = _player.transform.position;
        navMeshAgent.SetDestination(targetPosition);
    }

    private void ShotgunEnemy()
    {
        float attackDistance = 20.0f; // The distance at which the enemy will stop and attack
        float retreatDistance = 5.0f; // The distance at which the enemy will start retreating

        Vector3 targetPosition = _player.transform.position;
        float distanceToPlayer = Vector3.Distance(transform.position, targetPosition);

        // Make the enemy always face the player
        transform.LookAt(_player.transform);

        if (distanceToPlayer > attackDistance)
        {
            // Move towards the player
            navMeshAgent.stoppingDistance = attackDistance;
            navMeshAgent.SetDestination(targetPosition);
            Debug.Log("ShotgunEnemy is moving");
        }
        else if (distanceToPlayer <= attackDistance)
        {
            // Stop moving and attack
            navMeshAgent.stoppingDistance = 0;
            Debug.Log("ShotgunEnemy is attacking");
            StartCoroutine(StartAttack());
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


    private void SniperEnemy()
{
    Debug.Log("SniperEnemy behavior");
    float attackDistance = 50.0f; // The distance at which the enemy will stop and attack
    float retreatDistance = 8.0f; // The distance at which the enemy will start retreating
    Vector3 targetPosition = _player.transform.position;
    float distanceToPlayer = Vector3.Distance(transform.position, targetPosition);

    // Make the enemy always face the player
    transform.LookAt(_player.transform);

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
        StartCoroutine(StartAttack());
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

    // Make the enemy always face the player
    transform.LookAt(_player.transform);

    if (distanceToPlayer > attackDistance)
    {
        // Move towards the player
        navMeshAgent.SetDestination(targetPosition);
    }
    else if (distanceToPlayer <= attackDistance)
    {
        // Stop moving and attack
        navMeshAgent.SetDestination(transform.position);
        StartCoroutine(StartAttack());
        Debug.Log("CrowbarEnemy is attacking");
    }
}

private void SprayerEnemy()
{
    Debug.Log("SprayerEnemy behavior");
    float attackDistance = 40.0f; // The distance at which the enemy will stop and attack
    float retreatDistance = 5.0f; // The distance at which the enemy will start retreating
    Vector3 targetPosition = _player.transform.position;
    float distanceToPlayer = Vector3.Distance(transform.position, targetPosition);

    // Make the enemy always face the player
    transform.LookAt(_player.transform);

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
        StartCoroutine(StartAttack());
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