// using UnityEngine;

// namespace collegeGame
// {
//     public class Skelet_idle : StateMachineBehaviour
//     {
//         private float timer;
//         private Transform player;
//         private float chaseRange = 10f;
//         private const float patrolTime = 5f;

//         override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//         {
//             timer = 0f;
//             player = GameObject.FindGameObjectWithTag("Player")?.transform;
//         }

//         override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//         {
//             timer += Time.deltaTime;
//             if (timer > patrolTime)
//                 animator.SetBool("Patrol", true);

//             if (player != null)
//             {
//                 float distance = Vector3.Distance(animator.transform.position, player.position);
//                 if (distance < chaseRange)
//                     animator.SetBool("Chasing", true);
//             }
//         }
//     }
// }
using UnityEngine;

namespace collegeGame
{
    public class Skelet_idle : StateMachineBehaviour
    {
        private float timer;
        private Transform player;
        private float chaseRange = 10f;
        private const float patrolTime = 5f;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            timer = 0f;
            FindPlayer();
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            timer += Time.deltaTime;
            if (timer > patrolTime)
                animator.SetBool("Patrol", true);

            CheckPlayerDistance(animator);
        }

        private void FindPlayer()
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        private void CheckPlayerDistance(Animator animator)
        {
            if (player != null)
            {
                float distance = Vector3.Distance(animator.transform.position, player.position);
                if (distance < chaseRange)
                    animator.SetBool("Chasing", true);
            }
        }
    }
}
