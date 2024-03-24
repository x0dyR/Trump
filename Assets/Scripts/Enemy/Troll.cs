using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace collegeGame
{
    public class Troll : MonoBehaviour
    {
        public GameObject Player;
        private UnityEngine.AI.NavMeshAgent agent;
        private Animator animator; // Добавляем переменную аниматора

        private void Start()
        {
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            
            // Получаем доступ к компоненту аниматора дочернего объекта с именем "Troll"
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            agent.destination = Player.transform.position;

            // Устанавливаем параметр анимации run, если скорость NavMeshAgent больше нуля
            if (agent.velocity.magnitude > 0)
            {
                animator.SetBool("IsRunning", true);
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }
        }
    }
}
// using UnityEngine;
// using UnityEngine.AI;

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
//             // Выбираем случайную точку внутри навигационной сетки
//             Vector3 randomPoint = Random.insideUnitSphere * 50f;
//             NavMeshHit hit;
//             NavMesh.SamplePosition(transform.position + randomPoint, out hit, 50f, NavMesh.AllAreas);

//             // Устанавливаем случайную точку как новую цель для NavMeshAgent
//             agent.SetDestination(hit.position);
            
//             // Устанавливаем параметр анимации IsWalking
//             animator.SetBool("IsWalking", true);
//         }
//     }
// }
