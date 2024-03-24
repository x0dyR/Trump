using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace collegeGame
{
    public class Troll : MonoBehaviour
    {
        public GameObject Player;
        private UnityEngine.AI.NavMeshAgent agent;
        private Animator animator;

        private void Start()
        {
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            agent.destination = Player.transform.position;

           
            float distanceToPlayer = Vector3.Distance(transform.position, Player.transform.position);

            
            if (distanceToPlayer <= 5f)
            {
                
                agent.isStopped = true;

                animator.SetBool("IsRunning", false);
            }
            else
            {
                
                agent.isStopped = false;

             
                animator.SetBool("IsRunning", agent.velocity.magnitude > 0);
            }
        }
    }
}





// public class Troll : MonoBehaviour
// {
//     private NavMeshAgent agent;
//     private Animator animator;

//     private void Start()
//     {
//         agent = GetComponent<NavMeshAgent>();
//         animator = GetComponentInChildren<Animator>();
//     }

//     private void Update()
//     {
//         if (!agent.hasPath || agent.remainingDistance < 0.5f)
//         {
//             
//             Vector3 randomPoint = Random.insideUnitSphere * 50f;
//             NavMeshHit hit;
//             NavMesh.SamplePosition(transform.position + randomPoint, out hit, 50f, NavMesh.AllAreas);

//             
//             agent.SetDestination(hit.position);
            
//             // Устанавливаем параметр анимации IsWalking
//             animator.SetBool("IsWalking", true);
//         }
//     }
// }
