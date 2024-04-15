using System.Collections;
using UnityEngine;

namespace Managers.Lawyer
{
    public abstract class Lawyer : MonoBehaviour
    {
        public EntityStates lawyerState;
        public Animator lawyerAnimator;

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
            lawyerAnimator.SetBool("IsExit", true);
            

            StartCoroutine(ExitSequence());
        }

        IEnumerator ExitSequence()
        {
            yield return new WaitForSeconds(1.5f);
            lawyerAnimator.SetBool("IsExit", false);
            gameObject.SetActive(false);
        }
    }
}