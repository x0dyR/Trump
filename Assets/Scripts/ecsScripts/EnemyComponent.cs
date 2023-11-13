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
        public GameObject prefab;
    }

    public struct EnemyLevel : IComponentData
    {
        public float level;
    }

    public struct EnemyDamage : IComponentData
    {
        public float damage;
    }

    public struct EnemyInventory : IComponentData
    {
        // Используем массив фиксированного размера вместо List<string>
        public string[] inventory;
    }

    public struct EnemyPosition : IComponentData
    {
        public float3 pos;
    }
}
