using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace collegeGame
{
    public class EnemyAuthoring : MonoBehaviour
    {
        public float enemyDamage;
        public float enemyLevel;
        public float enemyHealth;
        public GameObject player;
    }

    public class EnemySpawnerBaker : Baker<EnemyAuthoring>
    {
        public override void Bake(EnemyAuthoring authoring)
        {

            var transform = GetComponent<Transform>();
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new EnemyEntity
            {
                prefab = GetEntity(authoring.player,TransformUsageFlags.Dynamic)
            });
            AddComponent(entity, new EnemyHealth
            {
                health = authoring.enemyHealth
            });
            AddComponent(entity, new EnemyLevel
            {
                level = authoring.enemyLevel
            });
            AddComponent(entity, new EnemyDamage
            {
                damage = authoring.enemyDamage
            });
            AddComponent(entity, new EnemyPosition
            {
                pos = new float3(1f,2f,3f),
            });
        }
    }
}
