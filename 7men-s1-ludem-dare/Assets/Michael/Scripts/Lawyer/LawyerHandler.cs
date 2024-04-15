using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers.Lawyer
{
    public enum LawyerType
    {
        PublicDefence,
        Prosecutor,
        Contract,
    }
    
    public class LawyerHandler : MonoBehaviour
    {
        [Header("References:")] public Transform parent;

        [Header("Lawyer Times")] 
        public float publicDefenceCooldownTime;
        public float prosecutorCooldownTime;
        public float contractCooldownTime;
        
        public float publicDefenceUseTime;
        public float prosecutorUseTime;
        public float contractUseTime;

        [Header("Lawyer Prefabs")] 
        public GameObject contractLawyerPrefab;
        public GameObject publicDefenceLawyerPrefab;
        public GameObject prosecutorLawyerPrefab;

        private GameObject _contractLawyer;
        private GameObject _prosecutorLawyer;
        private GameObject _publicDefenceLawyer;

        private Lawyer _contractLawyerStates;
        private Lawyer _publicDefenceLawyerStates;
        private Lawyer _prosecutorLawyerStates;

        private float _publicDefenceTimer;
        private float _prosecutorTimer;
        private float _contractTimer;
        
        private float _publicDefenceCooldownTimer;
        private float _prosecutorCooldownTimer;
        private float _contractCooldownTimer;

        private bool _publicDefenceInUse;
        private bool _prosecutorInUse;
        private bool _contractInUse;

        private bool _publicDefenceCooling;
        private bool _prosecutorCooling;
        private bool _contractorCooling;
        
        private EventManager _eventManager;
        
        private void Start()
        {
            _eventManager = EventManager.Instance;

            Setup();

            _eventManager.PlayerEvents.OnSpawnLawyer += HandleSpawnLawyerCall;
            
            HandleSpawnLawyerCall(LawyerType.Contract, Vector3.zero);
        }

        private void Setup()
        {
            //Timer Setup
            _prosecutorTimer = prosecutorUseTime;
            _publicDefenceTimer = publicDefenceUseTime;
            _contractTimer = contractUseTime;

            _publicDefenceCooldownTimer = publicDefenceCooldownTime;
            _prosecutorCooldownTimer = prosecutorCooldownTime;
            _contractCooldownTimer = contractCooldownTime;
            
            //Lawyer Setup
            _contractLawyerStates = contractLawyerPrefab.GetComponent<Lawyer>();
            _prosecutorLawyerStates = prosecutorLawyerPrefab.GetComponent<Lawyer>();
            _publicDefenceLawyerStates = publicDefenceLawyerPrefab.GetComponent<Lawyer>();
        }

        void HandleSpawnLawyerCall(LawyerType lawyer, Vector3 playerPos)
        {
            switch (lawyer)
            {
                //
                case LawyerType.Contract:
                    SpawnContractLawyer(playerPos);
                    break;
                
                case LawyerType.Prosecutor:
                    SpawnProsecutorLawyer(playerPos);
                    break;
                
                case LawyerType.PublicDefence:
                    SpawnPublicDefenceLawyer(playerPos);
                    break;
            }
        }

        private void SpawnContractLawyer(Vector3 playerPos)
        {
            if (_contractInUse) return;
            if (_contractorCooling) return;
            if (!_contractLawyerStates.spawned || _contractLawyer == null)
            {
                _contractLawyer = Instantiate(contractLawyerPrefab, playerPos, Quaternion.identity, parent);
                _contractLawyerStates = _contractLawyer.GetComponent<Lawyer>();
            }
            else
            {
                _contractLawyer.transform.position = playerPos;
                _contractLawyer.SetActive(true);
            }
                    
            _contractInUse = true;
            _contractLawyerStates.OnSpawn();
        }

        private void SpawnProsecutorLawyer(Vector3 playerPos)
        {
            if (_prosecutorInUse) return;
            if (_prosecutorCooling) return;
            if (!_prosecutorLawyerStates.spawned || _prosecutorLawyer == null)
            {
                _prosecutorLawyer = Instantiate(prosecutorLawyerPrefab, playerPos, Quaternion.identity, parent);
                _prosecutorLawyerStates = _prosecutorLawyer.GetComponent<Lawyer>();
            }
            else
            {
                _prosecutorLawyer.transform.position = playerPos;
                _prosecutorLawyer.SetActive(true);
            }
                    
            _prosecutorInUse = true;
            _prosecutorLawyerStates.OnSpawn();
        }

        private void SpawnPublicDefenceLawyer(Vector3 playerPos)
        {
            if (_publicDefenceInUse) return;
            if (_publicDefenceCooling) return;
            if (!_publicDefenceLawyerStates.spawned || _publicDefenceLawyer == null)
            {
                _publicDefenceLawyer = Instantiate(prosecutorLawyerPrefab, playerPos, Quaternion.identity, parent);
                _publicDefenceLawyerStates = _publicDefenceLawyer.GetComponent<Lawyer>();
            }
            else
            {
                _publicDefenceLawyer.transform.position = playerPos;
                _publicDefenceLawyer.SetActive(true);
            }
                    
            _publicDefenceInUse = true;
            _publicDefenceLawyerStates.OnSpawn();
            return;
        }

        private void Update()
        {
            ContractLawyerTimeSystem();
        }

        private void ContractLawyerTimeSystem()
        {
            if (_contractorCooling)
            {
                _contractCooldownTimer -= Time.deltaTime;
                if (_contractCooldownTimer <= 0)
                {
                    _contractorCooling = false;
                    _contractCooldownTimer = contractCooldownTime;
                }
            }

            if (_contractInUse)
            {
                _contractLawyerStates.OnAttack();
                _contractTimer -= Time.deltaTime;
                if (_contractTimer <= 0)
                {
                    _contractTimer = contractUseTime;
                    _contractInUse = false;
                    _contractLawyerStates.OnDeath();
                    _contractorCooling = true;
                    HandleSpawnLawyerCall(LawyerType.Contract, Vector3.zero);
                }
            }
        }
        
        private void ProsecutorLawyerTimeSystem()
        {
            if (_contractorCooling)
            {
                _contractCooldownTimer -= Time.deltaTime;
                if (_contractCooldownTimer <= 0)
                {
                    _contractorCooling = false;
                    _contractCooldownTimer = contractCooldownTime;
                }
            }

            if (_contractInUse)
            {
                _contractLawyerStates.OnAttack();
                _contractTimer -= Time.deltaTime;
                if (_contractTimer <= 0)
                {
                    _contractTimer = contractUseTime;
                    _contractInUse = false;
                    _contractLawyerStates.OnDeath();
                    _contractorCooling = true;
                    HandleSpawnLawyerCall(LawyerType.Contract, Vector3.zero);
                }
            }
        }
        
        private void PublicDefenceLawyerTimeSystem()
        {
            if (_contractorCooling)
            {
                _contractCooldownTimer -= Time.deltaTime;
                if (_contractCooldownTimer <= 0)
                {
                    _contractorCooling = false;
                    _contractCooldownTimer = contractCooldownTime;
                }
            }

            if (_contractInUse)
            {
                _contractLawyerStates.OnAttack();
                _contractTimer -= Time.deltaTime;
                if (_contractTimer <= 0)
                {
                    _contractTimer = contractUseTime;
                    _contractInUse = false;
                    _contractLawyerStates.OnDeath();
                    _contractorCooling = true;
                    HandleSpawnLawyerCall(LawyerType.Contract, Vector3.zero);
                }
            }
        }
    }
}