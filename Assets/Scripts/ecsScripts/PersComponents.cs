using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace collegeGame
{
    public struct PlayerMoveInput : IComponentData
    {
        public float2 Value;
    }

    public struct PlayerMoveSpeed : IComponentData
    {
        public float Value;
    }
}