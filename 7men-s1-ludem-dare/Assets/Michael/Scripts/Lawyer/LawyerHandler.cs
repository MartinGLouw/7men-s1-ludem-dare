using System;
using UnityEngine;
using UnityEngine.UI;
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
        public Transform player;

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

        [Header("UI View")] 
        public Image pImage;
        public Image pbImage;
        public Image cImage;

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

        public void OnContractCalled()
        {
            HandleSpawnLawyerCall(LawyerType.Contract, player.position);
        }

        public void OnProsecutorCalled()
        {
            HandleSpawnLawyerCall(LawyerType.Prosecutor, player.position);
        }
        
        public void OnPublicDefenceCall()
        {
            HandleSpawnLawyerCall(LawyerType.PublicDefence, player.position);
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
            UpdateImages();
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
            UpdateImages();
        }

        private void SpawnPublicDefenceLawyer(Vector3 playerPos)
        {
            if (_publicDefenceInUse) return;
            if (_publicDefenceCooling) return;
            if (!_publicDefenceLawyerStates.spawned || _publicDefenceLawyer == null)
            {
                _publicDefenceLawyer = Instantiate(publicDefenceLawyerPrefab, playerPos, Quaternion.identity, parent);
                _publicDefenceLawyerStates = _publicDefenceLawyer.GetComponent<Lawyer>();
            }
            else
            {
                _publicDefenceLawyer.transform.position = playerPos;
                _publicDefenceLawyer.SetActive(true);
            }
                    
            _publicDefenceInUse = true;
            _publicDefenceLawyerStates.OnSpawn();
            UpdateImages();
        }

        private void Update()
        {
            ContractLawyerTimeSystem();
            PublicDefenceLawyerTimeSystem();
            ProsecutorLawyerTimeSystem();
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
                    UpdateImages();
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
                    UpdateImages();
                }
            }
        }
        
        private void ProsecutorLawyerTimeSystem()
        {
            if (_prosecutorCooling)
            {
                _prosecutorCooldownTimer -= Time.deltaTime;
                if (_prosecutorCooldownTimer <= 0)
                {
                    _prosecutorCooling = false;
                    _prosecutorCooldownTimer = prosecutorCooldownTime;
                    UpdateImages();
                }
            }

            if (_prosecutorInUse)
            {
                _prosecutorLawyerStates.OnAttack();
                _prosecutorTimer -= Time.deltaTime;
                if (_prosecutorTimer <= 0)
                {
                    _prosecutorTimer = prosecutorUseTime;
                    _prosecutorInUse = false;
                    _prosecutorLawyerStates.OnDeath();
                    _prosecutorCooling = true;
                    UpdateImages();
                }
            }
        }
        
        private void PublicDefenceLawyerTimeSystem()
        {
            if (_publicDefenceCooling)
            {
                _publicDefenceCooldownTimer -= Time.deltaTime;
                if (_publicDefenceCooldownTimer <= 0)
                {
                    _publicDefenceCooling = false;
                    UpdateImages();
                    _publicDefenceCooldownTimer = publicDefenceCooldownTime;
                }
            }

            if (_publicDefenceInUse)
            {
                _publicDefenceLawyerStates.OnAttack();
                _publicDefenceTimer -= Time.deltaTime;
                if (_publicDefenceTimer <= 0)
                {
                    _publicDefenceTimer = publicDefenceUseTime;
                    _publicDefenceInUse = false;
                    _publicDefenceLawyerStates.OnDeath();
                    _publicDefenceCooling = true;
                    UpdateImages();
                }
            }
        }

        private void UpdateImages()
        {
            if (_contractInUse || _contractorCooling) cImage.color = Color.red;
            else cImage.color = Color.white;

            if (_prosecutorInUse || _prosecutorCooling) pImage.color = Color.red;
            else pImage.color = Color.white;
            
            if (_publicDefenceInUse || _publicDefenceCooling) pbImage.color = Color.red;
            else pbImage.color = Color.white;
        }
    }
}