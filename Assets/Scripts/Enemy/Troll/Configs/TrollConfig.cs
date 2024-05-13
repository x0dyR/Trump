using UnityEngine;

namespace collegeGame
{
    [CreateAssetMenu(fileName = "TrollConfig", menuName = "Enemy/TrollConfig")]
    public class TrollConfig : ScriptableObject
    {
       
        [field: SerializeField, Range(1, 5)] private float _patrolSpeed; // Скорость патрулирования
        [field: SerializeField, Range(0, 30)] private float _distanceToView;// дистанция видимости
        [field: SerializeField, Range(0, 30)] private float _attackRange;// дальность атаки
        [field: SerializeField, Range(0, 30)] private float _speed;// скорость погони за игроком(тобишь основная скорость)
        [field: SerializeField, Range(0, 30)] private float _damage;// получает урон от кого либо
        [field: SerializeField, Range(0, 100)] private float _health;// здоровье противника
        [field: SerializeField, Range(1, 30)] private float _enemyDamage; // Урон, наносимый скелетом
        

        public float PatrolSpeed => _patrolSpeed;
        public float DistanceToView => _distanceToView;
        public float AttackRange => _attackRange;
        public float Speed => _speed;
        public float Damage => _damage;
        public float EnemyDamage => _enemyDamage;
        public float Health => _health;
        
    }
}
