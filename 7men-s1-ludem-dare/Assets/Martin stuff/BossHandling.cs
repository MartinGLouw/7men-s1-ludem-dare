using System;
using Managers.Lawyer;
using UnityEngine;
using UnityEngine.AI;
using Managers.BossStates;
using UnityEngine.Serialization;

public enum Phase
{
    Phase1,
    Phase2,
    Phase3,
}

[Serializable]
public struct AllBossStates
{
    public BossStateMachine BulletStorm;
    public BossStateMachine FrontKick;
    public BossStateMachine MegaStomp;
    public BossStateMachine ShotgunStrike;
    public BossStateMachine HeavyStrike;
    public BossStateMachine CallToArms;
    public BossStateMachine Flee;
    public BossStateMachine Idle;
    
}


public class BossHandling : MonoBehaviour, IDamageable<Projectiles>
{
    [Header("references: ")]
    public Animator bossAnim;
    public GameObject player;
    public AllBossStates availableStates;
    
    [Header("Boss State")]
    public Phase bossPhase;
    public BossStates bossState;
    public BossStateMachine currentState;
    
    [Header("Boss Settings")]
    public int bossHealth = 300;
    public float bossMeleeDistance = 4;
    public float bossShootingDistance = 25f;
    public float phaseCheckingInterval = 3f;
    public Vector2 attackCooldown = new Vector2(2, 5);

    private Rigidbody _bossRb;
    
    private NavMeshAgent _agent;
    private EventManager _eventManager;

    private float _playerDistance;
    private float _phaseCheckTimer;

    private void Start()
    {
        _eventManager = EventManager.Instance;
        _agent = GetComponent<NavMeshAgent>();
        _bossRb = GetComponent<Rigidbody>();

        _phaseCheckTimer = phaseCheckingInterval;
        
        bossPhase = Phase.Phase1;
        currentState = availableStates.Idle;
    }

    private void Update()
    {
        if (player)
        {
            _agent.SetDestination(player.transform.position);
        }
        
        //Distance
        _playerDistance = Vector3.Distance(transform.position, player.transform.position);
        
        //Implement Phase
        
        //Anim
        bossAnim.SetFloat("Speed", _bossRb.velocity.sqrMagnitude);

        _phaseCheckTimer -= Time.deltaTime;
        if (_phaseCheckTimer <= 0)
        {
            PhaseHandling();
            _phaseCheckTimer = phaseCheckingInterval;
        }
    }

    private void PhaseHandling()
    {
        bool melee = _playerDistance < bossMeleeDistance;
        bool shoot = _playerDistance < bossShootingDistance && !melee;

        if (melee) shoot = false;
        
        switch (bossPhase)
        {
            case Phase.Phase1:
                if (shoot) { currentState.ChangeState(availableStates.BulletStorm); }
                if (melee) {currentState.ChangeState(availableStates.FrontKick);}
                break;
            case Phase.Phase2:
                //Implement call to arms
                if (shoot) { currentState.ChangeState(availableStates.ShotgunStrike); }
                if(melee) {currentState.ChangeState(availableStates.HeavyStrike);}
                break;
            case Phase.Phase3:
                currentState.ChangeState(availableStates.MegaStomp);
                break;
        }
    }

    public void TakeDamage(Projectiles value)
    {
        bossHealth -= value.damage;

        if (bossHealth <= 200 && bossHealth >= 100)
        {
            bossPhase = Phase.Phase2;
            PhaseHandling();
        }
        
        if (bossHealth <= 100)
        {
            bossPhase = Phase.Phase3;
            PhaseHandling();
        }

        if (bossHealth <= 0)
        {
            _eventManager.GameManagerEvents.FireEndGameEvent();
        }
    }
}