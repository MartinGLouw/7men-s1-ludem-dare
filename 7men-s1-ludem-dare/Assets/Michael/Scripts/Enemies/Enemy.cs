using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Managers.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        [Header("Settings")]
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

        public virtual void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.SetDestination(playerRef.transform.position);
            _player = playerRef;
            enemyAnim.SetBool("IsWalking", true);
        }

        public void Init(GameObject player)
        {
            playerRef = player;
            _player = player;
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.SetDestination(playerRef.transform.position);
            enemyAnim.SetBool("IsWalking", true);
        }

        public virtual void Update()
        {
            _playerDistance = Vector3.Distance(playerRef.transform.position ,transform.position);
            Vector3 retreatDirection = (transform.position - playerRef.transform.position).normalized * 5;
            transform.forward = -retreatDirection;

            if (_playerDistance < attackingDistance)
            {
                if (isScared)
                {
                    navMeshAgent.isStopped = true;
                }
                isAttacking = true;
                StartCoroutine(EnemyAttackBehavior());

                if (_playerDistance < retreatDistance)
                {
                    if (!isScared) return;
                    navMeshAgent.isStopped = false;
                    var position = transform.position;
                    navMeshAgent.SetDestination(position + retreatDirection);
                }
            }
            else
            {
                navMeshAgent.isStopped = false;
                isAttacking = false;
                navMeshAgent.SetDestination(playerRef.transform.position);
            }

        }
        

        public abstract IEnumerator EnemyAttackBehavior();
        
        private void StopActions()
        {
            navMeshAgent.isStopped = true;
            enemyAnim.SetBool("IsWalking", false);
            enemyAnim.SetBool("IsAttacking", false);
            this.enabled = false;
        }

    }
}