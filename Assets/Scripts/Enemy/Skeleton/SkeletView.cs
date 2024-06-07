using UnityEngine;

namespace Trump
{
    [RequireComponent(typeof(Animator))]
    public class SkeletView : MonoBehaviour
    {
        private Animator _animator;

        //public readonly string idle = "Idle";
        public readonly string patrol = "Patrol";
        public readonly string chase = "Chase";
        public readonly string attack = "Attack";
        public readonly string dead = "Dead";

        public void Initialize() => _animator = GetComponent<Animator>();
        public Animator Animator => _animator;


        public void StartPatrol()
        {
            //_animator.SetBool(idle, false);
            _animator.SetBool(patrol, true);
            _animator.SetBool(chase, false);
            _animator.SetBool(attack, false);
            
        }

        public void StartChase()
        {
            //_animator.SetBool(idle, false);
            _animator.SetBool(patrol, false);
            _animator.SetBool(chase, true);
            _animator.SetBool(attack, false);
            
        }

        public void StartAttack()
        {
           // _animator.SetBool(idle, false);
            _animator.SetBool(patrol, false);
            _animator.SetBool(chase, false);
            _animator.SetBool(attack, true);
           
        }
        public void StartDead()
        {
            //_animator.SetBool(idle, false);
            _animator.SetBool(patrol, false);
            _animator.SetBool(chase, false);
            _animator.SetBool(attack, false);
            _animator.SetTrigger(dead);
        }
    }
}



