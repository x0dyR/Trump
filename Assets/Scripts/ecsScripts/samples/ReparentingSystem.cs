using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

namespace collegeGame
{
    public partial struct ReparentingSystem : ISystem
    {
        bool attached;
        float timer;
        const float interval = 0.7f;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            timer = interval;
            attached = true;
            state.RequireForUpdate<Reparenting>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            timer -= SystemAPI.Time.DeltaTime;
            if (timer > 0)
            { return; }
            timer = interval;
            var rotatorEnity = SystemAPI.GetSingletonEntity<RotationSpeed>();
            var ecb = new EntityCommandBuffer(Allocator.Temp);

            if(attached)
            {
                DynamicBuffer<Child> children = SystemAPI.GetBuffer<Child>(rotatorEnity);
                for(int i = 0;i < children.Length;i++)
                {
                    ecb.RemoveComponent<Parent>(children[i].Value);
                }
            }
            else
            {
                foreach(var(transform,entity) in 
                    SystemAPI.Query<RefRO<LocalTransform>>()
                    .WithNone<RotationSpeed>().WithEntityAccess())
                {
                    ecb.AddComponent(entity, new Parent { Value = rotatorEnity });
                }
            }
            ecb.Playback(state.EntityManager);
            attached = !attached;
        }
    }
}
