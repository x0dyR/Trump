using UnityEngine;
using UnityEngine.AI;

namespace collegeGame
{
    public class Troll : MonoBehaviour
    {
        public Transform playerTransform;
        private NavMeshAgent agent;
        private Animator animator;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            agent.destination = playerTransform.position;
            /*float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);*/
            //Vector3.Distance медленный
            Vector3 vectorDistanceToPlayer = transform.position - agent.destination;
            float distanceToPlayer = Mathf.Sqrt(vectorDistanceToPlayer.sqrMagnitude);

            if (distanceToPlayer <= 7f)
            {
                agent.isStopped = false;
                animator.SetFloat("Blend", 3);
            }
            else if (distanceToPlayer <= 10f)
            {
                agent.isStopped = false;
                animator.SetFloat("Blend", 2);
            }
            else if (distanceToPlayer >= 15f)
            {
                agent.isStopped = true;
                animator.SetFloat("Blend", 0);
            }
            else if (distanceToPlayer <= 15f)
            {
                agent.isStopped = false;
                animator.SetFloat("Blend", 1);
            }
            /*
            if (distanceToPlayer <= 5f)
            {
                agent.isStopped = true;
                animator.SetFloat("Blend", .4f);
            }
            else
            {
                agent.isStopped = false;
                animator.SetFloat("Blend", .7f);
            }*/
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
