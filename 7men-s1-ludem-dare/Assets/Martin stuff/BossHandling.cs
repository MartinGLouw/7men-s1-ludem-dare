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
    public Idle Idle;
   
}


public class BossHandling : MonoBehaviour, IDamageable<DamageData>
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
    public BossStateMachine meleeState;
    public BossStateMachine shootingState;
    
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
    private bool _bossFled = false;
    private bool _shoot = false;
    private bool _dead = false;

    private void OnEnable()
    {
        EventManager.Instance.GameManagerEvents.OnEndGame += StopActions;
        EventManager.Instance.GameManagerEvents.OnLoseGame += StopActions;
    }
    private void OnDisable()
    {
        EventManager.Instance.GameManagerEvents.OnEndGame -= StopActions;
        EventManager.Instance.GameManagerEvents.OnLoseGame -= StopActions;
    }

    private void Start()
    {
        _eventManager = EventManager.Instance;
        _agent = GetComponent<NavMeshAgent>();
        _bossRb = GetComponent<Rigidbody>();

        _phaseCheckTimer = phaseCheckingInterval;
        
        bossPhase = Phase.Phase1;
        currentState = availableStates.Idle;
        currentState.OnStateEnter();

        _attackCooldownTimer = Random.Range(attackCooldown.x, attackCooldown.y);
        _playerDistance = 100f;

        _dead = false;
        _flee = false;
        _agent.isStopped = false;
        _shoot = false;
        _melee = false;

        meleeState = availableStates.FrontKick;
        shootingState = availableStates.BulletStorm;
        
        _bossCooldownTimer = bossCooldown;
    }

    private void Update()
    {
        if (_dead) return;
        
        _agent.SetDestination(player.transform.position);
        
        //Distance
        _playerDistance = Vector3.Distance(transform.position, player.transform.position);

        Vector3 direction = player.transform.position - gun.position;
        gun.up = direction;
        
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
        
        _melee = _playerDistance < bossMeleeDistance;
        _shoot = _playerDistance < bossShootingDistance && !_melee;

        if (_flee) return;
        
        if (_shoot && !_melee)
        {
            currentState.ChangeState(shootingState);
        }

        if (_melee)
        {
            currentState.ChangeState(meleeState);
        }
        
    }

    private void PhaseHandling()
    {
        if (_melee) _shoot = false;
        
        switch (bossPhase)
        {
            case Phase.Phase1:
                if (_shoot)
                {
                    currentState.ChangeState(availableStates.BulletStorm);
                    shootingState = availableStates.BulletStorm;
                }

                if (_melee)
                {
                    currentState.ChangeState(availableStates.FrontKick);
                    meleeState = availableStates.FrontKick;
                }
                
                if(!_melee && !_shoot) currentState.ChangeState(availableStates.Idle);
                break;
            case Phase.Phase2:
                if (_flee)
                {
                    currentState.ChangeState((availableStates.CallToArms));
                    return;
                }
                
                //Implement call to arms
                if (_shoot)
                {
                    currentState = GetRandomState(new BossStateMachine[]
                    {
                        availableStates.ShotgunStrike,
                        availableStates.BulletStorm,
                    });
                    shootingState = currentState;
                    // currentState.ChangeState(availableStates.ShotgunStrike);
                }

                if (_melee)
                {
                    Debug.Log("randomise");
                    currentState = GetRandomState(new BossStateMachine[]
                    {
                        availableStates.FrontKick,
                        availableStates.HeavyStrike,
                    });
                    meleeState = currentState;
                }
                
                if(!_melee && !_shoot) currentState.ChangeState(availableStates.Idle);
                break;
            case Phase.Phase3:
                if (_shoot)
                {
                    currentState = GetRandomState(new BossStateMachine[]
                    {
                        availableStates.ShotgunStrike,
                        availableStates.BulletStorm,
                        availableStates.MegaStomp
                    });
                    shootingState = currentState;
                }

                if (_melee)
                {
                    currentState = GetRandomState(new BossStateMachine[]
                    {
                        availableStates.FrontKick,
                        availableStates.MegaStomp,
                        availableStates.HeavyStrike
                    });

                    meleeState = currentState;
                    //currentState.ChangeState(availableStates.MegaStomp);
                }
                else
                {
                    currentState.ChangeState(availableStates.Idle);
                }
                break;
        }
        
        currentState.OnStateEnter();
    }

    private BossStateMachine GetRandomState(BossStateMachine[] bossStates)
    {
        BossStateMachine randState = availableStates.Idle;

        int randomTier = Random.Range(0, bossStates.Length);
        randState = bossStates[randomTier];
        
        return randState;
    }

    private void HandleFlee()
    {
        if (_bossFled) return;

        _agent.isStopped = true;
        _flee = true;
        _eventManager.EnemyEvents.FireOnSpawnEnemies(2);
        _bossFled = true;
    }

    public void TakeDamage(DamageData data)
    {
        bossHealth -= data.dmgAmount;
        bossHealthUI.UpdateBossHealth(bossHealth / 300);

        if (bossHealth <= 200 && bossHealth >= 100)
        {
            HandleFlee();
            bossPhase = Phase.Phase2;
            PhaseHandling();
        }
        
        if (bossHealth <= 100)
        {
            _agent.speed = 5f;
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


    private void StopAllAnimations()
    {
        bossAnim.StopPlayback();  
        bossAnim.Rebind();        
    }

    private void StopActions()
    {
        StopAllAnimations();
        
        _dead = true;
        _agent.isStopped = true;
    }
}