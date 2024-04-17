using UnityEngine;

namespace Managers.Lawyer
{
    public class PublicDefenceLawyer : Lawyer
    {
        public GameObject barrier;
        
        public override void OnSpawn()
        {
            base.OnSpawn();
            ShieldOnOff(true);
        }

        public override void OnAttack()
        {
            base.OnAttack();
        }

        public override void OnDeath()
        {
            base.OnDeath();
            ShieldOnOff(false);
        }

        void ShieldOnOff(bool on)
        {
            
            barrier.SetActive(on);
        }
    }
}