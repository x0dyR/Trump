// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// namespace collegeGame
// {
//     public class TrollAttack : StateMachineBehaviour
//     {
//         public float attackDistance = 3f; // Расстояние, при котором тролль начнет атаку
//         public string attackLayerName = "Attack"; // Имя слоя атаки

//         private Animator attackAnimator; // Ссылка на аниматор атаки
//         private Transform playerTransform; // Ссылка на игрока

//         override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//         {
//             // Получаем ссылки на аниматор атаки и игрока
//             attackAnimator = animator.GetComponentInChildren<Animator>();
//             playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

//             if (attackAnimator == null || playerTransform == null)
//             {
//                 Debug.LogError("Attack animator or player not found!");
//                 return;
//             }
            
//             // Включаем слой атаки, чтобы он мог начать проигрываться
//             attackAnimator.SetLayerWeight(attackAnimator.GetLayerIndex(attackLayerName), 1);
//         }

//         override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//         {
//             // Проверяем расстояние между троллем и игроком
//             float distance = Vector3.Distance(animator.transform.position, playerTransform.position);
            
//             // Если расстояние меньше или равно заданному, активируем атаку
//             if (distance <= attackDistance)
//             {
//                 attackAnimator.SetBool("IsAttack", true);
//             }
//             else
//             {
//                 attackAnimator.SetBool("IsAttack", false);
//             }
//         }

//         override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//         {
//             // Выключаем слой атаки при выходе из состояния атаки
//             attackAnimator.SetLayerWeight(attackAnimator.GetLayerIndex(attackLayerName), 0);
//         }
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace collegeGame
{
    public class TrollAttack : StateMachineBehaviour
    {
        public float attackDistance = 3f; // Расстояние, при котором тролль начнет атаку
        public string attackLayerName = "Attack"; // Имя слоя атаки

        private Animator attackAnimator; // Ссылка на аниматор атаки
        private Transform playerTransform; // Ссылка на игрока
        private bool isAttacking; // Флаг для отслеживания состояния атаки

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // ссылки на аниматор атаки и игрока
            attackAnimator = animator.GetComponentInChildren<Animator>();
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

            if (attackAnimator == null || playerTransform == null)
            {
                Debug.LogError("Attack animator or player not found!");
                return;
            }
            
            // Включаем слой атаки, чтобы он мог начать проигрываться
            attackAnimator.SetLayerWeight(attackAnimator.GetLayerIndex(attackLayerName), 1);
            isAttacking = true; // Устанавливаем флаг атаки в true при входе в состояние атаки
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // Проверяем, находится ли тролль в режиме атаки и расстояние до игрока
            if (isAttacking)
            {
                float distance = Vector3.Distance(animator.transform.position, playerTransform.position);

                // Если расстояние меньше или равно заданному, активируем атаку
                if (distance <= attackDistance)
                {
                    attackAnimator.SetBool("IsAttack", true);
                }
                else
                {
                    attackAnimator.SetBool("IsAttack", false);
                }
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // Выключаем слой атаки при выходе из состояния атаки
            attackAnimator.SetLayerWeight(attackAnimator.GetLayerIndex(attackLayerName), 0);

            // По завершении атаки, поворачиваем тролля к игроку
            Vector3 directionToPlayer = playerTransform.position - animator.transform.position;
            directionToPlayer.y = 0f; // Игнорируем изменение высоты
            animator.transform.rotation = Quaternion.LookRotation(directionToPlayer);

            // Устанавливаем флаг атаки в false при выходе из состояния атаки
            isAttacking = false;
        }
    }
}

