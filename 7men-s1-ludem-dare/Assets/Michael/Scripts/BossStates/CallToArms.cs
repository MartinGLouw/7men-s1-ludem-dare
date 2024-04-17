using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Managers.BossStates
{
    public class CallToArms : BossStateMachine
    {
        public Transform bossTransfrom;
        public Transform fleeDestination;
        public Transform origin;
        public float jumpSpeed = 1f;
        
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            bossAnimator.SetTrigger("OnJump");
            bossAnimator.SetBool("IsJumping", true);
            StartCoroutine(LerpPosition(fleeDestination.position, 1f, true));
        }

        public override void OnStateExit()
        {
            base.OnStateExit();

            StopAllCoroutines();
            StartCoroutine(LerpPosition(origin.position, jumpSpeed, false));
            
        }

        private IEnumerator LerpPosition(Vector3 targetPosition, float duration, bool animate)
        {
            NavMeshAgent agent = bossTransfrom.GetComponent<NavMeshAgent>();
            agent.enabled = false;
            float time = 0;
            Vector3 startPosition = transform.position;

            while (time < duration)
            {
                bossTransfrom.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;
            agent.enabled = true;

            if (!animate)
            {
                bossAnimator.SetBool("IsJumping", false);
            }
        }
        
    }
}