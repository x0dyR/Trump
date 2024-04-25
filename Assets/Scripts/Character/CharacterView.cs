using UnityEngine;

namespace collegeGame.StateMachine
{
    [RequireComponent(typeof(Animator))]

    public class CharacterView : MonoBehaviour
    {
        ///<summary>
        /// без каких либо для триггеров
        /// "is" для булевых 
        ///"f_" для флоата
        /// "int_" инт
        /// </summary>
        private Animator _animator;

        public readonly string idle = "isIdling";
        public readonly string running = "isRunning";
        public readonly string grounded = "isGrounded";
        public readonly string jumping = "isJumping";
        public readonly string falling = "isFalling";
        public readonly string airborn = "isAirborn";
        public readonly string movement = "isMovement";

        //experiments
        public readonly string moving = "isMoving";
        public readonly string attacking = "isAttacking";
        public readonly string attack = "Attack";

        public void Initialize() => _animator = GetComponent<Animator>();

        public Animator Animator => _animator;

        public void StartIdle() => _animator.SetBool(idle, true);
        public void StopIdle() => _animator.SetBool(idle, false);

        public void StartRunning() => _animator.SetBool(running, true);
        public void StopRunning() => _animator.SetBool(running, false);

        public void StartGrounded() => _animator.SetBool(grounded, true);
        public void StopGrounded() => _animator.SetBool(grounded, false);

        public void StartJumping() => _animator.SetBool(jumping, true);
        public void StopJumping() => _animator.SetBool(jumping, false);

        public void StartFalling() => _animator.SetBool(falling, true);
        public void StopFalling() => _animator.SetBool(falling, false);

        public void StartAirborn() => _animator.SetBool(airborn, true);
        public void StopAirborn() => _animator.SetBool(airborn, false);

        /*public void StartAttacking() => _animator.SetTrigger(attacking); //триггер чтобы было комбо   */     
        
        public void StartMoving() => _animator.SetBool(moving, true);
        public void StopMoving() => _animator.SetBool(moving, false);

        public void StartAttacking() { _animator.SetBool(attacking, true); _animator.SetTrigger(attack); }
        public void StopAttacking() => _animator.SetBool(attacking, false);
    }
}
