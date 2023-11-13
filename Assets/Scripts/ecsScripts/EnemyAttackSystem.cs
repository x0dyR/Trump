using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity;
using UnityEngine;
using Unity.Burst;

namespace collegeGame
{
    public partial struct EnemyAttackSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            new AttackJob
            {

            }.Schedule();

        }

        [BurstCompile]
        public partial struct AttackJob : IJobEntity
        {
            [BurstCompile]
            private void Execute(
                ref LocalTransform enemyPosition,
                ref PlayerHealth playerHealth,
                ref PlayerPosition playerPosition,
                ref EnemyDamage enemyDamage,
                ref PlayerDamage playerDamage,
                ref PlayerLevel playerLevel,
                ref EnemyLevel enemyLevel)
            {

            }
        }
    }
}
