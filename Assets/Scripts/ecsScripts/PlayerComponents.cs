using Unity.Entities;
using Unity.Mathematics;

namespace collegeGame
{
    public struct PlayerHealth : IComponentData
    {
        public float health;
    }

    public struct PlayerDamage : IComponentData
    {
        public float dmg;
    }

    public struct PlayerLevel : IComponentData
    {
        public float level;
    }

    public struct PlayerPosition : IComponentData
    {
        public float3 position;
    }
}
