using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace collegeGame
{
    public struct EnemyHealth : IComponentData
    {
        public float health;
    }

    public struct EnemyEntity : IComponentData
    {
        public Entity prefab;
    }

    public struct EnemyLevel : IComponentData
    {
        public float level;
    }

    public struct EnemyDamage : IComponentData
    {
        public float damage;
    }

    public struct EnemyPosition : IComponentData
    {
        public float3 pos;
    }
}
