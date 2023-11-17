using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

namespace collegeGame
{
    public class SpawnerEnemyAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        class Baker : Baker<SpawnerEnemyAuthoring>
        {
            public override void Bake(SpawnerEnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new EnemyEntity 
                { 
                    Prefab = GetEntity(authoring.Prefab,TransformUsageFlags.None)
                });
            }
        }
    }
    public struct EnemyEntity : IComponentData
    {
        public Entity Prefab;
    }
}
