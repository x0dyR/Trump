using System;
using UnityEngine;

namespace collegeGame
{
    [CreateAssetMenu(fileName = "EnemyFabric", menuName = "Factory/EnemyFabric")]
    public class EnemyFabric : ScriptableObject
    {
        [field: SerializeField] private EnemyConfig _skelet, _troll;

        public AbsEnemy Get(EnemyType enemyType)
        {
            EnemyConfig config = GetConfigBy(enemyType);
            AbsEnemy instance = Instantiate(config.Prefab);
            return instance;
        }

        private EnemyConfig GetConfigBy(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.Skelet:
                    return _skelet;
                case EnemyType.Troll:
                    return _troll;
                default:
                    throw new ArgumentException(nameof(enemyType));
            }
        }
    }
}
