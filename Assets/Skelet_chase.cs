using UnityEngine;
using UnityEngine.AI;

namespace collegeGame
{
    public class Skelet_chase : StateMachineBehaviour
    {
        private NavMeshAgent agent;
        private Transform player;
        private float attackRange = 3f;
        private float chaseRange = 10f;
        private const float defaultSpeed = 2f;
        private const float chaseSpeed = 4f;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            agent = animator.GetComponent<NavMeshAgent>();
            agent.speed = chaseSpeed;
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (player != null)
            {
                agent.SetDestination(player.position);
                float distance = Vector3.Distance(animator.transform.position, player.position);

                if (distance < attackRange)
                {
                    animator.SetBool("Attack", true);
                }
                if (distance > chaseRange)
                {
                    animator.SetBool("Chasing", false);
                }
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            agent.ResetPath();
            agent.speed = defaultSpeed;
        }
    }
}
