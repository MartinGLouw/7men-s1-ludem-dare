using UnityEngine;

namespace Managers.Lawyer
{
    public abstract class Lawyer : MonoBehaviour
    {
        public EntityStates lawyerState;

        public bool spawned;

        public virtual void OnSpawn()
        {
            lawyerState = EntityStates.Spawning;
            spawned = true;
            Debug.Log("Lawyer Spawn");
        }

        public virtual void OnAttack()
        {
            if (lawyerState != EntityStates.Attacking)
            {
                lawyerState = EntityStates.Attacking;
            }
            
        }
        public virtual void OnDeath()
        {
            lawyerState = EntityStates.Death;
            gameObject.SetActive(false);
        }
    }
}