using UnityEngine;

namespace collegeGame
{
    [CreateAssetMenu(fileName = "TrollConfig", menuName = "Enemy/TrollConfig")]
    public class TrollConfig : ScriptableObject
    {
       
        [field: SerializeField, Range(1, 5)] private float _patrolSpeed; // �������� ��������������
        [field: SerializeField, Range(0, 30)] private float _distanceToView;// ��������� ���������
        [field: SerializeField, Range(0, 30)] private float _attackRange;// ��������� �����
        [field: SerializeField, Range(0, 30)] private float _speed;// �������� ������ �� �������(������ �������� ��������)
        [field: SerializeField, Range(0, 30)] private float _damage;// �������� ���� �� ���� ����
        [field: SerializeField, Range(0, 100)] private float _health;// �������� ����������
        [field: SerializeField, Range(1, 30)] private float _enemyDamage; // ����, ��������� ��������
        

        public float PatrolSpeed => _patrolSpeed;
        public float DistanceToView => _distanceToView;
        public float AttackRange => _attackRange;
        public float Speed => _speed;
        public float Damage => _damage;
        public float EnemyDamage => _enemyDamage;
        public float Health => _health;
        
    }
}
