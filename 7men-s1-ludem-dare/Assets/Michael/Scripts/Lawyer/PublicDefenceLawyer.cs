using UnityEngine;

namespace Managers.Lawyer
{
    public class PublicDefenceLawyer : Lawyer
    {
        public GameObject barrier;

        public void SetBarrierPosition(Vector3 position)
        {
            barrier.transform.position = position;
        }
        
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
            ShieldOnOff(false);
            base.OnDeath();
        }

        void ShieldOnOff(bool on)
        {
            barrier.SetActive(on);
        }
    }
}