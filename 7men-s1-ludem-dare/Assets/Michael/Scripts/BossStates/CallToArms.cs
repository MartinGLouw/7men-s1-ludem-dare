using System.Collections;
using UnityEngine;

namespace Managers.BossStates
{
    public class CallToArms : BossStateMachine
    {
        public Transform bossTransfrom;
        public Transform fleeDestination;

        private Vector3 origin;
        
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            origin = transform.position;
            Debug.Log("Call To Arms");
            bossAnimator.SetTrigger("OnJump");
            bossAnimator.SetBool("IsJumping", true);
            StartCoroutine(LerpPosition(fleeDestination.position, 1f));
        }

        public override void OnStateExit()
        {
            base.OnStateExit();

            StartCoroutine(LerpPosition(origin, 1f));
            bossAnimator.SetBool("IsJumping", false);
        }

        private IEnumerator LerpPosition(Vector3 targetPosition, float duration)
        {
            float time = 0;
            Vector3 startPosition = transform.position;

            while (time < duration)
            {
                bossTransfrom.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;  // Ensure the position is set exactly at the end
        }
        
    }
}