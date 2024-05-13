using UnityEngine;

namespace collegeGame.Enemy
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig")]

    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField, Range(0, 30)] private float _distanceToView;
        [field: SerializeField, Range(0, 30)] private float _attackRange;
        [field: SerializeField, Range(0, 30)] private float _speed;
        [field: SerializeField, Range(0, 30)] private float _damage;
        [field: SerializeField, Range(0, 30)] private float _health;

        public float DistanceToView => _distanceToView;

        public float AttackRange => _attackRange;

        public float Speed => _speed;

        public float Damage => _damage;

        public float Health => _health;
        
    }
}
