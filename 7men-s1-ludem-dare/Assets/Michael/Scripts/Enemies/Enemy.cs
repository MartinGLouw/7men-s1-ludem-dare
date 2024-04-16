using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Managers.Enemies
{
    public abstract class Enemy : MonoBehaviour, IDamagable<DamageData>
    {
        [Header("Settings")]
        public int enemyHealth = 50;
        public float attackingDistance = 4f;
        
        [Header("Attack Settings:")] 
        public Vector3 cubeAttackSize = new Vector3(2, 1, 2);
        public float attackCooldown = 0.8f;
        public LayerMask detectionLayer;
        public float meleeAttackForce = 80f;
        public DamageData damageData;
        public float retreatDistance = 8f;
        public bool isScared;
        
        [Header("References")] 
        public Animator enemyAnim;
        public GameObject bulletPrefab;
        public Transform firePoint;
        public GameObject playerRef;
        
        protected GameObject _player;
        protected bool canShoot = true;
        protected float _playerDistance;
        
        
        protected bool isAttacking = false;
        protected bool isWalking = false;
        
        // Current health for this specific enemy
        protected int currentHealth;
        protected NavMeshAgent navMeshAgent;
        

        public virtual void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.SetDestination(playerRef.transform.position);
            _player = playerRef;
            currentHealth = enemyHealth;
            enemyAnim.SetBool("IsWalking", true);
        }

        public virtual void Update()
        {
            _playerDistance = Vector3.Distance(playerRef.transform.position ,transform.position);

            if (_playerDistance < attackingDistance)
            {
                isAttacking = true;
                isWalking = false;
                enemyAnim.SetBool("IsWalking", false);
                navMeshAgent.isStopped = true;
                StartCoroutine(EnemyAttackBehavior());
            }
            else
            {
                isAttacking = false;
                if (!isAttacking)
                {
                    StopAllCoroutines();
                }
                navMeshAgent.isStopped = false;
                isWalking = true;
                enemyAnim.SetBool("IsWalking", true);
            } 
            
            if (_playerDistance <= retreatDistance)
            {
                // Move away from the player
                if (!isScared) return;
                var position = transform.position;
                Vector3 retreatDirection = (position - playerRef.transform.position).normalized;
                navMeshAgent.stoppingDistance = 0;
                navMeshAgent.SetDestination(position + retreatDirection);
            }
            else
            {
                if (isAttacking) return;
                EnemyMovement();
            }
            
        }

        public abstract void EnemyMovement();
        

        public abstract IEnumerator EnemyAttackBehavior();
        
        public void TakeDamage(DamageData data)
        {
            currentHealth -= data.dmgAmount;

            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
            }
            
        }
    }
}