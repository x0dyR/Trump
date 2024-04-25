using UnityEngine;

namespace collegeGame.Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyView : MonoBehaviour
    {
        ///<summary>
        /// ��� ����� ���� ��� ���������
        /// "is" ��� ������� 
        ///"f_" ��� ������
        /// "int_" ���
        /// </summary>
        private Animator _animator;

        public readonly string idle = "isIdling";
        public readonly string find = "isFinding";
        public readonly string attack = "isAttacking";

        public void Initialize() => _animator = GetComponent<Animator>();
        public Animator Animator => _animator;

        public void StartIdle() => _animator.SetBool(idle,true);//������ ������ � "=>" �� ������� �������� � ���������� ����� �� ���. �������
        public void StopIdle() => _animator.SetBool(idle,false);

        public void StartFind() => _animator.SetBool(find,true);
        public void StopFind() => _animator.SetBool(find, false); 

        public void StartAttack() => _animator.SetBool(attack, true);
        public void StopAttack() => _animator.SetBool(attack, false);
    }
}
