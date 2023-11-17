using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace collegeGame
{
    public class EnemyAuthoring : MonoBehaviour
    {
        public float EnemyHealth;
        public float EnemyDamage;
        public float EnemySpeed;
        class Baker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new EnemyHealth
                {
                    Value = authoring.EnemyHealth
                });
                AddComponent(entity, new EnemyDamage
                {
                    Value = authoring.EnemyDamage
                });
                AddComponent(entity, new EnemyMS
                {
                    Value = authoring.EnemySpeed
                });
            }
        }
    }

    public struct EnemyHealth : IComponentData
    {
        public float Value;
    }
    public struct EnemyDamage : IComponentData
    {
        public float Value;
    }
    public struct EnemyMS : IComponentData
    {
        public float Value;
    }
}
