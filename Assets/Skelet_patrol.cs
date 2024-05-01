// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AI;

// namespace collegeGame
// {
//     public class Skelet_patrol : StateMachineBehaviour
//     {
//         private float timer;
//         private List<Transform> points = new List<Transform>();
//         private NavMeshAgent agent;
//         private Transform player;
//         private float chaseRange = 10f;
//         private const float patrolTime = 5f;

//         override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//         {
//             timer = 0f;
//             GameObject pointsObject = GameObject.FindGameObjectWithTag("Points");
//             if (pointsObject != null)
//             {
//                 foreach (Transform t in pointsObject.transform)
//                 {
//                     points.Add(t);
//                 }
//             }

//             agent = animator.GetComponent<NavMeshAgent>();
//             if (points.Count > 0)
//             {
//                 agent.SetDestination(points[Random.Range(0, points.Count)].position);
//             }

//             player = GameObject.FindGameObjectWithTag("Player")?.transform;
//         }

//         override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//         {
//             timer += Time.deltaTime;
//             if (timer > patrolTime)
//             {
//                 animator.SetBool("Patrol", false);
//             }

//             if (player != null)
//             {
//                 float distance = Vector3.Distance(animator.transform.position, player.position);
//                 if (distance < chaseRange)
//                 {
//                     animator.SetBool("Chasing", true);
//                 }
//             }

//             if (agent.remainingDistance <= agent.stoppingDistance)
//             {
//                 agent.SetDestination(points[Random.Range(0, points.Count)].position);
//             }
//         }

//         override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//         {
//             agent.ResetPath();
//         }
//     }
// }
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace collegeGame
{
    public class Skelet_patrol : StateMachineBehaviour
    {
        private float timer;
        private List<Transform> points = new List<Transform>();
        private NavMeshAgent agent;
        private Transform player;
        private float chaseRange = 10f;
        private const float patrolTime = 5f;
        private bool hasPoints;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            timer = 0f;
            GameObject pointsObject = GameObject.FindGameObjectWithTag("Points");
            if (pointsObject != null)
            {
                foreach (Transform t in pointsObject.transform)
                {
                    points.Add(t);
                }
                hasPoints = true;
                agent = animator.GetComponent<NavMeshAgent>();
                SetRandomDestination();
            }
            else
            {
                hasPoints = false;
            }

            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!hasPoints)
                return;

            timer += Time.deltaTime;
            if (timer > patrolTime)
            {
                animator.SetBool("Patrol", false);
            }

            if (player != null)
            {
                float distance = Vector3.Distance(animator.transform.position, player.position);
                if (distance < chaseRange)
                {
                    animator.SetBool("Chasing", true);
                }
            }

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                SetRandomDestination();
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!hasPoints)
                return;

            agent.ResetPath();
        }

        private void SetRandomDestination()
        {
            agent.SetDestination(points[Random.Range(0, points.Count)].position);
        }
    }
}
