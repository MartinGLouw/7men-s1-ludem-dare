using System;
using Managers.Lawyer;
using UnityEngine;
using UnityEngine.AI;
using Managers.BossStates;

public enum Phase
{
    Phase1,
    Phase2,
    Phase3,
}


public class BossHandling : MonoBehaviour, IDamageable<Projectiles>
{
    
    public GameObject player;
    public BossStates bossState;
    public int bossHealth = 300;
    public Phase bossPhase;
    //Randomise melee attack
    public float bossMeleeDistance = 4;
    public float bossShootingDistance = 25f;

    public BossStateMachine bossStates;
    
    private NavMeshAgent navMeshAgent;
    private EventManager _eventManager;

    private float playerDistance;

    private void Start()
    {
        bossState = BossStates.BulletStorm;
        _eventManager = EventManager.Instance;
        navMeshAgent = GetComponent<NavMeshAgent>();
        bossPhase = Phase.Phase1;
        bossStates.OnStateEnter();
    }

    private void Update()
    {
        if (player)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
        
        //Distance
        playerDistance = Vector3.Distance(transform.position, player.transform.position);
        
        //Implement Phase
    }

    private void PhaseHandling(Phase phase, float distance)
    {
        switch (phase)
        {
            case Phase.Phase1:
                
                break;
        }
    }
    // Total health for the boss

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
        Vector3 shootDirection = player.transform.position - transform.position;
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

    public void TakeDamage(Projectiles value)
    {
        bossHealth -= value.damage;

        if (bossHealth == 200)
        {
            
        }

        if (bossHealth <= 0)
        {
            
        }
    }
}