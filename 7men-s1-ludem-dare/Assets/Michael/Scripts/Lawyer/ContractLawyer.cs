using System;
using UnityEngine;

namespace Managers.Lawyer
{
    public class ContractLawyer : Lawyer
    {
        
        public override void OnSpawn()
        {
            base.OnSpawn();
        }

        public override void OnAttack()
        {
            base.OnAttack();
            Debug.Log("Contract Attacking");
        }

        public override void OnDeath()
        {
            Debug.Log("Contract Death");
            base.OnDeath();
            
        }
    }
}