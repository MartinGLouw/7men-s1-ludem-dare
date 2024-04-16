using System;
using Managers.Lawyer;
using UnityEngine;
using UnityEngine.AI;
using Managers.BossStates;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum Phase
{
    Phase1,
    Phase2,
    Phase3,
}

[Serializable]
public struct AllBossStates
{
    public BulletStorm BulletStorm;
    public FrontKick FrontKick;
    public MegaStomp MegaStomp;
    public ShotgunStrike ShotgunStrike;
    public HeavyStrike HeavyStrike;
    public CallToArms CallToArms;
    public Flee Flee;
    public Idle Idle;
   
}


public class BossHandling : MonoBehaviour, IDamageable<float>
{
    [Header("references: ")]
    public Animator bossAnim;
    public GameObject player;
    public AllBossStates availableStates;
    public Transform gun;
    public BossHealthUI bossHealthUI;
    
    [Header("Boss State")]
    public Phase bossPhase;
    public BossStates bossState;
    public BossStateMachine currentState;
    
    [Header("Boss Settings")]
    public float bossHealth = 300;
    public float bossMeleeDistance = 4;
    public float bossShootingDistance = 25f;
    public float phaseCheckingInterval = 3f;
    public Vector2 attackCooldown = new Vector2(2, 5);
    public float bossCooldown = 30f;

    [Header("Debug")] 
    public bool takeDamage = false;

    private Rigidbody _bossRb;
    
    private NavMeshAgent _agent;
    private EventManager _eventManager;

    private float _playerDistance;
    private float _phaseCheckTimer;
    private float _bossCooldownTimer;
    private float _attackCooldownTimer;
    
    private bool _melee = false;
    private bool _flee = false;
    private bool _shoot = false;
    private bool _dead = false;

    private void Start()
    {
        bossAnim.SetFloat("SpeedMultiplier", 0.1f);
        
        _eventManager = EventManager.Instance;
        _agent = GetComponent<NavMeshAgent>();
        _bossRb = GetComponent<Rigidbody>();

        _phaseCheckTimer = phaseCheckingInterval;
        
        bossPhase = Phase.Phase1;
        currentState = availableStates.Idle;
        currentState.OnStateEnter();

        _attackCooldownTimer = Random.Range(attackCooldown.x, attackCooldown.y);
        _playerDistance = 100f;

        _shoot = false;
        _melee = false;
        
        _bossCooldownTimer = bossCooldown;
    }

    private void Update()
    {
        if (_dead) return;
        //Debugging
        if (takeDamage)
        {
            TakeDamage(100f);
            takeDamage = false;
        }
        
        if (player && !_flee)
        {
            _agent.SetDestination(player.transform.position);
        }
        
        //Distance
        _playerDistance = Vector3.Distance(transform.position, player.transform.position);

        Vector3 direction = player.transform.position - gun.position;
        gun.up = direction;
        
        //Implement Phase
        
        //Anim
        bossAnim.SetFloat("Speed", _bossRb.velocity.sqrMagnitude);

        //Phase check with distance check
        _phaseCheckTimer -= Time.deltaTime;
        if (_phaseCheckTimer <= 0)
        {
            PhaseHandling();
            _phaseCheckTimer = phaseCheckingInterval;
        }
        
        //Boss Cooldown
        if (_flee)
        {
            _bossCooldownTimer -= Time.deltaTime;
            if (_bossCooldownTimer <= 0)
            {
                _flee = false;
                _bossCooldownTimer = bossCooldown;
                currentState.ChangeState(availableStates.Idle);
                _agent.isStopped = false;
            }
        }
        
        
    }

    private void PhaseHandling()
    {
        _melee = _playerDistance < bossMeleeDistance;
        _shoot = _playerDistance < bossShootingDistance && !_melee;

        if (_melee) _shoot = false;
        
        switch (bossPhase)
        {
            case Phase.Phase1:
                if (_shoot) { currentState.ChangeState(availableStates.BulletStorm); }
                if (_melee) {currentState.ChangeState(availableStates.FrontKick);}
                
                if(!_melee && !_shoot) currentState.ChangeState(availableStates.Idle);
                break;
            case Phase.Phase2:
                if (_flee)
                {
                    currentState.ChangeState((availableStates.CallToArms));
                    return;
                }
                
                //Implement call to arms
                if (_shoot) { currentState.ChangeState(availableStates.ShotgunStrike); }
                if (_melee) { currentState.ChangeState(availableStates.HeavyStrike); }
                
                if(!_melee && !_shoot) currentState.ChangeState(availableStates.Idle);
                break;
            case Phase.Phase3:
                if(_melee) currentState.ChangeState(availableStates.MegaStomp);
                else
                {
                    currentState.ChangeState(availableStates.Idle);
                }
                break;
        }
    }

    public void TakeDamage(float dmg)
    {
        bossHealth -= dmg;
        bossHealthUI.UpdateBossHealth(bossHealth / 300);

        if (bossHealth <= 200 && bossHealth >= 100)
        {
            _flee = true;
            _eventManager.EnemyEvents.FireOnSpawnEnemies(2);
            bossPhase = Phase.Phase2;
            PhaseHandling();
        }
        
        if (bossHealth <= 100)
        {
            _flee = true;
            _agent.isStopped = true;
            bossPhase = Phase.Phase3;
            
            //bossMeleeDistance = bossShootingDistance;
            PhaseHandling();
        }

        if (bossHealth <= 0)
        {
            _eventManager.GameManagerEvents.FireEndGameEvent();
            bossAnim.SetTrigger("OnDeath");
        }
    }
}