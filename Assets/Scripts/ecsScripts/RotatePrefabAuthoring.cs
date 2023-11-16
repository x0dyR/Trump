using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace collegeGame
{
    public class RotatePrefabAuthoring : MonoBehaviour
    {
        public float degrees = 360.0f;
        class Baker : Baker<RotatePrefabAuthoring>
        {
            public override void Bake(RotatePrefabAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new RotationSpeed
                {
                    Radians = math.radians(authoring.degrees)
                });
            }
        }
    }
    public struct RotationSpeed : IComponentData
    {
        public float Radians;
    }
}
