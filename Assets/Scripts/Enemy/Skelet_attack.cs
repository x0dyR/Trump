// using UnityEngine;

// namespace collegeGame
// {
//     public class Skelet_attack : StateMachineBehaviour
//     {
//         private Transform player;
//         private const float attackRange = 3f;

//         override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//         {
//             player = GameObject.FindGameObjectWithTag("Player")?.transform;
//         }

//         override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//         {
//             if (player != null)
//             {
//                 animator.transform.LookAt(player);
//                 float distance = Vector3.Distance(animator.transform.position, player.position);
//                 if (distance < attackRange)
//                 {
//                     animator.SetBool("Attack", false);
//                 }
//             }
//         }
//     }
// }
using UnityEngine;

namespace collegeGame
{
    public class Skelet_attack : StateMachineBehaviour
    {
        private Transform player;
        private const float attackRange = 3f;
        private const float attackCooldown = 2f;
        private float lastAttackTime;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            lastAttackTime = -attackCooldown; // Готов к атаке сразу после входа в состояние
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (player != null)
            {
                animator.transform.LookAt(player);
                if (Time.time - lastAttackTime > attackCooldown)
                {
                    float distance = (animator.transform.position - player.position).sqrMagnitude;
                    if (distance < attackRange * attackRange)
                    {
                        // Выполнение атаки
                        Attack();
                        lastAttackTime = Time.time;
                    }
                }
            }
        }

        private void Attack()
        {
            // Логика атаки, например, запуск анимации атаки или нанесение урона игроку
            Debug.Log("Skeleton attacks!");
        }
    }
}
